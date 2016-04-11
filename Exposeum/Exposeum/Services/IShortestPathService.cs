using System.Collections.Generic;
using Exposeum.Models;

namespace Exposeum.Services
{
	public interface IShortestPathService
	{
	    /// <summary>
	    /// Returns a list of MapEdges representing the shortest path
	    /// </summary>
	    /// <param name="startElement"></param>
	    /// <param name="targetElement"></param>
	    /// <returns></returns>
	    IEnumerable<MapEdge> GetShortestPathEdgesList(MapElement startElement, MapElement targetElement);

	    /// <summary>
	    /// Returns a list of MapElements representing the shortest path
	    /// </summary>
	    /// <param name="startElement"></param>
	    /// <param name="targetElement"></param>
	    /// <returns></returns>
	    IEnumerable<MapElement> GetShortestPathElementsList(MapElement startElement, MapElement targetElement);

	    /// <summary>
	    /// Returns a ShortPath object representing the shortest path
	    /// </summary>
	    /// <param name="startElement"></param>
	    /// <param name="targetElement"></param>
	    /// <returns>Path</returns>
	    Path GetShortestPath(MapElement startElement, MapElement targetElement);

		/// <summary>
		/// Returns the active shortest Path object
		/// </summary>
		/// <returns>Path</returns>
		Path GetActiveShortestPath ();

		/// <summary>
		/// Sets the active shortest path
		/// </summary>
		/// <param name="path"></param>
		void SetActiveShortestPath (Path path);
	}
}

