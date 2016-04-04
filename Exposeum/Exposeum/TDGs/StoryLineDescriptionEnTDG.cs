using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class StoryLineDescriptionEnTdg : Tdg
    {
        private static StoryLineDescriptionEnTdg _instance;

        private StoryLineDescriptionEnTdg() { }

        public static StoryLineDescriptionEnTdg GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineDescriptionEnTdg();
            return _instance;
        }

        public void Add(StoryLineDescriptionEn item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(StoryLineDescriptionEn item)
        {
            Db.Update(item);
        }

        public StoryLineDescriptionEn GetStoryLineDescriptionEn(int id)
        {
            return Db.Get<StoryLineDescriptionEn>(id);
        }
        public bool Equals(StoryLineDescriptionEn desc1, StoryLineDescriptionEn desc2)
        {
            if (desc1.Id == desc2.Id && desc1.Title == desc2.Title && desc1.Description==desc2.Description && desc1.IntendedAudience == desc2.IntendedAudience)
                return true;
            return false;
        }
    }
}