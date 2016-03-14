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

        public void Add(Beacon item)
        {
            _db.Insert(item);
        }
        public void Update(Beacon item)
        {
            _db.Update(item);
        }

        public Beacon GetBeacon(int id)
        {
            return _db.Get<Beacon>(id);
        }
    }
}