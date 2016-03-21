using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class IconTdg : Tdg
    {
        private static IconTdg _instance;

        private IconTdg() { }

        public static IconTdg GetInstance()
        {
            if(_instance == null)
                _instance = new IconTdg();

            return _instance;
        }

        public void Add(Icon item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(Icon item)
        {
            Db.Update(item);
        }

        public Icon GetIcon(int id)
        {
            return Db.Get<Icon>(id);
        }

        public bool Equals(Icon object1, Icon object2)
        {
            if (object1.Id == object2.Id && object1.Path.Equals(object2.Path))
                return true;
            return false;
        }
    }
}