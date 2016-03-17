using System.Collections.Generic;
using Exposeum.Models;
using Exposeum.TDGs;

namespace Exposeum.Mappers
{
    public class EdgeMapper
    {
        private static EdgeMapper _instance;
        private readonly EdgeTDG _edgeTdg;
        private readonly List<Edge> _listOfEdges;
        private readonly MapElementsMapper _mapElementsMapper;

        private EdgeMapper()
        {
            _edgeTdg = EdgeTDG.GetInstance();
            _listOfEdges = new List<Edge>();
            _mapElementsMapper = MapElementsMapper.GetInstance();
        }

        public static EdgeMapper GetInstance()
        {
            if (_instance == null)
                _instance = new EdgeMapper();

            return _instance;
        }

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