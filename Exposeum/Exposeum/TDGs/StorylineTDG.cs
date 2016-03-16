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

        public Storyline GetStoryLine(int id)
        {
            return _db.Get<Storyline>(id);
        }
    }
}