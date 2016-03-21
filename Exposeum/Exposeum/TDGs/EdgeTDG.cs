using System.Collections.Generic;
using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class EdgeTdg : Tdg
    {
        private static EdgeTdg _instance;

        private EdgeTdg() { }

        public static EdgeTdg GetInstance()
        {
            if (_instance == null)
                _instance = new EdgeTdg();

            return _instance;
        }

        public void Add(Edge item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(Edge item)
        {
            Db.Update(item);
        }

        public Edge GetEdge(int id)
        {
            return Db.Get<Edge>(id);
        }

        public void DeleteAll()
        {
            Db.DeleteAll<Edge>();
        }

        public bool Equals(Edge object1, Edge object2)
        {
            if (object1.Id == object2.Id && object1.Distance==object2.Distance && object1.StartMapElementId == object2.StartMapElementId && object1.EndMapElementId == object2.EndMapElementId)
                return true;
            else
                return false;
        }

        public List<Edge> GetAllEdges()
        {
            return new List<Edge>(Db.Table<Edge>());
        }
        
    }
}