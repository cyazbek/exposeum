using Exposeum.Tables;


namespace Exposeum.TDGs
{
    public class StorylineTDG : TDG
    {
        private static StorylineTDG _instance;

        private StorylineTDG() { }

        public static StorylineTDG GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineTDG();
            return _instance;
        }

        public void Add(Storyline item)
        {
            _db.Insert(item);
        }

        public void Update(Storyline item)
        {
            _db.Update(item);
        }

        public Storyline GetStoryline(int id)
        {
            return _db.Get<Storyline>(id);
        }

        public bool Equals(Storyline story1, Storyline story2)
        {
            if (story1.ID == story2.ID &&
                story1.audience == story2.audience &&
                story1.duration == story2.duration &&
                story1.image == story2.image &&
                story1.floorsCovered == story2.floorsCovered &&
                story1.lastVisitedPoi == story2.lastVisitedPoi &&
                story1.status == story2.status &&
                story1.descriptionId == story2.descriptionId) return true;
            else return false; 
        }
    }
}