using Exposeum.Models;
using Exposeum.TDGs;

namespace Exposeum.Mappers
{
    public class MapMapper
    {
        private static MapMapper _instance;
        readonly StorylineMapper _storylineMapper;
        readonly MapEdgeMapper _edgeMapper;
        readonly MapElementsMapper _mapElementsMapper;
        readonly FloorMapper _floorMapper;
        readonly MapTdg _mapTdg; 

        private MapMapper()
        {
            _storylineMapper = StorylineMapper.GetInstance();
            _edgeMapper = MapEdgeMapper.GetInstance();
            _mapElementsMapper = MapElementsMapper.GetInstance();
            _mapTdg = MapTdg.GetInstance();
            _floorMapper = FloorMapper.GetInstance(); 
        }

        public static MapMapper GetInstance()
        {
            if (_instance == null)
                _instance = new MapMapper();
            return _instance; 
        }

        public Map ParseMap()
        {
            Map map = Map.GetInstance();
            map.Edges = _edgeMapper.GetAllMapEdges();
            map.Storylines = _storylineMapper.GetAllStorylines();
            map.MapElements = _mapElementsMapper.GetAllMapElements();
            map.Floors = _floorMapper.GetAllFloors(); 
            Tables.Map tableMap = _mapTdg.GetMap(map.Id);
            map.CurrentFloor = _floorMapper.GetFloor(tableMap.CurrentFloorId);
            map.CurrentStoryline = _storylineMapper.GetStoryline(tableMap.CurrentStoryLineId);
            return map;
        }

        public void UpdateMap()
        {
            Map map = Map.GetInstance();
            _edgeMapper.UpdateMapEdgesList(map.Edges);
            _floorMapper.UpdateFloorsList(map.Floors);
            _storylineMapper.UpdateStorylinesList(map.Storylines);
            _mapElementsMapper.UpdateList(map.MapElements);

            Tables.Map tableMap = new Tables.Map
            {
                Id = map.Id,
                CurrentFloorId = map.CurrentFloor.Id,
                CurrentStoryLineId = map.CurrentStoryline.StorylineId
            };
            _mapTdg.UpdateMap(tableMap);
        }
    }
}