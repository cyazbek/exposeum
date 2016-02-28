using System;
using Exposeum.Views;
using Exposeum.Models;
using Android.App;

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

			_model = new Map ();

			_beaconFinder.addObserver (this);

			_beaconFinder.setStoryLine(_model.CurrentStoryline);
			_beaconFinder.setInFocus(true);
			_beaconFinder.findBeacons();

			//If we are not in free explorer mode (ie there exists a current storyline) then add the
			//current storyline progression fragment to the map activity
			if (_model.CurrentStoryline != null) {

				// Create a new fragment and a transaction.
				FragmentTransaction fragmentTx = ((Activity)_map_view.Context).FragmentManager.BeginTransaction();
				_map_progression_view = new MapProgressionFragment(_model.CurrentStoryline);

				fragmentTx.Add(Resource.Id.map_frag_frame_lay, _map_progression_view);
				fragmentTx.Commit();

			}
		}

		public void FloorChanged(int newFloorIndex){
			_model.SetCurrentFloor(newFloorIndex);
			_map_view.Update ();
		}

		public void beaconFinderObserverUpdate (IBeaconFinderObservable observable)
		{
			BeaconFinder beaconFinder = (BeaconFinder)observable;
			EstimoteSdk.Beacon beacon = beaconFinder.getClosestBeacon();

			if (beacon != null)
			{
				if (_model.CurrentStoryline.hasBeacon(beacon))
				{
					PointOfInterest poi = _model.CurrentStoryline.findPOI(beacon);
					poi.SetTouched();
					_model.CurrentStoryline.addVisitedPoiToList(poi);
				}
			}

			_map_progression_view.Update ();
			_map_view.Update ();
		}

		public void PointOfInterestTapped(PointOfInterest selectedPOI){

			_map_view.InitiatePointOfInterestPopup (selectedPOI);

			PointOfInterest latestUnvisited = null;

			//only allow the next unvisited node to be clicked
			for (int i = 0; i < _model.CurrentStoryline.poiList.Count; i++) {
				if (!_model.CurrentStoryline.poiList [i].visited) {
					latestUnvisited = _model.CurrentStoryline.poiList [i];
					break;
				}
			}

			if (selectedPOI.Equals (latestUnvisited)) {
				selectedPOI.SetTouched ();
				_model.CurrentStoryline.poiVisitedList.Add (latestUnvisited);
			}

			_map_progression_view.Update ();
			_map_view.Update (); //technically unncecessary but included for completeness
		}

		public Map Model
		{
			get { return this._model; }
		}
	}
}
