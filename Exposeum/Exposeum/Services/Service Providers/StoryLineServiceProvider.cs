using System.Collections.Generic;
using Exposeum.Models;

namespace Exposeum.Services.Service_Providers
{
	public class StoryLineServiceProvider: IStoryLineService
	{
		private readonly Map _mapInstance;
		private readonly BeaconFinder _beaconFinder;

		public StoryLineServiceProvider(){
			_mapInstance = Map.GetInstance ();
			_beaconFinder = BeaconFinder.GetInstance ();
		}

		public List<StoryLine> GetStoryLines (){
			return _mapInstance.Storylines;
		}

		public StoryLine GetActiveStoryLine (){
			return _mapInstance.CurrentStoryline;
		}

		public void SetActiveStoryLine (StoryLine storyline){
			_mapInstance.CurrentStoryline = storyline;
			_mapInstance.SetActiveShortestPath (null);
			//update the beaconfinder with the new storyline
			_beaconFinder.SetPath(storyline);
		}

		public StoryLine GetStoryLineById(int id){
			//Loop through all the storylines and return the one
			//with the matching id
			foreach(StoryLine storyLine in GetStoryLines()){

				if (storyLine.StorylineId == id)
					return storyLine;

			}

			return null;
		}
	}
}

