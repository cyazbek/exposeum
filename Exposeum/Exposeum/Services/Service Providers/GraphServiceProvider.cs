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

			//add some waypoint betweem the start and the end 0.3306878,0.5831111
			WayPoint pot1 = new WayPoint(0.330687f, 0.5831111f, mapElements.Last().Floor);
			WayPoint pot2 = new WayPoint(0.2740971f,0.6113333f, mapElements.Last().Floor);

			MapElement previous = pot2;

            foreach (MapElement mapElement in mapElements)
            {

                mapEdges.Add(new MapEdge(previous, mapElement));
                previous = mapElement;
            }

			mapEdges.Add(new MapEdge(previous, pot1));
			mapEdges.Add(new MapEdge(pot1, pot2));

            _graphInstance.AddVerticesAndEdgeRange(mapEdges);
        }
    }

}



   