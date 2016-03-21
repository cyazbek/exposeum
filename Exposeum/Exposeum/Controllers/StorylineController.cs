using System.Collections.Generic;
using Exposeum.Models;
using Android.App;
using Android.Content;
using Exposeum.Menu_Bar;
using Exposeum.Services;
using Exposeum.Services.Service_Providers;

namespace Exposeum.Controllers
{
    public class StorylineController
    {
        private static StorylineController _storylineController;
		private readonly IStoryLineService _storyLineService;
		private StoryLine _selectedStoryLine;

        public static StorylineController GetInstance()
        {
            if (_storylineController == null)
                _storylineController = new StorylineController();
            return _storylineController;
        }

		private StorylineController(){
			_storyLineService = new StoryLineServiceProvider ();
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
            BeaconFinder.GetInstance().StopRanging();
        }

        public void ResumeStorylineBeacons()
        {
            BeaconFinder.GetInstance().FindBeacons();
        }

		public void SetActiveStoryLine(){
			_storyLineService.SetActiveStoryLine (_selectedStoryLine);
		}

        public void BeginJournery()
        {
            _selectedStoryLine.CurrentStatus = Status.InProgress;
        }
    }
}