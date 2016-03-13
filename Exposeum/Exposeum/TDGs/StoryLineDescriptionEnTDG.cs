using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class StoryLineDescriptionEnTDG:TDG
    {
        private static StoryLineDescriptionEnTDG _instance;

        public static StoryLineDescriptionEnTDG GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineDescriptionEnTDG();
            return _instance;
        }

        public void Add(StoryLineDescriptionEn item)
        {
            _db.Insert(item);
        }

        public void Update(StoryLineDescriptionEn item)
        {
            _db.Update(item);
        }

        public StoryLineDescriptionEn GetStoryLineDescriptionEn(int id)
        {
            return _db.Get<StoryLineDescriptionEn>(id);
        }
    }
}