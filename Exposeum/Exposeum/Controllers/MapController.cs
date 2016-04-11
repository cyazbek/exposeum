using System.Collections.Generic;
using Exposeum.Views;
using Exposeum.Models;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Linq;
using Exposeum.Exceptions;
using Exposeum.Services;
using Exposeum.Services.Service_Providers;
using Ninject;
using System.Threading;

namespace Exposeum.Controllers
{
    public class MapController : IBeaconFinderObserver
    {
        private MapView _mapView;
        public View TotalMapView { get; private set; }
        private static MapController _instance;
        private readonly MapProgressionFragment _mapProgressionView;
        private Context Context { get; set; }
        private readonly Map _mapModel;
        private readonly BeaconFinder _beaconFinder = BeaconFinder.GetInstance();
        private readonly IShortestPathService _shortestPathService;


        public static MapController GetInstance()
        {
            return _instance;

        }

        public static MapController GetInstance(Context context)
        {
            if (_instance == null)
                _instance = new MapController(context);
            return _instance;
        }

        public void Destroy()
        {
            //remove the current singleton from the list of observers
            _beaconFinder.RemoveObserver(_instance);
            _instance = null;
        }

        private MapController(Context context)
        {

            ConfigureMapView(context);

			_shortestPathService = ExposeumApplication.IoCContainer.Get<IShortestPathService>();

            _mapModel = Map.GetInstance();

            _beaconFinder.AddObserver(this);

            //If we are not in free explorer mode (ie there exists a current storyline) then add the
            //current storyline progression fragment to the map activity
            if (!ExposeumApplication.IsExplorerMode)
            {

                // Create a new fragment and a transaction.
                FragmentTransaction fragmentTx = ((Activity)_mapView.Context).FragmentManager.BeginTransaction();
                _mapProgressionView = new MapProgressionFragment(_mapModel.CurrentStoryline);

                fragmentTx.Add(Resource.Id.map_frag_frame_lay, _mapProgressionView);
                fragmentTx.Commit();

            }

            _beaconFinder.FindBeacons();
        }

        private void ConfigureMapView(Context context)
        {
            LayoutInflater li = LayoutInflater.FromContext(context);
            //Load the MapView XML
            TotalMapView = li.Inflate(Resource.Layout.MapView, null);
            //Get a reference to the frame layout in the XML
            FrameLayout mapViewFrameLayout = (FrameLayout)TotalMapView.FindViewById(Resource.Id.map_view_frame_lay);
            //create the Mapview
            _mapView = new MapView(context, this);
            //Add a new MapView into the frame layout
            mapViewFrameLayout.AddView(_mapView);
            //Get a reference to the seek bar in the view
            SeekBar floorSeekBar = TotalMapView.FindViewById<SeekBar>(Resource.Id.floor_seek_bar);
            //Bind an event handler to ProgressChanged
            floorSeekBar.ProgressChanged += OnMapSliderProgressChange;
        }

        public void OnMapSliderProgressChange(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            FloorChanged(e.Progress);
        }

        public void FloorChanged(int newFloorIndex)
        {
            Floor newFloor = _mapModel.Floors[newFloorIndex];
            if (newFloor != null)
                _mapModel.CurrentFloor = newFloor;
            _mapView.Update();
        }

        /// <summary>
        /// Method implemented from IBeaconFinderObserver. This Method is called by the BeaconFinder to update the
        /// observer (this)
        /// </summary>
        /// <param name="observable">The Observable implementation of IBeaconFinderObservable</param>
        /// <returns></returns>
        public void BeaconFinderObserverUpdate(IBeaconFinderObservable observable)
        {
            BeaconFinder beaconFinder = (BeaconFinder)observable;
            EstimoteSdk.Beacon beacon = beaconFinder.GetClosestBeacon();

            //if we don't have a shortest path in memory and (we are in explorer mode or the storyline is not done), update the main beacons & storyline
            if (_mapModel.GetActiveShortestPath() == null)
                UpdatePointOfInterestAndStoryLineState(beacon);
            else {
                if (_mapModel.GetActiveShortestPath().Status != Status.IsVisited)
                    UpdatePointOfInterestAnShortestPathState(beacon);
            }

            if (!ExposeumApplication.IsExplorerMode)
                _mapProgressionView.Update();

            _mapView.Update();
        }

