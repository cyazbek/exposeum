using Exposeum.Tables;
using System.Collections.Generic;

namespace Exposeum.TDGs
{
    public class FloorTdg : Tdg
    {
        private static FloorTdg _instance;

        private FloorTdg() { }

        public static FloorTdg GetInstance()
        {
            if (_instance == null)
                _instance = new FloorTdg();
            return _instance;
        }

        public void Update(Floor item)
        {
            Db.Update(item);
        }

        public void Add(Floor item)
        {
            Db.InsertOrReplace(item);
        }

        public Floor GetFloor(int id)
        {
            return Db.Get<Floor>(id);
        }

        public List<Floor> GetAllFloors()
        {
            return new List<Floor>(Db.Table<Floor>());
        }

        public bool Equals(Floor object1, Floor object2)
        {
            if (object1.Id == object2.Id && object1.ImagePath.Equals(object2.ImagePath))
                return true;
            return false;
        }
    }
}