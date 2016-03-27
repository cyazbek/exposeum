using System.Collections.Generic;
using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class MapEdgeTdg : Tdg
    {
        private static MapEdgeTdg _instance;

        private MapEdgeTdg() { }

        public static MapEdgeTdg GetInstance()
        {
            if (_instance == null)
                _instance = new MapEdgeTdg();

            return _instance;
        }

        public void Add(MapEdge item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(MapEdge item)
        {
            Db.Update(item);
        }

        public MapEdge GetEdge(int id)
        {
            return Db.Get<MapEdge>(id);
        }

        public void DeleteAll()
        {
            Db.DeleteAll<MapEdge>();
        }

        public bool Equals(MapEdge object1, MapEdge object2)
        {
            if (object1.Id == object2.Id && object1.Distance==object2.Distance && object1.StartMapElementId == object2.StartMapElementId && object1.EndMapElementId == object2.EndMapElementId)
                return true;
            return false;
        }

        public List<MapEdge> GetAllEdges()
        {
            return new List<MapEdge>(Db.Table<MapEdge>());
        }
        
    }
}