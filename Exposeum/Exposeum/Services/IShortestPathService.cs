using Exposeum.Models;
using System.Collections.Generic;

namespace Exposeum
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
	    /// <returns>ShortPath</returns>
	    Path GetShortestPath(MapElement startElement, MapElement targetElement);
	}
}

