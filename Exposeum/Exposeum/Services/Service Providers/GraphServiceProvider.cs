using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Exposeum.Models;
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

			//add some waypoint betweem the start and the end 0.3306878,0.5831111
			PointOfTravel pot1 = new PointOfTravel(0.330687f, 0.5831111f, mapElements.Last().Floor);
			PointOfTravel pot2 = new PointOfTravel(0.2740971f,0.6113333f, mapElements.Last().Floor);


            foreach (StoryLine slLine in storyLineService.GetStoryLines())
            {
                MapElement previous = pot2;

                foreach (MapElement mapElement in slLine.MapElements)
                {

                    mapEdges.Add(new MapEdge(previous, mapElement));



                    previous = mapElement;
                }

                mapEdges.Add(new MapEdge(previous, pot1));
                mapEdges.Add(new MapEdge(pot1, pot2));
            }

            //StoryLine nipperidiot = storyLineService.GetStoryLines()[0];
            //MapElement previous = nipperidiot.MapElements.Last();

            //foreach (MapElement mapElement in nipperidiot.MapElements)
            //{


            //    mapEdges.Add(new MapEdge(previous, mapElement));


            //    previous = mapElement;
            //}

            _graphInstance.AddVerticesAndEdgeRange(mapEdges);
            

        }
    }

}



   