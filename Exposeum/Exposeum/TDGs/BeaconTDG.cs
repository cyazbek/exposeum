using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class BeaconTdg : Tdg
    {
        private static BeaconTdg _instance;

        private BeaconTdg() { }

        public static BeaconTdg GetInstance()
        {
            if (_instance == null)
                _instance = new BeaconTdg();

            return _instance;
        }

        public void Add(Beacon item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(Beacon item)
        {
            Db.Update(item);
        }

        public Beacon GetBeacon(int id)
        {
            return Db.Get<Beacon>(id);
        }

        public bool Equals(Beacon beacon1, Beacon beacon2)
        {
            if (beacon1.Id == beacon2.Id && beacon1.Uuid.Equals(beacon2.Uuid) && beacon1.Minor == beacon2.Minor && beacon1.Major == beacon2.Major)
                return true;
            return false;
        }
    }
}