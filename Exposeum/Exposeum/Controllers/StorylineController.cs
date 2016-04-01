using System.Collections.Generic;
using Exposeum.Models;
using Android.App;
using Android.Content;
using Exposeum.Menu_Bar;
using Exposeum.Fragments;
using Exposeum.Services;
using Exposeum.Services.Service_Providers;
using Ninject;

namespace Exposeum.Controllers
{
    public class StorylineController : IBeaconFinderObserver
    {
        private static StorylineController _storylineController;
        private readonly IStoryLineService _storyLineService;
        private readonly IShortestPathService _shortestPathService;
        private StoryLine _selectedStoryLine;
        private BeaconFinder _beaconFinder;
        private SearchingForBeaconFragment _searchingForBeaconFragment;
        private FragmentTransaction _directToLastPointTransaction;
        private PointOfInterest _discoveredPoi;
		private Context _context;

        public static StorylineController GetInstance()
        {
            if (_storylineController == null)
                _storylineController = new StorylineController();
            return _storylineController;
        }

        private StorylineController()
        {
            _storyLineService = ExposeumApplication.IoCContainer.Get<IStoryLineService>();
            _shortestPathService = ExposeumApplication.IoCContainer.Get<IShortestPathService>();
            _beaconFinder = BeaconFinder.GetInstance();
        }

        public StoryLineListAdapter GetStoryLinesListAdapter(Activity activity)
        {
            return new StoryLineListAdapter(activity, GetStoryLines());
        }

        //Returns a List of all storylines available in the app.
        public List<StoryLine> GetStoryLines()
        {
            return _storyLineService.GetStoryLines();
        }

        public void SelectStoryLine(int storylinePosition)
        {
            _selectedStoryLine = _storyLineService.GetStoryLines()[storylinePosition];
        }

        public void ShowSelectedStoryLineDialog(FragmentTransaction transaction, Context context)
        {
            DialogFragment dialog;
            if (_selectedStoryLine.CurrentStatus == Status.InProgress)
            {
                dialog = new DialogStorylineInProgress(_selectedStoryLine, context, LocateUserOnGenericStoryLine);
            }
            else
            {
                dialog = new DialogStoryline(_selectedStoryLine, context);
            }

            dialog.Show(transaction, "Story Line title");
        }

        public void ShowPauseStoryLineDialog(FragmentTransaction transaction, Context context)
        {
            DialogFragment dialog = new DialogPauseStorylineConfirmation(_selectedStoryLine, context);

            dialog.Show(transaction, "Story Line title");
        }

        public void ResetStorylineProgress(StoryLine storyLine)
        {
            foreach (var mapElement in storyLine.MapElements)
            {
                mapElement.Visited = false;
            }

            storyLine.SetLastPointOfInterestVisited(null);
            storyLine.CurrentStatus = Status.IsNew;

        }

        public void PauseStorylineBeacons()
        {
            _beaconFinder.StopRanging();
        }

        private void LocateUserOnGenericStoryLine(FragmentTransaction transaction)
        {
            _directToLastPointTransaction = transaction;
            _beaconFinder.AddObserver(this);
            _beaconFinder.SetPath(_storyLineService.GetGenericStoryLine());
            _beaconFinder.FindBeacons();
        }

		private void DisplayDirectToLastPointFragment()
        {
			using (FragmentTransaction tr = ((Activity) _context).FragmentManager.BeginTransaction())
			{
				DialogFragment dialog = new DirectToLastPointFragment(FindAndSetShortestPathToLastVisitedPointOfInterest);
				dialog.Show(tr, "Story Line title");
			}
        }


        public void ResumeStoryLine()
        {
			_beaconFinder.SetPath(_storyLineService.GetActiveStoryLine());
            _beaconFinder.FindBeacons();
        }

        public void SetActiveStoryLine()
        {
            _storyLineService.SetActiveStoryLine(_selectedStoryLine);
        }

        public void BeginJournery()
        {
            _selectedStoryLine.CurrentStatus = Status.InProgress;
        }

        public void BeaconFinderObserverUpdate(IBeaconFinderObservable observable)
        {

            BeaconFinder beaconFinder = observable as BeaconFinder;
            EstimoteSdk.Beacon foundEstimoteBeacon = beaconFinder.GetClosestBeacon();
            Beacon foundExposeumBeacon = new Beacon(foundEstimoteBeacon);

            if (_storyLineService.GetGenericStoryLine().HasBeacon((foundEstimoteBeacon)))
            {
                _discoveredPoi =
                    _storyLineService.GetGenericStoryLine().FindPoi(((BeaconFinder) observable).GetClosestBeacon());
            }

            PointOfInterest lastVisited = _storyLineService.GetActiveStoryLine().GetLastVisitedPointOfInterest();
			int lastVisitedIndex = _storyLineService.GetActiveStoryLine().PoiList.LastIndexOf(lastVisited);
			PointOfInterest nextToVisit = _storyLineService.GetActiveStoryLine().PoiList[lastVisitedIndex+1];

			//if the found beacon does not correspond to the last visited poi or the next poi in the list that is to be visited,
			//ask if the user wants to be redirected. Otherwise, just resume the storyline normally
			if (!_storyLineService.GetActiveStoryLine().HasBeacon(foundEstimoteBeacon))
            {
                DisplayDirectToLastPointFragment();
            }
            else
            {
				ResumeStoryLine ();
            }

            //deregister observer
            _beaconFinder.RemoveObserver(this);
        }

        private void FindAndSetShortestPathToLastVisitedPointOfInterest()
        {
            //compute shortest path
            Path path = _shortestPathService.GetShortestPath(_discoveredPoi,
                _storyLineService.GetActiveStoryLine().GetLastVisitedPointOfInterest());
            //set the active shortest path
            _shortestPathService.SetActiveShortestPath(path);

        }

		public void SetContext(Context context){
			_context = context;
		}
    }
}