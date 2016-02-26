using Exposeum.Models;

namespace Exposeum.Controllers
{
    public class StorylineController
    {
        public StoryLine CurrentStoryLine { get; set; }

        public void CreateTempStoryline ()
        {
            //how it should be when create/parsing the JSON file and setting the data up.
            Floor floor1 = new Floor(Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_1));
            PointOfInterest poi1 = new PointOfInterest(0.53f, 0.46f, floor1);
            PointOfInterestDescription description1 = new PointOfInterestDescription("The First :: Title"
                    ,"A Summary about the first :: summary","A Full Description about the first :: Description");
            CurrentStoryLine.addPoi(poi1);
        }
    }
}