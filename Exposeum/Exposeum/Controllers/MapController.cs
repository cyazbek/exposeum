using System;
using Exposeum.Views;
using Exposeum.Models;
using Android.App;

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
			_beaconFinder.setInFocus(true);
			_beaconFinder.findBeacons();

			if (_model.CurrentStoryline != null) {

				// Create a new fragment and a transaction.
				FragmentTransaction fragmentTx = ((Activity)_view.Context).FragmentManager.BeginTransaction();
				MapProgressionFragment newMapProgressionFrag = new MapProgressionFragment();

				// The fragment will have the ID of Resource.Id.fragment_container.
				fragmentTx.Add(Resource.Id.map_frag_frame_lay, newMapProgressionFrag);

				// Commit the transaction.
				fragmentTx.Commit();

			}
		}

		public void FloorChanged(int newFloorIndex){
			_model.SetCurrentFloor(newFloorIndex);
			_view.Update ();
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

			_view.Update ();
		}

		public void PointOfInterestTapped(PointOfInterest selectedPOI){

			_view.InitiatePointOfInterestPopup (selectedPOI);

			PointOfInterest latestUnvisited = null;

			//only allow the next unvisited node to be clicked
			for (int i = 0; i < _model.CurrentStoryline.poiList.Count; i++) {
				if (!_model.CurrentStoryline.poiList [i].visited) {
					latestUnvisited = _model.CurrentStoryline.poiList [i];
					break;
				}
			}

			if (selectedPOI.Equals(latestUnvisited))
				selectedPOI.SetTouched ();
			
			_view.Update (); //technically unncecessary but included for completeness
		}

		public Map Model
		{
			get { return this._model; }
		}
	}
}
