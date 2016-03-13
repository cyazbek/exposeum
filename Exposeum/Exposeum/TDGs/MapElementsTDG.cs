using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class MapElementsTDG:TDG
    {
        private static MapElementsTDG _instance;

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
    }
}