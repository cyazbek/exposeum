using System.Collections.Generic;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;


namespace Exposeum.Mappers
{
    public class MapElementsMapper
    {
        private readonly MapElementsTDG _mapElementsTdg = MapElementsTDG.GetInstance();
        private readonly List<MapElement> _listOfMapElements = new List<MapElement>();

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