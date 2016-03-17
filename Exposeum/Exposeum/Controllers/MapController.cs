using Exposeum.Views;
using Exposeum.Models;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using Exposeum.Services;
using Exposeum.Services.Service_Providers;
using Exposeum.Utilities;

namespace Exposeum.Controllers
{
	public class MapController : IBeaconFinderObserver
	{
		private MapView _mapView;
		public View TotalMapView { get; private set;}
		private static MapController _instance;
		private MapProgressionFragment _mapProgressionView;
		private Context Context{get; set;}
		private Map _mapModel;
		private BeaconFinder _beaconFinder = BeaconFinder.GetInstance();
		private IShortestPathService _shortestPathService;


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

			_shortestPathService = new ShortestPathServiceProvider( GraphServiceProvider.GetInstance() );

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
			TotalMapView = li.Inflate (Resource.Layout.MapView, null);
			//Get a reference to the frame layout in the XML
			FrameLayout mapViewFrameLayout = (FrameLayout)TotalMapView.FindViewById (Resource.Id.map_view_frame_lay);
			//create the Mapview
			_mapView = new MapView (context, this);
			//Add a new MapView into the frame layout
			mapViewFrameLayout.AddView (_mapView);
			//Get a reference to the seek bar in the view
			SeekBar floorSeekBar = TotalMapView.FindViewById<SeekBar>(Resource.Id.floor_seek_bar);
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

			UpdatePointOfInterestAndStoryLineState (beacon);

			if (!ExposeumApplication.IsExplorerMode)
				_mapProgressionView.Update ();

			_mapView.Update ();
		}

		private void UpdatePointOfInterestAndStoryLineState(EstimoteSdk.Beacon beacon){
			
			if (beacon != null && (_mapModel.CurrentStoryline.HasBeacon(beacon)))
			{
				PointOfInterest poi = _mapModel.CurrentStoryline.FindPoi(beacon);

				//if POI is not visited
				if (!poi.Visited)
				{
					//Update the progress if in guided tour mode
					if (!ExposeumApplication.IsExplorerMode)
					{
						UpdateStoryLineProgress (poi);
					}
					else
					{
						//otherwise just update the state of the poi
						poi.SetVisited();

						//update the floor if the POI is located on a different floor than the one
						//currently displayed
						if(poi.Floor != _mapModel.CurrentFloor)
							_mapModel.SetCurrentFloor(poi.Floor);
						DisplayPopUp(poi);
					}
				}

			}
		}

		private void UpdateStoryLineProgress (PointOfInterest poi){
			StoryLine currentStoryLine = _mapModel.CurrentStoryline;

			try
			{
				//update the storyline progress
				currentStoryLine.UpdateProgress(poi);


				//update the floor if the POI is located on a different floor than the one
				//currently displayed
				if(poi.Floor != _mapModel.CurrentFloor)
					_mapModel.SetCurrentFloor(poi.Floor);
				
				DisplayPopUp(poi);

				//If the storyline is complete, we show path to the starting point
				if(currentStoryLine.CurrentStatus == Status.IsVisited)
					GoingBackToTheStart(currentStoryLine);
			}
			catch (PointOfInterestNotVisitedException e)
			{
				DisplayOutOfOrderPointOfInterestPopup(e.Poi);
			}
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

		private void GoingBackToTheStart(StoryLine storyline){
			Path path = GetShortestPathToStart (storyline);
			_mapModel.SetActiveShortestPath (path);
		}

		public Path GetShortestPathToStart(StoryLine storyline){
			MapElement start = storyline.MapElements.Last ();
			MapElement end = storyline.MapElements.First ();

            
			return _shortestPathService.GetShortestPath (start, end);
		}

		public Map Model
		{
			get { return _mapModel; }
		}
	}
}
