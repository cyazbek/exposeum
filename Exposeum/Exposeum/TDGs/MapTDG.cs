using Exposeum.Tables; 

namespace Exposeum.TDGs
{
    public class MapTdg:Tdg
    {
        private static MapTdg _instance; 
        private MapTdg()
        {

        }
        public static MapTdg GetInstance()
        {
            if (_instance == null)
                _instance = new MapTdg();
            return _instance; 
        }

        public void AddMap(Map map)
        {
            Db.InsertOrReplace(map);
        }
        
        public void UpdateMap(Map map)
        {
            Db.Update(map);
        }

        public Map GetMap(int id)
        {
            return Db.Get<Map>(id);
        }

        public bool Equals(Map map1, Map map2)
        {
            if (map1.Id == map2.Id && map1.CurrentFloorId == map2.CurrentFloorId && map1.CurrentStoryLineId == map2.CurrentStoryLineId)
                return true;
            else return false; 
        }
    }
}