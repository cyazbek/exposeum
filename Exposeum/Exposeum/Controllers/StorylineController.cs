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
	public class StorylineController: IBeaconFinderObserver
    {
        private static StorylineController _storylineController;
		private readonly IStoryLineService _storyLineService;
		private readonly IShortestPathService _shortestPathService;
		private StoryLine _selectedStoryLine;
		private BeaconFinder _beaconFinder;
		private SearchingForBeaconFragment _searchingForBeaconFragment;

        public static StorylineController GetInstance()
        {
            if (_storylineController == null)
                _storylineController = new StorylineController();
            return _storylineController;
        }

		private StorylineController(){
			_storyLineService = ExposeumApplication.IoCContainer.Get<IStoryLineService>();
			_shortestPathService = ExposeumApplication.IoCContainer.Get<IShortestPathService> ();
			_beaconFinder = BeaconFinder.GetInstance ();
		}

		public StoryLineListAdapter GetStoryLinesListAdapter(Activity activity){
			return new StoryLineListAdapter(activity,GetStoryLines());
		}

        //Returns a List of all storylines available in the app.
        public List<StoryLine> GetStoryLines()
        {
            return _storyLineService.GetStoryLines();
        }

		public void SelectStoryLine(int storylinePosition){
			_selectedStoryLine = _storyLineService.GetStoryLines()[storylinePosition];
		}

		public void ShowSelectedStoryLineDialog(FragmentTransaction transaction, Context context){
			
				
				DialogFragment dialog;
				if(_selectedStoryLine.CurrentStatus==Status.InProgress)
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

		private void LocateUserOnGenericStoryLine(FragmentTransaction transaction){
			_beaconFinder.AddObserver(this);
			_beaconFinder.SetPath (_storyLineService.GetGenericStoryLine ());
			_beaconFinder.FindBeacons();
			DisplayLocatinUserFragment (transaction);

		}

		private void DisplayDirectToLastPointFragment(FragmentTransaction transaction){

			DialogFragment dialog = new DirectToLastPointFragment(DisplayLocatinUserFragment);
			dialog.Show (transaction, "Get Directions to the latest Point Of Interest visited");
		}

		private void DisplayLocatinUserFragment(FragmentTransaction transaction){
			_searchingForBeaconFragment = new SearchingForBeaconFragment();
			_searchingForBeaconFragment.Show (transaction, "Locating You");
		}

		private void KillLocatingUserFragment(){
			_searchingForBeaconFragment.Dismiss ();
		}

        public void ResumeStorylineBeacons()
        {
			_beaconFinder.FindBeacons();
        }

		public void SetActiveStoryLine(){
			_storyLineService.SetActiveStoryLine (_selectedStoryLine);
		}

        public void BeginJournery()
        {
            _selectedStoryLine.CurrentStatus = Status.InProgress;
        }

		public void BeaconFinderObserverUpdate(IBeaconFinderObservable observable){


			//check if found beacon is last poi of storyline


			//find the poi associated with the beacon
			PointOfInterest poi = _storyLineService.GetGenericStoryLine().FindPoi( ((BeaconFinder)observable).GetClosestBeacon() );
			//compute shortest path
			Path path = _shortestPathService.GetShortestPath(poi, _storyLineService.GetActiveStoryLine().GetLastVisitedPointOfInterest());
			//set the active shortest path
			_shortestPathService.SetActiveShortestPath(path);
			//deregister observer
			_beaconFinder.RemoveObserver(this);
			//kill the locating user fragment
			KillLocatingUserFragment();

		}
    }
}