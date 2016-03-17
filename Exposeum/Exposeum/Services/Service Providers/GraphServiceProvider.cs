using System.Collections.Generic;
using System.Linq;
using Exposeum.Models;
using QuickGraph;

namespace Exposeum.Services.Service_Providers
{
    public class GraphServiceProvider : IGraphService
    {
		private static GraphServiceProvider _instance;
        private UndirectedGraph<MapElement, MapEdge> _graphInstance;

        private GraphServiceProvider()
        {
            PopulateGraph();
        }


		public static GraphServiceProvider GetInstance()
        {
			if(_instance == null)
				_instance = new GraphServiceProvider();
			return _instance;
        }

		public UndirectedGraph<MapElement, MapEdge> GetGraph()
		{
			return _graphInstance;
		}

        private void PopulateGraph()
        {
            _graphInstance = new UndirectedGraph<MapElement, MapEdge>();
            List<MapEdge> mapEdges = new List<MapEdge>();
            List<MapElement> mapElements = (new StoryLineServiceProvider()).GetActiveStoryLine().MapElements;

            MapElement previous = mapElements.Last();

            foreach (MapElement mapElement in mapElements)
            {

                mapEdges.Add(new MapEdge(previous, mapElement));
                previous = mapElement;
            }

            _graphInstance.AddVerticesAndEdgeRange(mapEdges);
        }
    }

}



   