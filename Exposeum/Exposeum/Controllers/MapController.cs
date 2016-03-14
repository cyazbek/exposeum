using System;
using Exposeum.Views;
using Exposeum.Models;
using Android.App;

using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace Exposeum.Controllers
{
	public class MapController : IBeaconFinderObserver
	{
		private MapView _mapView;
		public View _totalMapView { get; private set;}
		private static MapController _instance;
		private MapProgressionFragment _mapProgressionView;
		private Context _context{get; set;}
		private Map _mapModel;
		private BeaconFinder _beaconFinder = BeaconFinder.GetInstance();


		public static MapController GetInstance(){
			return _instance;
				
		}

		public static MapController GetInstance(Context context){
			if (_instance == null)
				_instance = new MapController (context);
			return _instance;
		}

		public void Destroy(){
			//remove the current singleton from the list of observers
			_beaconFinder.RemoveObserver (_instance);
			_instance = null;
		}

		private MapController(Context context){

			ConfigureMapView (context);

			_mapModel = Map.GetInstance ();

			_beaconFinder.AddObserver (this);

			_beaconFinder.SetStoryLine(_mapModel.CurrentStoryline);

			//If we are not in free explorer mode (ie there exists a current storyline) then add the
			//current storyline progression fragment to the map activity
			if (!ExposeumApplication.IsExplorerMode) {

				// Create a new fragment and a transaction.
				FragmentTransaction fragmentTx = ((Activity)_mapView.Context).FragmentManager.BeginTransaction();
				_mapProgressionView = new MapProgressionFragment(_mapModel.CurrentStoryline);

				fragmentTx.Add(Resource.Id.map_frag_frame_lay, _mapProgressionView);
				fragmentTx.Commit();

			}

			_beaconFinder.FindBeacons();
		}

		private void ConfigureMapView(Context context){
			LayoutInflater li = LayoutInflater.FromContext (context);
			//Load the MapView XML
			_totalMapView = li.Inflate (Resource.Layout.MapView, null);
			//Get a reference to the frame layout in the XML
			FrameLayout mapViewFrameLayout = (FrameLayout)_totalMapView.FindViewById (Resource.Id.map_view_frame_lay);
			//create the Mapview
			_mapView = new MapView (context, this);
			//Add a new MapView into the frame layout
			mapViewFrameLayout.AddView (_mapView);
			//Get a reference to the seek bar in the view
			SeekBar floorSeekBar = _totalMapView.FindViewById<SeekBar>(Resource.Id.floor_seek_bar);
			//Bind an event handler to ProgressChanged
			floorSeekBar.ProgressChanged += OnMapSliderProgressChange;
		}

		public void OnMapSliderProgressChange (object sender, SeekBar.ProgressChangedEventArgs e)
		{
			FloorChanged(e.Progress);
		}

		public void FloorChanged(int newFloorIndex){
			Floor newFloor = _mapModel.Floors [newFloorIndex];
			if (newFloor != null)
				_mapModel.SetCurrentFloor(newFloor);
			_mapView.Update ();
		}

		public void BeaconFinderObserverUpdate (IBeaconFinderObservable observable)
		{
			BeaconFinder beaconFinder = (BeaconFinder)observable;
			EstimoteSdk.Beacon beacon = beaconFinder.GetClosestBeacon();

			if (beacon != null && (_mapModel.CurrentStoryline.HasBeacon(beacon)))
			{
				PointOfInterest poi = _mapModel.CurrentStoryline.FindPoi(beacon);

			    if (!poi.Visited)
			    {
			        //don't display a popup if the beacon has already been visited or if the poi is not on app's current floor
			        if (!ExposeumApplication.IsExplorerMode)
			        {
			            try
			            {
							_mapModel.CurrentStoryline.UpdateProgress(poi);

							if(poi.Floor != _mapModel.CurrentFloor)
								_mapModel.SetCurrentFloor(poi.Floor);
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
						if(poi.Floor != _mapModel.CurrentFloor)
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
			get { return this._mapModel; }
		}
	}
}
