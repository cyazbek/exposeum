using System;
using Exposeum.Views;
using Exposeum.Models;

using System.Collections.Generic;
using System.Linq;

namespace Exposeum.Controllers
{
	public class MapController : IBeaconFinderObserver
	{
		private MapView _view;
		private Map _model;
		private BeaconFinder _beaconFinder = BeaconFinder.getInstance();

		public MapController(MapView view){
			_view = view;

			_model = new Map ();

			_beaconFinder.addObserver (this);

			_beaconFinder.setStoryLine(_model.CurrentStoryline);
			_beaconFinder.findBeacons();
		}

		public void FloorChanged(int newFloorIndex){
			Floor newFloor = _model.Floors [newFloorIndex];
			if (newFloor != null)
				_model.SetCurrentFloor(newFloor);
			_view.Update ();
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

			_view.Update ();
		}

        /// <summary>
        /// Method to display informational toast to Visitors when out of sequence POI visited in storyline
        /// </summary>
	    private void DisplayOutOfOrderPointOfInterestPopup(PointOfInterest poi)
	    {
            _view.InitiateOutOfOrderPointOfInterestPopup(poi);
	    }

	    public void displayPopUp(PointOfInterest selectedPOI)
        {	
			_view.InitiatePointOfInterestPopup (selectedPOI);
			_view.Update (); //technically unncecessary but included for completeness
		}

		public Map Model
		{
			get { return this._model; }
		}
	}
}
