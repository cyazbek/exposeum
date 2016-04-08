using System;
using Exposeum.Services;
using QuickGraph;
using Exposeum.Models;
using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace Exposeum
{
	public class GraphServiceProviderS4Demo: IGraphService
	{
		private UndirectedGraph<MapElement, MapEdge> _graphInstance;

		public GraphServiceProviderS4Demo ()
		{
			_graphInstance = new UndirectedGraph<MapElement, MapEdge>();
			PopulateGraph();
		}

		public UndirectedGraph<MapElement, MapEdge> GetGraph()
		{
			return _graphInstance;
		}

		private void PopulateGraph()
		{
			_graphInstance.AddVerticesAndEdgeRange(Map.GetInstance().Edges);
		}
	}
}

