using Exposeum.Models;
using Android.App;

namespace Exposeum.Controllers
{
    public static class StoryLineController
    {

		private static Map map = MapService.GetMapInstance();

		public StoryLineListAdapter GetStoryLines(Activity activity){
			return new StoryLineListAdapter(activity, map.getStoryLineList);
		}


    }
}