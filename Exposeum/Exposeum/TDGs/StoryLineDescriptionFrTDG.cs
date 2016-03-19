using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class StoryLineDescriptionFrTDG : TDG
    {
        private static StoryLineDescriptionFrTDG _instance;

        private StoryLineDescriptionFrTDG() { }

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

        public StoryLineDescriptionFr GetStoryLineDescriptionFr(int id)
        {
            return _db.Get<StoryLineDescriptionFr>(id);
        }
        public bool Equals(StoryLineDescriptionFr desc1, StoryLineDescriptionFr desc2)
        {
            if (desc1.ID == desc2.ID && desc1.title == desc2.title && desc1.description == desc2.description && desc1.description == desc2.description)
                return true;
            else return false;
        }
    }
}