using System;
using System.Collections.Generic;
using Exposeum.TempModels;
using Exposeum.Tables;
using Exposeum.TDGs;
using Enum = System.Enum;
using MapElement = Exposeum.TempModels.MapElement;
using PointOfInterest = Exposeum.TempModels.PointOfInterest;

namespace Exposeum.Mappers
{
    public class MapElementsMapper
    {
        private static MapElementsMapper _instance;
        private readonly MapElementsTdg _mapElementsTdg;
        private readonly PointOfInterestMapper _pointOfInterestMapper;
        private readonly WayPointMapper _wayPointMapper;

        private MapElementsMapper()
        {
            _mapElementsTdg = MapElementsTdg.GetInstance();
            _pointOfInterestMapper = PointOfInterestMapper.GetInstance();
            _wayPointMapper = WayPointMapper.GetInstance();
        }

        public static MapElementsMapper GetInstance()
        {
            if(_instance == null)
                _instance = new MapElementsMapper();

            return _instance;
        }

        public List<MapElement> GetAllMapElements()
        {
            List<MapElements> tableElements = _mapElementsTdg.GetAllMapElements();

            List<MapElement> modelList = new List<MapElement>();

            foreach (var x in tableElements)
            {
                if (x.Discriminator == "PointOfInterest")
                {
                    modelList.Add(_pointOfInterestMapper.PoiTableToModel(x));
                }
                else 
                    modelList.Add(_wayPointMapper.WaypointTableToModel(x));
            }
            return modelList;
        }       

        public void AddList(List<MapElement> elements)
        {
            foreach (var x in elements)
            {
                if(x.GetType().ToString() == "Exposeum.TempModels.PointOfInterest")
                    _pointOfInterestMapper.Add((PointOfInterest)x);
                else 
                    _wayPointMapper.Add((WayPoint)x);
            }
        }

        public List<MapElement> GetAllElementByStorylineId(int id)
        {
            List<MapElements> list = new List<MapElements>();
            list=_mapElementsTdg.GetAllMapElements();
            List<MapElements> tableElements = new List<MapElements>();
            foreach (var x in list)
            {
                if(x.StoryLineId==id)
                    tableElements.Add(x);
            }
            List<MapElement> modelList = new List<MapElement>();
            foreach (var x in tableElements)
            {
                if(x.Discriminator=="PointOfInterest")
                    modelList.Add(_pointOfInterestMapper.PoiTableToModel(x));
                else
                {
                    modelList.Add((_wayPointMapper.WaypointTableToModel(x)));
                }
            }
            return modelList;
        }

        public void UpdateList(List<MapElement> list)
        {
            foreach (var x in list)
            {
                if(x.GetType().ToString()=="Exposeum.TempModels.PointOfInterest")
                    _pointOfInterestMapper.Update((PointOfInterest)x);
                else 
                    _wayPointMapper.Update((WayPoint)x);
            }
        }


        public MapElement Get(int mapElementId)
        {
            MapElements mapElement = _mapElementsTdg.GetMapElement(mapElementId);

            if (mapElement.Discriminator == "PointOfInterest")
            {
                return _pointOfInterestMapper.PoiTableToModel(mapElement);
            }
            else
                return _wayPointMapper.WaypointTableToModel(mapElement);
        }
    }
}