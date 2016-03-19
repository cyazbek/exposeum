using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class ExhibitionContentFrTDG : TDG
    {
        private static ExhibitionContentFrTDG _instance;

        private ExhibitionContentFrTDG() { }

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

        public bool Equals(ExhibitionContentFr object1, ExhibitionContentFr object2)
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