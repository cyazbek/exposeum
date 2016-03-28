using Exposeum.Tables;
using System.Collections.Generic;

namespace Exposeum.TDGs
{
    public class StorylineTdg : Tdg
    {
        private static StorylineTdg _instance;

        private StorylineTdg() { }

        public static StorylineTdg GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineTdg();
            return _instance;
        }

        public void Add(Storyline item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(Storyline item)
        {
            Db.Update(item);
        }

        public Storyline GetStoryline(int id)
        {
            return Db.Get<Storyline>(id);
        }

        public List<Storyline> GetAllStorylines()
        {
            return new List<Storyline>(Db.Table<Storyline>());
        }

        public bool Equals(Storyline story1, Storyline story2)
        {
            if (story1.Id == story2.Id &&
                story1.Duration == story2.Duration &&
                story1.ImagePath == story2.ImagePath &&
                story1.FloorsCovered == story2.FloorsCovered &&
                story1.LastVisitedPoi == story2.LastVisitedPoi &&
                story1.Status == story2.Status &&
                story1.DescriptionId == story2.DescriptionId) return true;
            return false;
        }
    }
}