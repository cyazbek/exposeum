using Exposeum.Tables;

namespace Exposeum.TDGs
{
    class PoiDescriptionFrTDG:TDG
    {
        private static PoiDescriptionFrTDG _instance;

        public static PoiDescriptionFrTDG GetInstance()
        {
            if (_instance == null)
                _instance = new PoiDescriptionFrTDG();
            return _instance;
        }
    }
}