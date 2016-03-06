using Exposeum.Models;

namespace Exposeum.Controllers
{
    public class StorylineController
    {
        private static StorylineController _storylineController;

        public static StorylineController GetInstance()
        {
            if (_storylineController == null)
                _storylineController = new StorylineController();

            return _storylineController;
        }

        public StoryLine CurrentStoryLine { get; set; }

		//here we need a method to supply to the view a list of storyline so that it can generate the visual list.
		//public sometype getStorylines(){}

        public void CreateTempStoryline ()
		{
			//currently, the dummy seed data for the current storyline is in Map.cs seedData() method.
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
    }
}