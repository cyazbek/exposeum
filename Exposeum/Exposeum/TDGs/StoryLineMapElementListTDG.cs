using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class StoryLineMapElementListTDG:TDG
    {
        private static StoryLineMapElementListTDG _instance;

        public static StoryLineMapElementListTDG GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineMapElementListTDG();
            return _instance;
        }

        public void Add(StoryLineMapElementList item)
        {
            _db.Insert(item);
        }
        public void Update(StoryLineMapElementList item)
        {
            _db.Update(item);
        }

    }
}