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

        public void Add(StoryLineDescriptionEn item)
        {
            _db.InsertOrReplace(item);
        }

        public void Update(StoryLineDescriptionEn item)
        {
            _db.Update(item);
        }

        public StoryLineDescriptionEn GetStoryLineDescriptionEn(int id)
        {
            return _db.Get<StoryLineDescriptionEn>(id);
        }
        public bool Equals(StoryLineDescriptionEn desc1, StoryLineDescriptionEn desc2)
        {
            if (desc1.ID == desc2.ID && desc1.title == desc2.title && desc1.description==desc2.description && desc1.description == desc2.description)
                return true;
            else return false; 
        }
    }
}