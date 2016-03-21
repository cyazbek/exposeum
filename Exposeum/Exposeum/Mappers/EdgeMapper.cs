using System.Collections.Generic;
using Exposeum.TDGs;
using Exposeum.TempModels;

namespace Exposeum.Mappers
{
    public class EdgeMapper
    {
        private static EdgeMapper _instance;
        private readonly EdgeTdg _edgeTdg;
        private readonly List<Edge> _listOfEdges;
        private readonly MapElementsMapper _mapElementsMapper;

        private EdgeMapper()
        {
            _edgeTdg = EdgeTdg.GetInstance();
            _listOfEdges = new List<Edge>();
            _mapElementsMapper = MapElementsMapper.GetInstance();
        }

        public static EdgeMapper GetInstance()
        {
            if (_instance == null)
                _instance = new EdgeMapper();

            return _instance;
        }

        public void AddEdge(Edge edge)
        {
            Tables.Edge edgeTable = EdgeModelToTable(edge);
            _edgeTdg.Add(edgeTable);
        }

        public void UpdateEdge(Edge edge)
        {
            Tables.Edge edgeTable = EdgeModelToTable(edge);
            _edgeTdg.Update(edgeTable);
        }

        public Edge GetEdge(int edgeId)
        {
            Tables.Edge edgeTable = _edgeTdg.GetEdge(edgeId);
            Edge edgeModel = EdgeTableToModel(edgeTable);
            return edgeModel;
        }

        public List<Edge> GetAllEdges()
        {
            List<Tables.Edge> listEdges = _edgeTdg.GetAllEdges();

            foreach (var edge in listEdges)
            {
                Edge edgeModel = EdgeTableToModel(edge);
                _listOfEdges.Add(edgeModel);
            }

            return _listOfEdges;
        }

        private Tables.Edge EdgeModelToTable(Edge edge)
        {
            Tables.Edge edgeTable = new Tables.Edge
            {
                Id = edge.Id,
                Distance = edge.Distance,
                StartMapElementId = edge.Start.Id,
                EndMapElementId = edge.End.Id
            };

            return edgeTable;
        }

        public void UpdateEdgesList(List<Edge> list)
        {
            foreach(var x in list)
            {
                UpdateEdge(x);
            }
        }

        private Edge EdgeTableToModel(Tables.Edge edgeTable)
        {
            Edge edgeModel = new Edge
            {
                Id = edgeTable.Id,
                Distance = edgeTable.Distance,
                Start = MapElementsMapper.GetInstance().GetMapElement(edgeTable.StartMapElementId),
                End = MapElementsMapper.GetInstance().GetMapElement(edgeTable.EndMapElementId),
            };
            return edgeModel;
        }

    }
}