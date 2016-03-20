using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class IconTDG : TDG
    {
        private static IconTDG _instance;

        private IconTDG() { }

        public static IconTDG GetInstance()
        {
            if(_instance == null)
                _instance = new IconTDG();

            return _instance;
        }

        public void Add(Icon item)
        {
            _db.InsertOrReplace(item);
        }

        public void Update(Icon item)
        {
            _db.Update(item);
        }

        public Icon GetIcon(int id)
        {
            return _db.Get<Icon>(id);
        }

        public bool Equals(Icon object1, Icon Object2)
        {
            if (object1.ID == Object2.ID && object1.path.Equals(Object2.path))
                return true;
            else return false;
        }
    }
}