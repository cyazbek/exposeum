using System;
using System.Collections.Generic;
using System.Linq;
using Exposeum.Models;
using QuickGraph;
using QuickGraph.Algorithms;
using Exposeum.Utilities;

namespace Exposeum.Services.Service_Providers
{
    /// <summary>
    /// Service to return shortest path given a graph, a start node and end node.
    /// </summary>
	public class ShortestPathServiceProvider: IShortestPathService
    {

        private UndirectedGraph<MapElement, MapEdge> _graphInstance;
		private static ShortestPathServiceProvider _instance;

		/// <summary>
		/// Constructor
		/// </summary>
        private ShortestPathServiceProvider()
        {
			_graphInstance = new UndirectedGraph<MapElement, MapEdge> ();
			PopulateGraphFromDataSource ();

        }

		/// <summary>
		/// Returns the Singleton instance of ShortestPathServiceProvider
		/// </summary>
		/// <returns>ShortestPathServiceProvider</returns>
		public static ShortestPathServiceProvider GetInstance()
        {
			return _instance ?? (_instance = new ShortestPathServiceProvider());
        }


        //TODO: Need to actually put stuff in our graph
		/// <summary>
		/// Populates the internal graph object with data
		/// </summary>
		/// <returns></returns>
        private void PopulateGraphFromDataSource()
        {
            List<MapEdge> mapEdges = new List<MapEdge> ();
			List<MapElement> mapElements = (new StoryLineServiceProvider ()).GetActiveStoryLine ().MapElements;

			MapElement previous = mapElements.Last();

			foreach (MapElement mapElement in mapElements) {
				
				mapEdges.Add( new MapEdge(previous, mapElement) );
				previous = mapElement;
			}

			_graphInstance.AddVerticesAndEdgeRange (mapEdges);
        }

        /// <summary>
        /// Returns a list of MapEdges representing the shortest path
        /// </summary>
        /// <param name="startElement"></param>
        /// <param name="targetElement"></param>
		/// <returns>IEnumerable<MapEdge></returns>
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
		/// <returns>IEnumerable<MapElement></returns>
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

		/// <summary>
		/// Returns a ShortPath object representing the shortest path. All PointsOfInterests contained in this path
		/// are clones of the original and have an updated description.
		/// </summary>
		/// <param name="startElement"></param>
		/// <param name="targetElement"></param>
		/// <returns>ShortPath</returns>
		public ShortPath GetShortestPath(MapElement startElement, MapElement targetElement)
		{
		    List<MapElement> mapElements = GetShortestPathElementsList(startElement, targetElement).ToList();
			List<MapElement> clonedMapElements = new List<MapElement> ();

			int i = 0;
			foreach (MapElement mapElement in mapElements) {
				MapElement clonedElement = DeepCloneUtility.Clone (mapElement);
				clonedElement.Visited = false;

				//If the Map Element is a PointOfInterest, change the descriptions 
				//and names so that the push notificationsremain relevant.
				if (clonedElement.GetType () == typeof(PointOfInterest)) {
					PointOfInterest poi = ((PointOfInterest)clonedElement);
					poi.DescriptionEn = "You're on your way, " + (mapElements.Count - i) + " point(s) of interest to go.";
					poi.DescriptionFr = "Il vous reste " + (mapElements.Count - i) + " point(s) d'interet avant d'arriver.";
					poi.NameEn = i + " out of " + mapElements.Count;
					poi.NameFr = i + " de " + mapElements.Count;
				}

				clonedMapElements.Add (clonedElement);

				i++;
			}

			return new ShortPath (clonedMapElements);
		}

    }
}