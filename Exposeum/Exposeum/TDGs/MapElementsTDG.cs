using Exposeum.Tables;
using System.Collections.Generic;

namespace Exposeum.TDGs
{
    public class MapElementsTdg : Tdg
    {
        private static MapElementsTdg _instance;

        private MapElementsTdg() { }

        public static MapElementsTdg GetInstance()
        {
            if (_instance == null)
                _instance = new MapElementsTdg();
            return _instance;
        }

        public void Add(MapElements item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(MapElements item)
        {
            Db.Update(item);
        }

        public MapElements GetMapElement(int id)
        {
            return Db.Get<MapElements>(id);
        }

        public List<MapElements> GetAllMapElements()
        {
            return new List<MapElements>(Db.Table<MapElements>());
        }
        public bool Equals(MapElements element1, MapElements element2)
        {
            if (element1.Id == element2.Id &&
            element1.UCoordinate.Equals(element2.UCoordinate) &&
            element1.VCoordinate.Equals(element2.VCoordinate) &&
            element1.Discriminator == element2.Discriminator &&
            element1.Visited == element2.Visited &&
            element1.BeaconId == element2.BeaconId &&
            element1.StoryLineId == element2.StoryLineId &&
            element1.PoiDescription == element2.PoiDescription &&
            element1.Label == element2.Label &&
            element1.FloorId == element2.FloorId)
                return true;
            return false;
        }
    }
}