using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class FloorTDG : TDG
    {
        private static FloorTDG _instance;

        private FloorTDG() { }

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

        public bool Equals(Floor object1, Floor Object2)
        {
            if (object1.ID == Object2.ID && object1.imageId == Object2.imageId)
                return true;
            else return false; 
        }
    }
}