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
    }
}