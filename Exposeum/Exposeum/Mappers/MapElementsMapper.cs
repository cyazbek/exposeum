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

        public void AddMapElement(MapElement mapElement)
        {
            MapElements mapElementTable = MapElementModelToTable(mapElement);
            _mapElementsTdg.Add(mapElementTable);
        }

        public void AddMapElementList(List<MapElement> mapElements)
        {
            foreach(var mapElement in mapElements)
                AddMapElement(mapElement);            
        }

        public void UpdateMapElement(MapElement mapElement)
        {
            MapElements mapElementTable = MapElementModelToTable(mapElement);
            _mapElementsTdg.Update(mapElementTable);
        }

        public void UpdateMapElementList(List<MapElement> mapElements)
        {
            foreach (var mapElement in mapElements)
                UpdateMapElement(mapElement);
        }

        public MapElement GetMapElement(int id)
        {
            MapElements mapElementTable = _mapElementsTdg.GetMapElement(id);
            return MapElemenTableToModel(mapElementTable);
        }

        public List<MapElement> GetAllMapElements()
        {
            List<MapElements> listMapElementsTable = _mapElementsTdg.GetAllMapElements();

            foreach (var mapElementTable in listMapElementsTable)
            {
                MapElement mapElementModel = GetMapElement(mapElementTable.ID);
                _listOfMapElements.Add(mapElementModel);
            }

            return _listOfMapElements;
        }

        public List<MapElements> GetAllMapElementsFromStoryline(int storylineId)
        {
            List<MapElements> listMapElementsTable = new List<MapElements>();
            List<int> mapElementIds = StoryLineMapElementListTDG.GetInstance().GetAllStorylineMapElements(storylineId);

            foreach (int mapElementId in mapElementIds)
                listMapElementsTable.Add(_mapElementsTdg.GetMapElement(mapElementId));
            
            return listMapElementsTable;
        }

        private MapElements MapElementModelToTable(MapElement mapElement)
        {
            MapElements mapElements = new MapElements
            {
                ID = mapElement._id,
                visited = Convert.ToInt32(mapElement._visited),
                iconId = mapElement._iconId,
                uCoordinate = mapElement._uCoordinate,
                vCoordinate = mapElement._vCoordinate,
                floorId = FloorMapper.GetInstance().GetFloor(mapElement._floor._id)._id
            };
            
            switch (mapElement.GetType().ToString())
            {
                case "PointOfInterest":
                {
                    PointOfInterest poi = (PointOfInterest) mapElement;

                    mapElements.beaconId = BeaconMapper.GetInstance().GetBeacon(poi._beacon._id)._id;
                    // mapElements._storyLineId = StorylineMapper.GetInstance().GetStoryline(poi._storyLineId)._id;
                    mapElements.poiDescription = PointOfInterestDescriptionMapper.GetInstance().GetPointOfInterestDescription(poi._description._id)._id;
                    // mapElements.exhibitionContent = ExhibitionContentMapper.GetInstance().GetExhibitionContent(poi._exhibitionContent._id)._id;

                    return mapElements;
                }

                case "WayPoint":
                {
                    WayPoint wayPoint = (WayPoint)mapElement;
                    mapElements.label = wayPoint._label.ToString();
                    return mapElements;
                }
                default:
                    return null;
            }
        }

        private MapElement MapElemenTableToModel(MapElements mapElementTable)
        {
            string discrinator = mapElementTable.discriminator;

            switch (discrinator)
            {
                case "PointOfInterest":
                {
                    PointOfInterest pointOfInterest = new PointOfInterest
                    {
                        _id = mapElementTable.ID,
                        _beacon = BeaconMapper.GetInstance().GetBeacon(mapElementTable.beaconId),
                        _storyLineId = mapElementTable._storyLineId,
                        _description = PointOfInterestDescriptionMapper.GetInstance().GetPointOfInterestDescription(mapElementTable.poiDescription),
                        //_exhibitionContent = ExhibitionContentMapper.GetInstance().GetExhibitionContent(mapElementTable.exhibitionContent),
                    };
                    return pointOfInterest;
                }
                case "WayPoint":
                {
                    WayPoint wayPoint = new WayPoint
                    {
                        _id = mapElementTable.ID,
                        _label = (WaypointLabel)Enum.Parse(typeof(WaypointLabel), mapElementTable.label)
                    };
                    return wayPoint;
                }
                default:
                    return null;
            }
        }


    }
}