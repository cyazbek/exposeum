using Exposeum.Tables;
using System.Collections.Generic;

namespace Exposeum.TDGs
{
    public class ExhibitionContentFrTdg : ExhibitionContentTdg
    {
        private static ExhibitionContentFrTdg _instance;

        private ExhibitionContentFrTdg() { }

        public static ExhibitionContentFrTdg GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentFrTdg();
            return _instance;
        }

        public void Add(ExhibitionContentFr item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(ExhibitionContentFr item)
        {
            Db.Update(item);
        }

        public ExhibitionContentFr GetExhibitionContentFr(int id)
        {
            return Db.Get<ExhibitionContentFr>(id);
        }

        public List<int> GetExhibitionContentEnByStorylineId(int id)
        {
            List<ExhibitionContentFr> exhibitionContent = new List<ExhibitionContentFr>(Db.Table<ExhibitionContentFr>());
            List<int> exhibitionId = new List<int>();

            foreach (var x in exhibitionContent)
            {
                if (x.StoryLineId == id)
                    exhibitionId.Add(x.Id);
            }

            return exhibitionId;
        }

        public bool Equals(ExhibitionContentFr object1, ExhibitionContentFr object2)
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