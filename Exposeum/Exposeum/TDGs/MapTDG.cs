using Exposeum.Tables; 

namespace Exposeum.TDGs
{
    public class MapTDG:TDG
    {
        private static MapTDG _instance; 
        private MapTDG()
        {

        }
        public static MapTDG GetInstance()
        {
            if (_instance == null)
                _instance = new MapTDG();
            return _instance; 
        }

        public void AddMap(Map map)
        {
            _db.Insert(map);
        }
        
        public void UpdateMap(Map map)
        {
            _db.Update(map);
        }

        public Map getMap(int id)
        {
            return _db.Get<Map>(id);
        }

        public bool Equals(Map map1, Map map2)
        {
            if (map1.ID == map2.ID && map1.currentFloorID == map2.currentFloorID && map1.currentStoryLineID == map2.currentStoryLineID)
                return true;
            else return false; 
        }
    }
}