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
		private MapView _mapView;
		private MapProgressionFragment _mapProgressionView;
		private Map _model;
		private BeaconFinder _beaconFinder = BeaconFinder.GetInstance();

		public MapController(MapView view){
			_mapView = view;

			_model = StoryLineService.GetMapInstance ();

			_beaconFinder.AddObserver (this);

			_beaconFinder.SetStoryLine(_model.CurrentStoryline);
			_beaconFinder.FindBeacons();

			//If we are not in free explorer mode (ie there exists a current storyline) then add the
			//current storyline progression fragment to the map activity
			if (!ExposeumApplication.IsExplorerMode) {

				// Create a new fragment and a transaction.
				FragmentTransaction fragmentTx = ((Activity)_mapView.Context).FragmentManager.BeginTransaction();
				_mapProgressionView = new MapProgressionFragment(_model.CurrentStoryline);

				fragmentTx.Add(Resource.Id.map_frag_frame_lay, _mapProgressionView);
				fragmentTx.Commit();

			}
		}

		public void FloorChanged(int newFloorIndex){
			Floor newFloor = _model.Floors [newFloorIndex];
			if (newFloor != null)
				_model.SetCurrentFloor(newFloor);
			_mapView.Update ();
		}

		public void BeaconFinderObserverUpdate (IBeaconFinderObservable observable)
		{
			BeaconFinder beaconFinder = (BeaconFinder)observable;
			EstimoteSdk.Beacon beacon = beaconFinder.GetClosestBeacon();

            if (beacon != null && (_model.CurrentStoryline.HasBeacon(beacon)))
			{
			    PointOfInterest poi = _model.CurrentStoryline.FindPoi(beacon);

			    if (!poi.Visited)
			    {
			        //don't display a popup if the beacon has already been visited or if the poi is not on app's current floor
			        if (!ExposeumApplication.IsExplorerMode)
			        {
			            try
			            {
			                _model.CurrentStoryline.UpdateProgress(poi);

							if(poi.Floor != _model.CurrentFloor)
								_model.SetCurrentFloor(poi.Floor);
							DisplayPopUp(poi);
                        }
                        catch (PointOfInterestNotVisitedException e)
			            {
							DisplayOutOfOrderPointOfInterestPopup(e.Poi);
			            }
                    }
                    else
                    {
                        poi.SetVisited();
						if(poi.Floor != _model.CurrentFloor)
                        DisplayPopUp(poi);
                    }
			    }

			}

			if (!ExposeumApplication.IsExplorerMode)
				_mapProgressionView.Update ();

			_mapView.Update ();
		}

        /// <summary>
        /// Method to display informational toast to Visitors when out of sequence POI visited in storyline
        /// </summary>
	    private void DisplayOutOfOrderPointOfInterestPopup(PointOfInterest poi)
	    {
			_mapView.InitiateOutOfOrderPointOfInterestPopup(poi);
	    }

	    public void DisplayPopUp(PointOfInterest selectedPoi)
        {	
			_mapView.InitiatePointOfInterestPopup (selectedPoi);
			_mapView.Update (); //technically unncecessary but included for completeness
			if (!ExposeumApplication.IsExplorerMode)
				_mapProgressionView.Update ();
		}

		public Map Model
		{
			get { return this._model; }
		}
	}
}
