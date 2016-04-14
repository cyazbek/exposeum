using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Exposeum.Models;
using Java.Util;
using QuickGraph;
using Ninject;
using Exposeum.Mappers;

namespace Exposeum.Services.Service_Providers
{
    public class GraphServiceProvider : IGraphService
    {
        private UndirectedGraph<MapElement, MapEdge> _graphInstance;
		private MapEdgeMapper _mapEdgeMapper;

        public GraphServiceProvider()
        {
			_graphInstance = new UndirectedGraph<MapElement, MapEdge>();
			_mapEdgeMapper = MapEdgeMapper.GetInstance ();
            PopulateGraph();
        }

		public UndirectedGraph<MapElement, MapEdge> GetGraph()
		{
			return _graphInstance;
		}

        private void PopulateGraph()
        {
			List<MapEdge> mapEdges = _mapEdgeMapper.GetAllMapEdges ();

			_graphInstance.AddVerticesAndEdgeRange(mapEdges);
            

        }
    }

}
   