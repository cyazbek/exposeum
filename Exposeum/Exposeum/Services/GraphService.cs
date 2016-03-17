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

        private static UndirectedGraph<MapElement, MapEdge> _graphInstance;

        public GraphService()
        {
            PopulateGraph();
        }

        public UndirectedGraph<MapElement, MapEdge> GetGraph()
        {
            return _graphInstance ?? (_graphInstance = new UndirectedGraph<MapElement, MapEdge>());
        }

        private void PopulateGraph()
        {
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



   