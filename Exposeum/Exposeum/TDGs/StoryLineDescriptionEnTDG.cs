using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class StoryLineDescriptionEnTDG : TDG
    {
        private static StoryLineDescriptionEnTDG _instance;

        private StoryLineDescriptionEnTDG() { }

        public static StoryLineDescriptionEnTDG GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineDescriptionEnTDG();
            return _instance;
        }

        public void Add(StoryLineDescriptionEnMapper item)
        {
            _db.InsertOrReplace(item);
        }

        public void Update(StoryLineDescriptionEnMapper item)
        {
            _db.Update(item);
        }

        public StoryLineDescriptionEnMapper GetStoryLineDescriptionEn(int id)
        {
            return _db.Get<StoryLineDescriptionEnMapper>(id);
        }
        public bool Equals(StoryLineDescriptionEnMapper desc1, StoryLineDescriptionEnMapper desc2)
        {
            if (desc1.ID == desc2.ID && desc1.title == desc2.title && desc1.description==desc2.description && desc1.description == desc2.description)
                return true;
            else return false; 
        }
    }
}