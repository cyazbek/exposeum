using Exposeum.Tables;


namespace Exposeum.TDGs
{
    class StorylineTDG:TDG
    {
        private static StorylineTDG _instance;

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
    }
}