        /// <summary>
        /// This method will update the state of the POI associated with the beacon and 
        /// the associated StoryLine (if any)
        /// </summary>
        /// <param name="beacon"></param>
        /// <returns></returns>
        private void UpdatePointOfInterestAndStoryLineState(EstimoteSdk.Beacon beacon)
        {

            StoryLine storyline = _mapModel.CurrentStoryline;

            if (beacon != null && (storyline.HasBeacon(beacon)))
            {
                PointOfInterest poi = storyline.FindPoi(beacon);

                //if POI is not visited
                if (!poi.Visited)
                {
                    //Update the progress if in guided tour mode
                    if (!ExposeumApplication.IsExplorerMode)
                    {
                        UpdateStoryLineProgress(poi);
                    }
                    else
                    {
                        //otherwise just update the state of the poi
                        poi.SetVisited(true); 
                        UpdateFloor(poi);
                        DisplayPopUp(poi);
                    }
                }

            }
        }

        /// <summary>
        /// This method will update the state of the current storyline based on the given POI
        /// </summary>
        /// <param name="poi">StoryLine POI</param>
        /// <returns></returns>
        private void UpdateStoryLineProgress(PointOfInterest poi)
        {
            StoryLine currentStoryLine = _mapModel.CurrentStoryline;

            try
            {
                //update the storyline progress
                currentStoryLine.UpdateProgress(poi);
                UpdateFloor(poi);
                DisplayPopUp(poi);

            }
            catch (PointOfInterestNotVisitedException e)
            {
                DisplayOutOfOrderPointOfInterestPopup(poi, e.UnvistedMapElements);
            }
        }


        /// <summary>
        /// This method will update the state of the POI associated with the beacon and 
        /// the associated Path
        /// </summary>
        /// <param name="beacon"></param>
        /// <returns></returns>
        private void UpdatePointOfInterestAnShortestPathState(EstimoteSdk.Beacon beacon)
        {
            Path path = _mapModel.GetActiveShortestPath();
            if (beacon != null && (path.HasBeacon(beacon)))
            {
                PointOfInterest poi = path.FindPoi(beacon);

                //if POI is not visited
                if (!poi.Visited)
                {
                    UpdateShortestPathProgress(poi);
                }
            }
        }

        /// <summary>
        /// This method will update the state of the current ShortestPath based on the given POI
        /// </summary>
        /// <param name="poi">Path POI</param>
        /// <returns></returns>
        private void UpdateShortestPathProgress(PointOfInterest poi)
        {
            //update the storyline progress
            _mapModel.GetActiveShortestPath().UpdateProgress(poi);
            UpdateFloor(poi);

			//If the shortest path is visited it should be set to null and the storyline 
			//should be restored on the beacon finder
			if (_mapModel.GetActiveShortestPath ().Status == Status.IsVisited){
				_mapModel.SetActiveShortestPath (null);
				_mapView.Update();
				_beaconFinder.SetPath (_mapModel.CurrentStoryline);

			}
        }

        /// <summary>
        /// This method will update the floor current floor of the map if it is 
        /// not the same as the floor of the supplied POI
        /// </summary>
        /// <param name="poi"></param>
        /// <returns></returns>
        private void UpdateFloor(PointOfInterest poi)
        {
            //update the floor if the POI is located on a different floor than the one
            //currently displayed
            if (poi.Floor != _mapModel.CurrentFloor)
                _mapModel.CurrentFloor = poi.Floor;
        }

