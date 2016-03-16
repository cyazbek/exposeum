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
        public bool Equals(ExhibitionContentEn object1, ExhibitionContentEn object2)
        {
            if (object1.ID == object2.ID &&
            object1.title == object2.title &&
            object1.description == object2.description &&
            object1.filepath == object2.filepath &&
            object1.duration == object2.duration &&
            object1.resolution == object2.resolution &&
            object1.encoding == object2.encoding &&
            object1.discriminator == object2.discriminator)
                return true;
            else return false;
        }
    }
}