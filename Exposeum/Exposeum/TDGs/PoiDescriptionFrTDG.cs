using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class PoiDescriptionFrTdg : Tdg
    {
        private static PoiDescriptionFrTdg _instance;

        private PoiDescriptionFrTdg() { }

        public static PoiDescriptionFrTdg GetInstance()
        {
            if (_instance == null)
                _instance = new PoiDescriptionFrTdg();
            return _instance;
        }

        public void Add(PoiDescriptionFr item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(PoiDescriptionFr item)
        {
            Db.Update(item);
        }

        public PoiDescriptionFr GetPoiDescriptionFr(int id)
        {
            return Db.Get<PoiDescriptionFr>(id);
        }
        public bool Equals(PoiDescriptionFr object1, PoiDescriptionFr object2)
        {
            if (object1.Id == object2.Id &&
                object1.Title == object2.Title &&
                object1.Summary == object2.Summary)
                return true;
            return false;
        }
    }
}