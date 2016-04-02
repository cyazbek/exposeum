using System.Collections.Generic;
using Exposeum.TDGs;
using Exposeum.Models;

namespace Exposeum.Mappers
{
    public class MapEdgeMapper
    {
        private static MapEdgeMapper _instance;
        private readonly MapEdgeTdg _mapEdgeTdg;
        private readonly List<MapEdge> _listOfEdges;
        private readonly MapElementsMapper _mapElementsMapper;


        private MapEdgeMapper()
        {
            _mapEdgeTdg = MapEdgeTdg.GetInstance();
            _listOfEdges = new List<MapEdge>();
            _mapElementsMapper = MapElementsMapper.GetInstance();
        }

        public static MapEdgeMapper GetInstance()
        {
            if (_instance == null)
                _instance = new MapEdgeMapper();

            return _instance;
        }

        public void AddMapEdge(MapEdge edge)
        {
            Tables.MapEdge edgeTable = MapEdgeModelToTable(edge);
            _mapEdgeTdg.Add(edgeTable);
        }

        public void UpdateMapEdge(MapEdge edge)
        {
            Tables.MapEdge edgeTable = MapEdgeModelToTable(edge);
            _mapEdgeTdg.Update(edgeTable);
        }

        public MapEdge GetMapEdge(int edgeId)
        {
            Tables.MapEdge edgeTable = _mapEdgeTdg.GetEdge(edgeId);
            MapEdge edgeModel = MapEdgeTableToModel(edgeTable);
            return edgeModel;
        }

        public List<MapEdge> GetAllMapEdges()
        {
            List<Tables.MapEdge> listEdges = _mapEdgeTdg.GetAllEdges();

            foreach (var edge in listEdges)
            {
                MapEdge edgeModel = MapEdgeTableToModel(edge);
                _listOfEdges.Add(edgeModel);
            }

            return _listOfEdges;
        }

        private Tables.MapEdge MapEdgeModelToTable(MapEdge edge)
        {
            Tables.MapEdge edgeTable = new Tables.MapEdge
            {
                Id = edge.Id,
                Distance = edge.Distance,
                StartMapElementId = edge.Source.Id,
                EndMapElementId = edge.Target.Id
            };

            return edgeTable;
        }

        public void UpdateMapEdgesList(List<MapEdge> list)
        {
            foreach(var x in list)
            {
                UpdateMapEdge(x);
            }
        }

        private MapEdge MapEdgeTableToModel(Tables.MapEdge edgeTable)
        {
            MapEdge edgeModel = new MapEdge(_mapElementsMapper.Get(edgeTable.StartMapElementId), _mapElementsMapper.Get(edgeTable.EndMapElementId))
            {
                Id = edgeTable.Id,
                Distance = edgeTable.Distance,
            };

            return edgeModel;
        }

    }
}