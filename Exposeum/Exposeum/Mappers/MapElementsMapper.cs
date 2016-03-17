using System.Collections.Generic;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;


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
                    PointOfInterest pointOfInterest = null;
                    return pointOfInterest;
                case "WayPoint":
                    PointOfTravel pointOfTravel = null;
                    return pointOfTravel;
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