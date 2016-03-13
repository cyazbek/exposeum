using System;
using Exposeum.Models;
using System.Collections.Generic;

namespace Exposeum
{
	public class StoryLineServiceProvider: IStoryLineService
	{
		private static IStoryLineService _instance;
		private Map _mapInstance;
		private BeaconFinder _beaconFinder;

		private StoryLineServiceProvider(){
			_mapInstance = Map.GetInstance ();
			_beaconFinder = BeaconFinder.GetInstance ();
		}

		public static IStoryLineService GetInstance(){
			if (_instance == null)
				_instance = new StoryLineServiceProvider();

			return _instance;
		}

		public List<StoryLine> GetStoryLines (){
			return _mapInstance.GetStoryLineList;
		}

		public StoryLine GetActiveStoryLine (){
			return _mapInstance.CurrentStoryline;
		}

		public void SetActiveStoryLine (StoryLine storyline){
			_mapInstance.CurrentStoryline = storyline;
			//update the beaconfinder with the new storyline
			_beaconFinder.SetStoryLine(storyline);
		}

		public StoryLine GetStoryLineById(int id){
			//Loop through all the storylines and return the one
			//with the matching id
			foreach(StoryLine storyLine in GetStoryLines()){

				if (storyLine.Id == id)
					return storyLine;

			}

			return null;
		}
	}
}

