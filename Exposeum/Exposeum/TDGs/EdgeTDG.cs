using System.Collections.Generic;
using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class EdgeTDG : TDG
    {
        private static EdgeTDG _instance;

        private EdgeTDG() { }

        public static EdgeTDG GetInstance()
        {
            if (_instance == null)
                _instance = new EdgeTDG();

            return _instance;
        }

        public void Add(Edge item)
        {
            _db.InsertOrReplace(item);
        }

        public void Update(Edge item)
        {
            _db.Update(item);
        }

        public Edge GetEdge(int id)
        {
            return _db.Get<Edge>(id);
        }

        public void DeleteAll()
        {
            _db.DeleteAll<Edge>();
        }

        public bool Equals(Edge object1, Edge object2)
        {
            if (object1.ID == object2.ID && object1.distance==object2.distance && object1.startMapElementId == object2.startMapElementId && object1.endMapElementId == object2.endMapElementId)
                return true;
            else
                return false;
        }

        public List<Edge> GetAllEdges()
        {
            return new List<Edge>(_db.Table<Edge>());
        }
        
    }
}