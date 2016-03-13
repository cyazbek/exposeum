using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class PoiDescriptionFrTDG:TDG
    {
        private static PoiDescriptionFrTDG _instance;

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
    }
}