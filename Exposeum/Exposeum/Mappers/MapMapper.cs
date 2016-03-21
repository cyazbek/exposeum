using System.Collections.Generic;
using Exposeum.TempModels;
using Exposeum.TDGs;

namespace Exposeum.Mappers
{
    public class MapMapper
    {
        private static MapMapper _instance;
        readonly StorylineMapper _storylineMapper;
        readonly EdgeMapper _edgeMapper;
        readonly MapElementsMapper _mapElementsMapper;
        readonly FloorMapper _floorMapper;
        readonly MapTDG _mapTDG; 

        private MapMapper()
        {
            _storylineMapper = StorylineMapper.GetInstance();
            _edgeMapper = EdgeMapper.GetInstance();
            _mapElementsMapper = MapElementsMapper.GetInstance();
            _mapTDG = MapTDG.GetInstance();
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
            map._edges = _edgeMapper.GetAllEdges();
            map._storylines = _storylineMapper.GetAllStorylines();
            map._mapElements = _mapElementsMapper.GetAllMapElements();
            map._floors = _floorMapper.GetAllFloors(); 
            Tables.Map tableMap = _mapTDG.getMap(map._id);
            map._currentFloor = _floorMapper.GetFloor(tableMap.currentFloorID);
            map._currentStoryline = _storylineMapper.GetStoryline(tableMap.currentStoryLineID);
            return map;
        }

        public void UpdateMap()
        {
            Map map = Map.GetInstance();
            _edgeMapper.UpdateEdgesList(map._edges);
            _floorMapper.UpdateFloorsList(map._floors);
            _storylineMapper.UpdateStorylinesList(map._storylines);
            _mapElementsMapper.UpdateMapElementList(map._mapElements);
            Tables.Map tableMap = new Tables.Map
            {
                ID = map._id,
                currentFloorID = map._currentFloor._id,
                currentStoryLineID = map._currentStoryline._storylineId
            };
            _mapTDG.UpdateMap(tableMap);
        }
    }
}