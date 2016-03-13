using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class FloorTDG:TDG
    {
        private static FloorTDG _instance;

        public static FloorTDG GetInstance()
        {
            if (_instance == null)
                _instance = new FloorTDG();
            return _instance;
        }

        public void Update(Floor item)
        {
            _db.Update(item);
        }
        public void Add(Floor item)
        {
            _db.Insert(item);
        }

        public Floor GetFloor(int id)
        {
            return _db.Get<Floor>(id);
        }
    }
}