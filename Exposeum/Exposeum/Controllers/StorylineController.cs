using Exposeum.Models;
using Android.App;
using Android.Content;

namespace Exposeum.Controllers
{
    public class StorylineController
    {
        private static StorylineController _storylineController;
		private static Map map = StoryLineService.GetMapInstance();
		private static StoryLine selectedStoryLine;

        public static StorylineController GetInstance()
        {
            if (_storylineController == null)
                _storylineController = new StorylineController();
            return _storylineController;
        }

		public StoryLineListAdapter GetStoryLines(Activity activity){
			return new StoryLineListAdapter(activity, map.getStoryLineList);
		}

		public void SelectStoryLine(int storylinePosition){
			selectedStoryLine = map.getStoryLineList[storylinePosition];
		}

		public void ShowSelectedStoryLineDialog(FragmentTransaction transaction, Context context){
			
				
				DialogFragment dialog;
				if(selectedStoryLine.currentStatus==Status.inProgress)
				{
					dialog = new DialogStorylineInProgress(selectedStoryLine, context);
				}
				else
				{
					dialog = new DialogStoryline(selectedStoryLine, context);
				}

				dialog.Show(transaction, "Story Line title");
		}
			
        public void ResetStorylineProgress(StoryLine storyLine)
        {
            foreach (var poi in storyLine.poiList)
            {
                poi.Visited = false;
            }

            storyLine.SetLastPointOfInterestVisited(null);
            storyLine.currentStatus = Status.isNew;

        }

        public void PauseStorylineBeacons()
        {
            BeaconFinder.getInstance().stopRanging();
        }

        public void ResumeStorylineBeacons()
        {
            BeaconFinder.getInstance().findBeacons();
        }

		public void SetActiveStoryLine(){
			map.CurrentStoryline = selectedStoryLine;
            BeaconFinder.getInstance().setStoryLine(selectedStoryLine);
		}
    }
}