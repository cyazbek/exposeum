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



   