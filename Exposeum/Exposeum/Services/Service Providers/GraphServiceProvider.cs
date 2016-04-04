using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Exposeum.Models;
using Java.Util;
using QuickGraph;
using Ninject;

namespace Exposeum.Services.Service_Providers
{
    public class GraphServiceProvider : IGraphService
    {
        private UndirectedGraph<MapElement, MapEdge> _graphInstance;

        public GraphServiceProvider()
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
            List<MapEdge> mapEdges = new List<MapEdge>();
            var storyLineService = ExposeumApplication.IoCContainer.Get<IStoryLineService>();
            List<MapElement> mapElements = storyLineService.GetActiveStoryLine().MapElements;

            PointOfInterest pExternal = new PointOfInterest(0.53f, 0.76f);
            PointOfInterestDescription dExternal = new PointOfInterestDescription("External Test POI"
                    , "External Summary", "External POI should still trigger shortest path");
            pExternal.Description = dExternal;
            pExternal.Beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 49800, 5890);
			//add some waypoint betweem the start and the end 0.3306878,0.5831111
			WayPoint pot1 = new WayPoint(0.330687f, 0.5831111f, mapElements.Last().Floor);
			WayPoint pot2 = new WayPoint(0.2740971f,0.6113333f, mapElements.Last().Floor);



            foreach (StoryLine slLine in storyLineService.GetStoryLines())
            {
                MapElement previous = pot2;
                MapEdge eExternal = null;

                foreach (MapElement mapElement in slLine.MapElements)
                {
                    if (mapElement is PointOfInterest)
                    {
                        eExternal = new MapEdge(pExternal, mapElement);
                    }

                    mapEdges.Add(new MapEdge(previous, mapElement));



                    previous = mapElement;
                }

                if (eExternal != null) mapEdges.Add(eExternal);
                mapEdges.Add(new MapEdge(previous, pot1));
                mapEdges.Add(new MapEdge(pot1, pot2));
            }

            _graphInstance.AddVerticesAndEdgeRange(mapEdges);
            

        }
    }

}



   