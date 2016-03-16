using System;
using System.Collections.Generic;
using System.Linq;
using Exposeum.Models;
using QuickGraph;
using QuickGraph.Algorithms;

namespace Exposeum.Services.Service_Providers
{
    /// <summary>
    /// Service to return shortest path given a graph, a start node and end node.
    /// </summary>
	public class ShortestPathServiceProvider: IShortestPathService
    {

        private UndirectedGraph<MapElement, MapEdge> _graphInstance;
		private static ShortestPathServiceProvider _instance;

        private ShortestPathServiceProvider()
        {
			_graphInstance = new UndirectedGraph<MapElement, MapEdge> ();
        }


		private static ShortestPathServiceProvider GetInstance()
        {
			return _instance ?? (_instance = new ShortestPathServiceProvider());
        }


        //TODO: Need to actually put stuff in our graph
        private bool PopulateGraphFromDataSource()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of MapEdges representing the shortest path
        /// </summary>
        /// <param name="startElement"></param>
        /// <param name="targetElement"></param>
        /// <returns></returns>
        public IEnumerable<MapEdge> GetShortestPathEdgesList(MapElement startElement, MapElement targetElement)
        {

            // delegate with edge costs acquired from Edge distance property
            Func<MapEdge, double> edgeCost = e => e.Distance;

            // compute paths
			TryFunc<MapElement, IEnumerable<MapEdge>> tryGetPaths = _graphInstance.ShortestPathsDijkstra(edgeCost, startElement);

            // return path if found, null otherwise
            IEnumerable<MapEdge> path;
            return tryGetPaths(targetElement, out path) ? path : null;
        }

        /// <summary>
        /// Returns a list of MapElements representing the shortest path
        /// </summary>
        /// <param name="startElement"></param>
        /// <param name="targetElement"></param>
        /// <returns></returns>
        public IEnumerable<MapElement> GetShortestPathElementsList(MapElement startElement, MapElement targetElement)
        {

            var edgeList = GetShortestPathEdgesList(startElement, targetElement).ToList();
            List<MapElement> elementList = new List<MapElement>();

            foreach (MapEdge e in edgeList)
            {
                elementList.Add(e.Source);
                // if it's the last edge, also add the target node
                if (e == edgeList.Last())
                {
                    elementList.Add(e.Target);
                }
            }

            return elementList;
        }

    }
}