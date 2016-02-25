using System;
using Exposeum.Views;
using Exposeum.Models;

namespace Exposeum.Controllers
{
	public class MapController : IBeaconFinderObserver
	{
		private static MapController _instance;
		private MapView _view;
		private Map _model;
		private BeaconFinder beaconFinder = BeaconFinder.getInstance();

		public MapController(MapView view){
			_view = view;

			_model = new Map ();

			beaconFinder.addObserver (this);

			beaconFinder.setStoryLine(_model._currentStoryline);
			beaconFinder.setInFocus(true);
			beaconFinder.findBeacons();
		}

		public Map GetMap(){
			return _model;
		}

		public void FoorChanged(int newFloorIndex){
			_model.SetCurrentFloor(newFloorIndex);
			_view.update ();
		}

		public void beaconFinderObserverUpdate (IBeaconFinderObservable observable)
		{
			BeaconFinder beaconFinder = (BeaconFinder)observable;
			EstimoteSdk.Beacon beacon = beaconFinder.getClosestBeacon();

			if (beacon != null)
			{
				if (_model._currentStoryline.hasBeacon(beacon))
				{
					PointOfInterest poi = _model._currentStoryline.findPOI(beacon);
					poi.SetTouched();
					_model._currentStoryline.addVisitedPoiToList(poi);
				}
			}

			_view.update ();
		}

		public void PointOfInterestTapped(PointOfInterest selectedPOI){

			_view.initiatePointOfInterestPopup (selectedPOI);

			PointOfInterest latestUnvisited = null;

			//only allow the next unvisited node to be clicked
			for (int i = 0; i < _model._currentStoryline.poiList.Count; i++) {
				if (!_model._currentStoryline.poiList [i].visited) {
					latestUnvisited = _model._currentStoryline.poiList [i];
					break;
				}
			}

			if (selectedPOI.Equals(latestUnvisited))
				selectedPOI.SetTouched ();
			
			_view.update (); //technically unncecessary but included for completeness
		}
	}
}
