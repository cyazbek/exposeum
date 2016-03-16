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
    }
}