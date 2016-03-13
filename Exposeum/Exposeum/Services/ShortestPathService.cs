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
using QuickGraph.Algorithms;

namespace Exposeum.Services
{
    /// <summary>
    /// Service to return shortest path given a graph, a start node and end node.
    /// </summary>
    public static class ShortestPathService
    {
        public static IEnumerable<MapEdge> GetShortestPath(UndirectedGraph<MapElement, MapEdge> graph, MapElement startElement, MapElement targetElement)
        {
            // delegate with edge costs acquired from Edge distance property
            Func<MapEdge, double> edgeCost = e => e.Distance; 

            // compute paths
            TryFunc<MapElement, IEnumerable<MapEdge>> tryGetPaths = graph.ShortestPathsDijkstra(edgeCost, startElement);

            // return path if found, null otherwise
            IEnumerable<MapEdge> path;
            return tryGetPaths(targetElement, out path) ? path : null;
        }

    }
}