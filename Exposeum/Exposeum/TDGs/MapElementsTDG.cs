using Exposeum.Tables;
using System.Collections.Generic;

namespace Exposeum.TDGs
{
    public class MapElementsTDG : TDG
    {
        private static MapElementsTDG _instance;

        private MapElementsTDG() { }

        public static MapElementsTDG GetInstance()
        {
            if (_instance == null)
                _instance = new MapElementsTDG();
            return _instance;
        }

        public void Add(MapElements item)
        {
            _db.Insert(item);
        }

        public void Update(MapElements item)
        {
            _db.Update(item);
        }

        public MapElements GetMapElement(int id)
        {
            return _db.Get<MapElements>(id);
        }

        public List<MapElements> GetAllMapElements()
        {
            return new List<MapElements>(_db.Table<MapElements>());
        }
        public bool Equals(MapElements element1, MapElements element2)
        {
            if (element1.ID == element2.ID &&
            element1.uCoordinate == element2.uCoordinate &&
            element1.vCoordinate == element2.vCoordinate &&
            element1.discriminator == element2.discriminator &&
            element1.visited == element2.visited &&
            element1.beaconId == element2.beaconId &&
            element1.poiDescription == element2.poiDescription &&
            element1.label == element2.label &&
            element1.exhibitionContent == element2.exhibitionContent &&
            element1.floorId == element2.floorId)
                return true;
            else return false; 
        }
    }
}