using Exposeum.Tables;
using System.Collections.Generic;

namespace Exposeum.TDGs
{
    public class ExhibitionContentEnTdg : Tdg
    {
        private static ExhibitionContentEnTdg _instance;

        private ExhibitionContentEnTdg() { }

        public static ExhibitionContentEnTdg GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentEnTdg();

            return _instance;
        }

        public void Add(ExhibitionContentEn item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(ExhibitionContentEn item)
        {
            Db.Update(item);
        }

        public ExhibitionContentEn GetExhibitionContentEn(int id)
        {
            return Db.Get<ExhibitionContentEn>(id);
        }

        public List<int> GetExhibitionContentEnByStorylineId(int id)
        {
            List<ExhibitionContentEn> exhibitionContent = new List<ExhibitionContentEn>(Db.Table<ExhibitionContentEn>());
            List<int> exhibitionId = new List<int>();

            foreach (var x in exhibitionContent)
            {
                if (x.StoryLineId == id)
                    exhibitionId.Add(x.Id);
            }

            return exhibitionId;
        }

        public bool Equals(ExhibitionContentEn object1, ExhibitionContentEn object2)
        {
            if (object1.Id == object2.Id &&
            object1.Title == object2.Title &&
            object1.Description == object2.Description &&
            object1.Filepath == object2.Filepath &&
            object1.Duration == object2.Duration &&
            object1.Resolution == object2.Resolution &&
            object1.Encoding == object2.Encoding &&
            object1.Discriminator == object2.Discriminator)
                return true;
            return false;
        }
    }
}