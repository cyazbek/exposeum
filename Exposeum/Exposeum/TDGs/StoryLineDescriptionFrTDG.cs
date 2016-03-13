using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class StoryLineDescriptionFrTDG:TDG
    {
        private static StoryLineDescriptionFrTDG _instance;

        public static StoryLineDescriptionFrTDG GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineDescriptionFrTDG();
            return _instance;
        }
        public void Add(StoryLineDescriptionFr item)
        {
            _db.Insert(item);
        }
        public void Update(StoryLineDescriptionFr item)
        {
            _db.Update(item);
        }
    }
}