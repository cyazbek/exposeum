using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class ExhibitionContentFrTDG:TDG
    {
        private static ExhibitionContentFrTDG _instance;

        public static ExhibitionContentFrTDG GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentFrTDG();
            return _instance;
        }

        public void Add(ExhibitionContentFr item)
        {
            _db.Insert(item);
        }

        public void Update(ExhibitionContentFr item)
        {
            _db.Update(item);
        }
        public ExhibitionContentFr GetExhibitionContentFr(int id)
        {
            return _db.Get<ExhibitionContentFr>(id);
        }
    }
}