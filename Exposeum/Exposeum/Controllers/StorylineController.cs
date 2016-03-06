using Exposeum.Models;

namespace Exposeum.Controllers
{
    public class StorylineController
    {
        public StoryLine CurrentStoryLine { get; set; }

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

            storyLine = null;
        }
    }
}