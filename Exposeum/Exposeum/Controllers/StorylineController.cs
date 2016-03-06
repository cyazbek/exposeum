using Exposeum.Models;
using Android.App;

namespace Exposeum.Controllers
{
    public static class StoryLineController
    {

		private static Map map = MapService.GetMapInstance();
		private static StoryLine selectedStoryLine;

		public static StoryLineListAdapter GetStoryLines(Activity activity){
			return new StoryLineListAdapter(activity, map.getStoryLineList);
		}

		public static void SelectStoryLine(int storylinePosition){
			selectedStoryLine = map.getStoryLineList[storylinePosition];
		}

		public static void ShowSelectedStoryLineDialog(FragmentTransaction transaction){
			
				
				DialogFragment dialog;
				if(selectedStoryLine.currentStatus==Status.inProgress)
				{
					dialog = new DialogStorylineInProgress(selectedStoryLine);
				}
				else
				{
					dialog = new DialogStoryline(selectedStoryLine);
				}

				dialog.Show(transaction, "Story Line title");
		}

		public static void SetActiveStoryLine(){
			map.CurrentStoryline = selectedStoryLine;
		}
    }
}