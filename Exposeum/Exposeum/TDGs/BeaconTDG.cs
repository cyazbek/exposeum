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
        public bool Equals(Beacon beacon1, Beacon beacon2)
        {
            if (beacon1.ID == beacon2.ID && beacon1.UUID.Equals(beacon2.UUID) && beacon1.minor == beacon2.minor && beacon1.major == beacon2.major)
                return true;
            else
                return false;
        }  
    }
}