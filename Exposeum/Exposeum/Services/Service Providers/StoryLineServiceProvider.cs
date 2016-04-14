using System.Collections.Generic;
using Exposeum.Models;
using Java.Util;
using QuickGraph;
using Ninject;
using Android.Graphics.Drawables;

namespace Exposeum.Services.Service_Providers
{
	public class StoryLineServiceProvider: IStoryLineService
	{
		private readonly Map _mapInstance;
		private readonly BeaconFinder _beaconFinder;
		private UndirectedGraph<MapElement, MapEdge> _graphInstance;
		private StoryLine _genericStoryLine;

		public StoryLineServiceProvider(){
			_mapInstance = Map.GetInstance ();
			_beaconFinder = BeaconFinder.GetInstance ();
			_graphInstance = ExposeumApplication.IoCContainer.Get<IGraphService>().GetGraph();
			BuildGenericStoryLine ();
		}

		private void BuildGenericStoryLine(){
			//build a generic storyline from all the edges of the graph

			_genericStoryLine = new StoryLine
			{
				StorylineId = -1,
				ImgPath = "",
				Duration = 0,
				FloorsCovered = 4,
				Status = Status.IsNew,
			};

			foreach(MapElement vertice in _graphInstance.Vertices){
				_genericStoryLine.AddMapElement (vertice);
			}
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

		//TODO: implement properly
		public StoryLine GetGenericStoryLine(){
			return _genericStoryLine;
		}
	}
}

