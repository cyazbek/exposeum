using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class PoiDescriptionEnTDG : TDG
    {
        private static PoiDescriptionEnTDG _instance;

        private PoiDescriptionEnTDG() { }

        public static PoiDescriptionEnTDG GetInstance()
        {
            if (_instance == null)
                _instance = new PoiDescriptionEnTDG();
            return _instance;
        }

        public void Add(PoiDescriptionEn item)
        {
            _db.Insert(item);
        }

        public void Update(PoiDescriptionEn item)
        {
            _db.Update(item);
        }

        public PoiDescriptionEn GetPoiDescriptionEn(int id)
        {
            return _db.Get<PoiDescriptionEn>(id);
        }
        public bool Equals(PoiDescriptionEn object1, PoiDescriptionEn object2)
        {
            if (object1.ID == object2.ID &&
                object1.title == object2.title &&
                object1.summary == object2.summary)
                return true;
            else return false; 
        }
    }
}