        /// <summary>
        /// Method to display informational toast to Visitors when out of sequence POI visited in storyline
        /// </summary>
		private void DisplayOutOfOrderPointOfInterestPopup(PointOfInterest currentPoi, IEnumerable<MapElement> skippedMapElements)
        {
			_mapView.InitiateOutOfOrderPointOfInterestPopup(currentPoi, skippedMapElements, SkipOutOfOrderPOI, GoingBackToLastPoint);
        }

		private void SkipOutOfOrderPOI(PointOfInterest currentPoi, IEnumerable<MapElement> skippedMapElements){

			foreach (var mapElement in skippedMapElements)
		    {
				mapElement.SetVisited(true);
		    }

            _mapView.Update();

            if (!ExposeumApplication.IsExplorerMode)
                _mapProgressionView.Update();
                    
		}

        private void DoNotSkipOutOfOrderPOI(PointOfInterest currentPoi)
        {
            
        }

        /// <summary>
        /// This method will update display a popup in the view with contextual information about the supplied POI
        /// </summary>
        /// <param name="selectedPoi"></param>
        /// <returns></returns>
        public void DisplayPopUp(PointOfInterest selectedPoi)
        {
            //Set the callback to execute after user dismisses popup
            PointOfInterestPopup.DismissCallback callback = null;
            if (_mapModel.CurrentStoryline.Status == Status.IsVisited)
                callback = DisplayEndOfStoryLinePopUp;

            _mapView.InitiatePointOfInterestPopup(selectedPoi, callback);
            _mapView.Update(); //technically unncecessary but included for completeness

            if (!ExposeumApplication.IsExplorerMode)
                _mapProgressionView.Update();
        }

        public void DisplayEndOfStoryLinePopUp()
        {
            _mapView.InitiateEndOfStoryLinePopup(EndOfStoryLinePopupCallback);
        }

        private void EndOfStoryLinePopupCallback(bool directionsToStart)
        {
            if (directionsToStart)
                GoingBackToTheStart(_mapModel.CurrentStoryline);
        }

        /// <summary>
        /// This method initiate the process of going back to the starting point of a storyline 
        /// through the shortest path
        /// </summary>
        /// <param name="storyline"></param>
        /// <returns></returns>
        private void GoingBackToTheStart(StoryLine storyline)
        {
            Path path = GetShortestPathToStart(storyline);
            _mapModel.SetActiveShortestPath(path);
            _beaconFinder.SetPath(path);
			_mapView.Update();
        }

        /// <summary>
        /// Given a StoryLine, this method will find the shortest path from the end 
        /// to the start if the StoryLine 
        /// </summary>
        /// <param name="storyline"></param>
        /// <returns>Path</returns>
        public Path GetShortestPathToStart(StoryLine storyline)
        {
            MapElement start = storyline.MapElements.Last();
            MapElement end = storyline.MapElements.First();


            return _shortestPathService.GetShortestPath(start, end);
        }

        /// <summary>
        /// Display path to last visited beacon
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        private void GoingBackToLastPoint(MapElement start)
        {
            Path path = GetShortestPathToLastPoint(start);
            _mapModel.SetActiveShortestPath(path);
            _beaconFinder.SetPath(path);
            _mapView.Update();
        }

        /// <summary>
        /// Get shortest path from incoming POI to last visited
        /// </summary>
        /// <param name="start"></param>
        /// <returns>Path</returns>
        public Path GetShortestPathToLastPoint(MapElement start)
        {
            
			MapElement end = _mapModel.CurrentStoryline.LastPointOfInterestVisited;

            if (end == null)
            {
                end = _mapModel.CurrentStoryline.MapElements.First();
            }

            return _shortestPathService.GetShortestPath(start, end);
        }

        public Map Model
        {
            get { return _mapModel; }
        }

		public void MapViewUpdate(){
			_mapView.Update();
		}
    }
}
