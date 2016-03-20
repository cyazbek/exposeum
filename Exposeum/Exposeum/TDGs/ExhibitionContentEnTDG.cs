using Exposeum.Tables;
using System.Collections.Generic;

namespace Exposeum.TDGs
{
    public class ExhibitionContentEnTDG : ExhibitionContentTDG
    {
        private static ExhibitionContentEnTDG _instance;

        private ExhibitionContentEnTDG() { }

        public static ExhibitionContentEnTDG GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentEnTDG();

            return _instance;
        }

        public void Add(ExhibitionContentEn item)
        {
            _db.InsertOrReplace(item);
        }

        public void Update(ExhibitionContentEn item)
        {
            _db.Update(item);
        }

        public ExhibitionContentEn GetExhibitionContentEn(int id)
        {
            return _db.Get<ExhibitionContentEn>(id);
        }

        public List<int> GetExhibitionContentEnByStorylineId(int id)
        {
            List<ExhibitionContentEn> exhibitionContent = new List<ExhibitionContentEn>(_db.Table<ExhibitionContentEn>());
            List<int> exhibitionId = new List<int>();

            foreach (var x in exhibitionContent)
            {
                if (x.storyLineId == id)
                    exhibitionId.Add(x.ID);
            }

            return exhibitionId;
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