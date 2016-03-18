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
using Exposeum;
using Exposeum.Models;
using Exposeum.Services;
using Exposeum.Services.Service_Providers;
using NUnit.Framework;
using QuickGraph;

namespace UnitTests
{
    [TestFixture]
    class ShortestPathServiceProviderTests
    {
        private IShortestPathService _shortestPathService;
        private IGraphService _graphService;

        [SetUp]
        public void Setup()
        {
            _graphService = new MockGraphServiceProvider();
            _shortestPathService = new ShortestPathServiceProvider(_graphService);

        }

        [Test]
        public void GetShortestElementsListFromValidGraph()
        {
            MapElement p1 = ((MockGraphServiceProvider)_graphService).GetStartElement();
            MapElement p2 = ((MockGraphServiceProvider)_graphService).GetTargetElement();

            var elementsList = _shortestPathService.GetShortestPathElementsList(p1, p2);

            Assert.AreEqual(3, elementsList.ToList().Count);
        }

        [Test]
        public void GetShortestEdgesListFromValidGraph()
        {
            MapElement p1 = ((MockGraphServiceProvider)_graphService).GetStartElement();
            MapElement p2 = ((MockGraphServiceProvider)_graphService).GetTargetElement();

            var edgesList = _shortestPathService.GetShortestPathEdgesList(p1, p2);

            Assert.AreEqual(2, edgesList.Count());
        }

        [Test]
        public void GetShortestPathFromValidGraph()
        {
            MapElement p1 = ((MockGraphServiceProvider) _graphService).GetStartElement();
            MapElement p2 = ((MockGraphServiceProvider)_graphService).GetTargetElement();

            var path = _shortestPathService.GetShortestPath(p1, p2);

            Assert.AreEqual(3, path.MapElements.Count);
        }

    }

    internal class MockGraphServiceProvider : IGraphService
    {

        MapElement P1 = new PointOfInterest();
        MapElement P2 = new PointOfInterest();
        MapElement P3 = new PointOfInterest();
        MapElement P4 = new PointOfInterest();
        MapElement P5 = new PointOfInterest();
        MapElement P6 = new PointOfInterest();
        MapElement P7 = new PointOfInterest();

        public MapElement GetStartElement()
        {
            return P1;
        }

        public MapElement GetTargetElement()
        {
            return P3;
        }
        public UndirectedGraph<MapElement, MapEdge> GetGraph()
        {
           
            MapEdge E1 = new MapEdge(P1, P2);
            MapEdge E2 = new MapEdge(P2, P3);
            MapEdge E3 = new MapEdge(P1, P4);
            MapEdge E4 = new MapEdge(P4, P5);
            MapEdge E5 = new MapEdge(P5, P6);
            MapEdge E6 = new MapEdge(P6, P7);
            MapEdge E7 = new MapEdge(P7, P4);

            UndirectedGraph<MapElement, MapEdge> graph = new UndirectedGraph<MapElement, MapEdge>();

            graph.AddVerticesAndEdge(E1);
            graph.AddVerticesAndEdge(E2);
            graph.AddVerticesAndEdge(E3);
            graph.AddVerticesAndEdge(E4);
            graph.AddVerticesAndEdge(E5);
            graph.AddVerticesAndEdge(E6);
            graph.AddVerticesAndEdge(E7);

            return graph;
        }
    }
}