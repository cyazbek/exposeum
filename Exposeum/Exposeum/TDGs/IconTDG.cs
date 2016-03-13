using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class IconTDG : TDG
    {
        private static IconTDG _instance;

        public static IconTDG GetInstance()
        {
            if(_instance == null)
                _instance = new IconTDG();

            return _instance;
        }

        public void Add(Icon item)
        {
            _db.Insert(item);
        }

        public void Update(Icon item)
        {
            _db.Update(item);
        }
        public Icon GetIcon(int id)
        {
            return _db.Get<Icon>(id);
        }
    }
}