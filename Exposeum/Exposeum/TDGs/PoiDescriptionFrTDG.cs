using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class PoiDescriptionFrTDG : TDG
    {
        private static PoiDescriptionFrTDG _instance;

        private PoiDescriptionFrTDG() { }

        public static PoiDescriptionFrTDG GetInstance()
        {
            if (_instance == null)
                _instance = new PoiDescriptionFrTDG();
            return _instance;
        }

        public void Add(PoiDescriptionFr item)
        {
            _db.Insert(item);
        }

        public void Update(PoiDescriptionFr item)
        {
            _db.Update(item);
        }

        public PoiDescriptionFr GetPoiDescriptionFr(int id)
        {
            return _db.Get<PoiDescriptionFr>(id);
        }
        public bool Equals(PoiDescriptionFr object1, PoiDescriptionFr object2)
        {
            if (object1.ID == object2.ID &&
                object1.title == object2.title &&
                object1.summary == object2.summary)
                return true;
            else return false;
        }
    }
}