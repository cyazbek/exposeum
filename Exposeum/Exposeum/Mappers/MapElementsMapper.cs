using System.Collections.Generic;
using Exposeum.Models;
using Exposeum.TempModels;
using Exposeum.Tables;
using Exposeum.TDGs;
using MapElement = Exposeum.TempModels.MapElement;
using PointOfInterest = Exposeum.TempModels.PointOfInterest;


namespace Exposeum.Mappers
{
    public class MapElementsMapper
    {
        private static MapElementsMapper _instance;
        private readonly MapElementsTDG _mapElementsTdg;
        private readonly List<MapElement> _listOfMapElements;

        private MapElementsMapper()
        {
            _mapElementsTdg = MapElementsTDG.GetInstance();
            _listOfMapElements = new List<MapElement>();
        }

        public static MapElementsMapper GetInstance()
        {
            if(_instance == null)
                _instance = new MapElementsMapper();

            return _instance;
        }

        public MapElement GetMapElement(int id)
        {
            MapElements mapElementTable = _mapElementsTdg.GetMapElement(id);

            string discrinator = mapElementTable.discriminator;

            switch (discrinator)
            {
                case "PointOfInterest":
                {
                    PointOfInterest pointOfInterest = new PointOfInterest
                    {
                        _pointOfInterestId = mapElementTable.ID,
                        _beacon = BeaconMapper.GetInstance().GetBeacon(mapElementTable.beaconId),
                        _storyLineId = mapElementTable._storyLineId,
                        _description = PointOfInterestDescriptionMapper.GetInstance().GetPointOfInterestDescription(mapElementTable.poiDescription),
                        //_exhibitionContent = ExhibitionContentMapper.GetInstance().GetExhibitionContent(mapElementTable.exhibitionContent),
                    };
                    return pointOfInterest;
                }
                case "WayPoint":
                    WayPoint wayPoint = null;
                    return wayPoint;
                default:
                    return null;
            }
        }

        public List<MapElement> GetAllMapElements()
        {
            List<Tables.MapElements> listMapElementsTable = _mapElementsTdg.GetAllMapElements();

            foreach (var mapElementTable in listMapElementsTable)
            {
                int idMapElementTable = mapElementTable.ID;

                MapElement mapElementModel = GetMapElement(idMapElementTable);

                _listOfMapElements.Add(mapElementModel);
            }

            return _listOfMapElements;
        }
    }
}