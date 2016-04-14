using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Exposeum.Models;
using Java.Util;
using QuickGraph;
using Ninject;
using Exposeum.Mappers;

namespace Exposeum.Services.Service_Providers
{
    public class GraphServiceProvider : IGraphService
    {
        private UndirectedGraph<MapElement, MapEdge> _graphInstance;
		private MapEdgeMapper _mapEdgeMapper;

        public GraphServiceProvider()
        {
			_graphInstance = new UndirectedGraph<MapElement, MapEdge>();
			_mapEdgeMapper = MapEdgeMapper.GetInstance ();
            PopulateGraph();
        }

		public UndirectedGraph<MapElement, MapEdge> GetGraph()
		{
			return _graphInstance;
		}

        private void PopulateGraph()
        {
<<<<<<< HEAD
			
            List<MapEdge> mapEdges = new List<MapEdge>();
			List<MapElement> mapElements = ExposeumApplication.IoCContainer.Get<IStoryLineService>().GetActiveStoryLine().MapElements;

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

=======
			List<MapEdge> mapEdges = _mapEdgeMapper.GetAllMapEdges ();
>>>>>>> 34b00cc0eeb4bbfd26a95a7125712dd80fd175b3
            _graphInstance.AddVerticesAndEdgeRange(mapEdges);
            

        }
    }

}



   