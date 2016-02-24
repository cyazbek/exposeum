using System;
using Exposeum.Views;
using Exposeum.Models;

namespace Exposeum.Controller
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

		public void PointOfInterestTapped(PointOfInterest poi){
			poi.SetTouched ();
			_view.update (); //technically unncecessary but included for completeness
		}
	}
}
