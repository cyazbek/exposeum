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
    }
}