using System;
using Exposeum.Models;
using System.Collections.Generic;
using Exposeum.Services;

namespace Exposeum
{
	public interface IShortestPathService
	{
	    /// <summary>
	    /// Returns a list of MapEdges representing the shortest path
	    /// </summary>
	    /// <param name="startElement"></param>
	    /// <param name="targetElement"></param>
	    /// <param name="graphService"></param>
	    /// <returns></returns>
	    IEnumerable<MapEdge> GetShortestPathEdgesList(MapElement startElement, MapElement targetElement);

	    /// <summary>
	    /// Returns a list of MapElements representing the shortest path
	    /// </summary>
	    /// <param name="startElement"></param>
	    /// <param name="targetElement"></param>
	    /// <param name="graphService"></param>
	    /// <returns></returns>
	    IEnumerable<MapElement> GetShortestPathElementsList(MapElement startElement, MapElement targetElement);

	    /// <summary>
	    /// Returns a ShortPath object representing the shortest path
	    /// </summary>
	    /// <param name="startElement"></param>
	    /// <param name="targetElement"></param>
	    /// <param name="graphService"></param>
	    /// <returns>ShortPath</returns>
	    Path GetShortestPath(MapElement startElement, MapElement targetElement);
	}
}

