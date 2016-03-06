using System;
using Exposeum.Views;
using Exposeum.Models;
using Android.App;

using System.Collections.Generic;
using System.Linq;

namespace Exposeum.Controllers
{
	public class MapController : IBeaconFinderObserver
	{
		private MapView _map_view;
		private MapProgressionFragment _map_progression_view;
		private Map _model;
		private BeaconFinder _beaconFinder = BeaconFinder.getInstance();

		public MapController(MapView view){
			_map_view = view;

			_model = MapService.GetMapInstance ();

			_beaconFinder.addObserver (this);

			_beaconFinder.setStoryLine(_model.CurrentStoryline);
			_beaconFinder.findBeacons();

			//If we are not in free explorer mode (ie there exists a current storyline) then add the
			//current storyline progression fragment to the map activity
			if (!ExposeumApplication.IsExplorerMode) {

				// Create a new fragment and a transaction.
				FragmentTransaction fragmentTx = ((Activity)_map_view.Context).FragmentManager.BeginTransaction();
				_map_progression_view = new MapProgressionFragment(_model.CurrentStoryline);

				fragmentTx.Add(Resource.Id.map_frag_frame_lay, _map_progression_view);
				fragmentTx.Commit();

			}
		}

		public void FloorChanged(int newFloorIndex){
			Floor newFloor = _model.Floors [newFloorIndex];
			if (newFloor != null)
				_model.SetCurrentFloor(newFloor);
			_map_view.Update ();
		}

		public void beaconFinderObserverUpdate (IBeaconFinderObservable observable)
		{
			BeaconFinder beaconFinder = (BeaconFinder)observable;
			EstimoteSdk.Beacon beacon = beaconFinder.getClosestBeacon();

            if (beacon != null && (_model.CurrentStoryline.hasBeacon(beacon)))
			{
			    PointOfInterest poi = _model.CurrentStoryline.findPOI(beacon);

			    if (!poi.Visited)
			    {
                    // for TESTS:
			        ExposeumApplication.IsExplorerMode = false;
			        //don't display a popup if the beacon has already been visited or if the poi is not on app's current floor
			        if (!ExposeumApplication.IsExplorerMode)
			        {
			            try
			            {
			                _model.CurrentStoryline.updateProgress(poi);

							if(poi.floor != _model.CurrentFloor)
								_model.SetCurrentFloor(poi.floor);
							displayPopUp(poi);
                        }
                        catch (PointOfInterestNotVisitedException e)
			            {
							DisplayOutOfOrderPointOfInterestPopup(e.POI);
			            }
                    }
                    else
                    {
                        poi.SetVisited();
						if(poi.floor != _model.CurrentFloor)
                        displayPopUp(poi);
                    }
			    }

			}

			if (!ExposeumApplication.IsExplorerMode)
				_map_progression_view.Update ();

			_map_view.Update ();
		}

        /// <summary>
        /// Method to display informational toast to Visitors when out of sequence POI visited in storyline
        /// </summary>
	    private void DisplayOutOfOrderPointOfInterestPopup(PointOfInterest poi)
	    {
			_map_view.InitiateOutOfOrderPointOfInterestPopup(poi);
	    }

	    public void displayPopUp(PointOfInterest selectedPOI)
        {	
			_map_view.InitiatePointOfInterestPopup (selectedPOI);
			_map_view.Update (); //technically unncecessary but included for completeness
			if (!ExposeumApplication.IsExplorerMode)
				_map_progression_view.Update ();
		}

		public Map Model
		{
			get { return this._model; }
		}
	}
}
