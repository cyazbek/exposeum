using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class MapElementFloorListTDG:TDG
    {
        private static MapElementFloorListTDG _instance;

        public static MapElementFloorListTDG GetInstance()
        {
            if (_instance == null)
                _instance = new MapElementFloorListTDG();
            return _instance;
        }
        public void Add(MapElementFloorList item)
        {
            _db.Insert(item);
        }
    }
}