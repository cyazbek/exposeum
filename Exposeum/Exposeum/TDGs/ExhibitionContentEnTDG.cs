using SQLite;

namespace Exposeum.TDGs
{
    public class ExhibitionContentEnTDG : TDG
    {
        private static ExhibitionContentEnTDG _instance;

        public static ExhibitionContentEnTDG GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentEnTDG();

            return _instance;
        }
    }
}