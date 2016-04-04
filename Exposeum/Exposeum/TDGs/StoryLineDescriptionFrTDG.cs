using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class StoryLineDescriptionFrTdg : Tdg
    {
        private static StoryLineDescriptionFrTdg _instance;

        private StoryLineDescriptionFrTdg() { }

        public static StoryLineDescriptionFrTdg GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineDescriptionFrTdg();
            return _instance;
        }

        public void Add(StoryLineDescriptionFr item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(StoryLineDescriptionFr item)
        {
            Db.Update(item);
        }

        public StoryLineDescriptionFr GetStoryLineDescriptionFr(int id)
        {
            return Db.Get<StoryLineDescriptionFr>(id);
        }
        public bool Equals(StoryLineDescriptionFr desc1, StoryLineDescriptionFr desc2)
        {
            if (desc1.Id == desc2.Id && desc1.Title == desc2.Title && desc1.Description == desc2.Description && desc1.IntendedAudience == desc2.IntendedAudience)
                return true;
            return false;
        }
    }
}