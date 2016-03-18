using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Exposeum.Models;
using QuickGraph;

namespace Exposeum.Services
{
    public class GraphService : IGraphService
    {
		private static GraphService _instance;
        private UndirectedGraph<MapElement, MapEdge> _graphInstance;

        private GraphService()
        {
            PopulateGraph();
        }


		public static GraphService GetInstance()
        {
			if(_instance == null)
				_instance = new GraphService();
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
			PointOfTravel pot1 = new PointOfTravel(0.330687f, 0.5831111f, mapElements.Last().Floor);
			PointOfTravel pot2 = new PointOfTravel(0.2740971f,0.6113333f, mapElements.Last().Floor);

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



   