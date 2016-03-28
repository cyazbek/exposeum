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

        public void AddList(List<MapElement> elements)
        {
            foreach (var x in elements)
            {
                if(elements.GetType().ToString() == "Exposeum.TempModels.PointOfInterest")

            }
        }

    }
}