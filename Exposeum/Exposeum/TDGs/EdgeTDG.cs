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
    }
}