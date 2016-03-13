using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class BeaconTDG : TDG
    {
        private static BeaconTDG _instance;

        public static BeaconTDG GetInstance()
        {
            if (_instance == null)
                _instance = new BeaconTDG();

            return _instance;
        }
    }
}