using System.Collections.Generic;
using Exposeum.Models;
using Exposeum.TDGs;

namespace Exposeum.Mappers
{
    public class EdgeMapper
    {
        private readonly EdgeTDG _edgeTdg = EdgeTDG.GetInstance();
        private readonly List<MapEdge> _listOfEdges = new List<MapEdge>();
        private readonly MapElementsMapper _mapElementsMapper = new MapElementsMapper();


        public List<MapEdge> GetAllEdges()
        {
            List<Tables.Edge> listEdges = _edgeTdg.GetAllEdges();

            foreach (var edge in listEdges)
            {
                int startElement = edge.startMapElementId;
                int endElement = edge.endMapElementId;
                double distance = edge.distance;

                MapElement startMapElement = _mapElementsMapper.GetMapElement(startElement);
                MapElement endMapElement = _mapElementsMapper.GetMapElement(endElement);
                
                MapEdge edgeModel = new MapEdge(startMapElement, endMapElement, distance);

                _listOfEdges.Add(edgeModel);
            }

            return _listOfEdges;
        }
    }
}