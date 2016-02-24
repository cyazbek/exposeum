using System;
using Exposeum.Views;
using Exposeum.Models;

namespace Exposeum.Controllers
{
	public class MapController
	{
		private static MapController _instance = new MapController();
		private MapView _view;
		private Map _model;

		public static MapController getInstance(){
			return _instance; //eager loading
		}

		public Map RegisterMapView(MapView mapView){
			_view = mapView;
			_model = new Map ();
			return _model;
		}

		public void FoorChanged(int newFloorIndex){
			_model.SetCurrentFloor(newFloorIndex);
			_view.update ();
		}

		public void PointOfInterestTapped(PointOfInterest selectedPOI){

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
