using SQLite;

namespace Exposeum.TDGs
{
    public class ImageTDG: TDG
    {
        private static ImageTDG _instance;

        public static ImageTDG GetInstance()
        {
            if (_instance == null)
                _instance = new ImageTDG();

            return _instance;
        }
    }
}