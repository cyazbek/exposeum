using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class StorylineDescriptionListTDG:TDG
    {
        private static StorylineDescriptionListTDG _instance;

        public static StorylineDescriptionListTDG GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineDescriptionListTDG();
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