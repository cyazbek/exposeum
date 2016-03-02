using Exposeum.Models;

namespace Exposeum.Controllers
{
    public class StorylineController
    {
        public StoryLine CurrentStoryLine { get; set; }

		//here we need a method to supply to the view a list of storyline so that it can generate the visual list.
		//public sometype getStorylines(){}

        public void CreateTempStoryline ()
		{
			//currently, the dummy seed data for the current storyline is in Map.cs seedData() method.
        }

		//sample method that will be called when a user selects a storyline in the view
		public void initStoryLine(/*some unique identifier that allows me to retreive the storyline from my lists of sotylines*/){
			//I init the storyline and pass it somewhere either to a global state or to the map controller (but I don't know if the two controller should be coupled this way)
		}
    }
}