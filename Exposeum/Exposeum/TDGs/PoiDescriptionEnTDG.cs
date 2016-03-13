using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class PoiDescriptionEnTDG:TDG
    {
        private static PoiDescriptionEnTDG _instance;

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
    }
}