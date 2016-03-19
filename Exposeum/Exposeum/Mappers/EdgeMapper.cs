using System.Collections.Generic;
using Exposeum.Models;
using Exposeum.TDGs;

namespace Exposeum.Mappers
{
    public class EdgeMapper
    {
        private readonly EdgeTDG _edgeTdg = EdgeTDG.GetInstance();
        private readonly List<Edge> _listOfEdges = new List<Edge>();
        private readonly MapElementsMapper _mapElementsMapper = new MapElementsMapper();


        public List<Edge> GetAllEdges()
        {
            List<Tables.Edge> listEdges = _edgeTdg.GetAllEdges();

            foreach (var edge in listEdges)
            {
                int startElement = edge.startMapElementId;
                int endElement = edge.endMapElementId;
                double distance = edge.distance;

                MapElement startMapElement = _mapElementsMapper.GetMapElement(startElement);
                MapElement endMapElement = _mapElementsMapper.GetMapElement(endElement);
                
                Edge edgeModel = new Edge(startMapElement, endMapElement, distance);

                _listOfEdges.Add(edgeModel);
            }

            return _listOfEdges;
        }
    }
}