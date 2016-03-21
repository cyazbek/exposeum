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
        private readonly List<MapElement> _listOfMapElements;

        private MapElementsMapper()
        {
            _mapElementsTdg = MapElementsTdg.GetInstance();
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
                MapElement mapElementModel = GetMapElement(mapElementTable.Id);
                _listOfMapElements.Add(mapElementModel);
            }

            return _listOfMapElements;
        }

        public List<MapElement> GetAllMapElementsFromStoryline(int storylineId)
        {
            List<MapElement> listMapElementsTable = new List<MapElement>();
            List<int> mapElementIds = StoryLineMapElementListTdg.GetInstance().GetAllStorylineMapElements(storylineId);

            foreach (int mapElementId in mapElementIds)
            {
                MapElements tableMapElement = _mapElementsTdg.GetMapElement(mapElementId);
                listMapElementsTable.Add(MapElemenTableToModel(tableMapElement));
            }

            return listMapElementsTable;
        }

        private MapElements MapElementModelToTable(MapElement mapElement)
        {
            MapElements mapElements = new MapElements
            {
                Id = mapElement.Id,
                Visited = Convert.ToInt32(mapElement.Visited),
                IconId = mapElement.IconId,
                UCoordinate = mapElement.UCoordinate,
                VCoordinate = mapElement.VCoordinate,
                FloorId = mapElement.Floor.Id
            };
            
            switch (mapElement.GetType().ToString())
            {
                case "Exposeum.TempModels.PointOfInterest":
                {
                    PointOfInterest poi = (PointOfInterest) mapElement;

                    mapElements.BeaconId = poi.Beacon.Id;
                    mapElements.StoryLineId = poi.StoryLineId;
                    mapElements.PoiDescription = poi.Description.Id;
                    // mapElements.exhibitionContent = poi._exhibitionContent._id;

                    return mapElements;
                }

                case "Exposeum.TempModels.WayPoint":
                {
                    WayPoint wayPoint = (WayPoint)mapElement;
                    mapElements.Label = wayPoint.Label.ToString();
                    return mapElements;
                }
                default:
                    return null;
            }
        }

        private MapElement MapElemenTableToModel(MapElements mapElementTable)
        {
            string discrinator = mapElementTable.Discriminator;

            switch (discrinator)
            {
                case "PointOfInterest":
                {
                    PointOfInterest pointOfInterest = new PointOfInterest
                    {
                        Id = mapElementTable.Id,
                        Beacon = BeaconMapper.GetInstance().GetBeacon(mapElementTable.BeaconId),
                        StoryLineId = mapElementTable.StoryLineId,
                        Description = PointOfInterestDescriptionMapper.GetInstance().GetPointOfInterestDescription(mapElementTable.PoiDescription),
                        //_exhibitionContent = ExhibitionContentMapper.GetInstance().GetExhibitionContent(mapElementTable.exhibitionContent),
                    };
                    return pointOfInterest;
                }
                case "WayPoint":
                {
                    WayPoint wayPoint = new WayPoint
                    {
                        Id = mapElementTable.Id,
                        Label = (WaypointLabel)Enum.Parse(typeof(WaypointLabel), mapElementTable.Label)
                    };
                    return wayPoint;
                }
                default:
                    return null;
            }
        }

    }
}