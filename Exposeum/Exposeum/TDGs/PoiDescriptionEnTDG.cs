using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class PoiDescriptionEnTdg : Tdg
    {
        private static PoiDescriptionEnTdg _instance;

        private PoiDescriptionEnTdg() { }

        public static PoiDescriptionEnTdg GetInstance()
        {
            if (_instance == null)
                _instance = new PoiDescriptionEnTdg();
            return _instance;
        }

        public void Add(PoiDescriptionEn item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(PoiDescriptionEn item)
        {
            Db.Update(item);
        }

        public PoiDescriptionEn GetPoiDescriptionEn(int id)
        {
            return Db.Get<PoiDescriptionEn>(id);
        }
        public bool Equals(PoiDescriptionEn object1, PoiDescriptionEn object2)
        {
            if (object1.Id == object2.Id &&
                object1.Title == object2.Title &&
                object1.Summary == object2.Summary)
                return true;
            return false;
        }
    }
}