using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class ExhibitionContentEnTDG : TDG
    {
        private static ExhibitionContentEnTDG _instance;

        public static ExhibitionContentEnTDG GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentEnTDG();

            return _instance;
        }

        public void Add(ExhibitionContentEn item)
        {
            _db.Insert(item);
        }

        public void Update(ExhibitionContentEn item)
        {
            _db.Update(item);
        }
        public ExhibitionContentEn GetExhibitionContentEn(int id)
        {
            return _db.Get<ExhibitionContentEn>(id);
        }
    }
}