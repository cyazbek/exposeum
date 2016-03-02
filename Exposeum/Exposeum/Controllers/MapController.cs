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
			_model.SetCurrentFloor(newFloorIndex);
			_view.Update ();
		}

		public void beaconFinderObserverUpdate (IBeaconFinderObservable observable)
		{
			BeaconFinder beaconFinder = (BeaconFinder)observable;
			EstimoteSdk.Beacon beacon = beaconFinder.getClosestBeacon();

            if (beacon != null && (_model.CurrentStoryline.hasBeacon(beacon)))
			{
			    PointOfInterest poi = _model.CurrentStoryline.findPOI(beacon);

				if(!poi.visited && poi.floor.Equals(_model.CurrentFloor)) { //don't display a popup if the beacon has already been visited or if the poi is not on app's current floor
					_model.CurrentStoryline.updateProgress(poi);
					displayPopUp(poi);
				}
			}

			_view.Update ();
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
