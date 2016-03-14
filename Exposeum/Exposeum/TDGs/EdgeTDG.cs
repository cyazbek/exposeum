using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class EdgeTDG : TDG
    {
        private static EdgeTDG _instance;

        public static EdgeTDG GetInstance()
        {
            if (_instance == null)
                _instance = new EdgeTDG();

            return _instance;
        }

        public void Add(Edge item)
        {
            _db.Insert(item);
        }

        public void Update(Edge item)
        {
            _db.Update(item);
        }
        public Edge GetEdge(int id)
        {
            return _db.Get<Edge>(id);
        }
    }
}