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
					dialog = new DialogStorylineInProgress(_selectedStoryLine, context);
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

		private void LocateUserOnGenericStoryLine(){
			_beaconFinder.AddObserver(this);
			_beaconFinder.SetPath (_storyLineService.GetGenericStoryLine ());
			_beaconFinder.FindBeacons();
			displayLocatinUserFragment ();

		}

		}

		private void killLocatingUserFragment(){
		
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


			//find the poi associated with the beacon
			PointOfInterest poi = _storyLineService.GetGenericStoryLine().FindPoi( ((BeaconFinder)observable).GetClosestBeacon() );
			//compute shortest path
			Path path = _shortestPathService.GetShortestPath(poi, _storyLineService.GetActiveStoryLine().GetLastVisitedPointOfInterest());
			//set the active shortest path
			_shortestPathService.SetActiveShortestPath(path);
			//deregister observer
			_beaconFinder.RemoveObserver(this);
			//kill the locating user fragment
			killLocatingUserFragment();

		}
    }
}