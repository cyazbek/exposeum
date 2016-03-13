using Exposeum.Models;
using Android.App;
using Android.Content;

namespace Exposeum.Controllers
{
    public class StorylineController
    {
        private static StorylineController _storylineController;
		private Map _map = Map.GetInstance();
		public StoryLine _selectedStoryLine { get; set; }
            
        
        

        public static StorylineController GetInstance()
        {
            if (_storylineController == null)
                _storylineController = new StorylineController();
            return _storylineController;
        }

		public StoryLineListAdapter GetStoryLines(Activity activity){
			return new StoryLineListAdapter(activity, _map.GetStoryLineList);
		}

		public void SelectStoryLine(int storylinePosition){
			_selectedStoryLine = _map.GetStoryLineList[storylinePosition];
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
			
        public void ResetStorylineProgress()
        {
			foreach (var mapElement in _selectedStoryLine.MapElements)
            {
				mapElement.Visited = false;
            }

            _selectedStoryLine.SetLastPointOfInterestVisited(null);
            _selectedStoryLine.CurrentStatus = Status.IsNew;
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
			_map.CurrentStoryline = _selectedStoryLine;
            BeaconFinder.GetInstance().SetStoryLine(_selectedStoryLine);
		}
    }
}