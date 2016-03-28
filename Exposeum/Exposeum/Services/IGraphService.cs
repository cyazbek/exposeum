using Exposeum.Models;
using QuickGraph;

namespace Exposeum.Services
{
    /// <summary>
    /// interface for the graph service
    /// </summary>
    public interface IGraphService
    {
        /// <summary>
        /// return a graph
        /// </summary>
        /// <returns></returns>
        UndirectedGraph<MapElement, MapEdge> GetGraph();

    }


}