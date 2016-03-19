using Exposeum.TDGs;
using Exposeum.TempModels;
using Java.Util;

namespace Exposeum.Mappers
{
    public class BeaconMapper
    {
        private static BeaconMapper _instance;
        private readonly BeaconTDG _beaconTdg;

        private BeaconMapper()
        {
            _beaconTdg = BeaconTDG.GetInstance();
        }

        public static BeaconMapper GetInstance()
        {
            if(_instance == null)
                _instance = new BeaconMapper();

            return _instance;
        }

        public void AddBeacon(Beacon beacon)
        {
            Tables.Beacon beaconTable = BeaconModelToTable(beacon);
            _beaconTdg.Add(beaconTable);
        }

        public void UpdateBeacon(Beacon beacon)
        {
            Tables.Beacon beaconTable = BeaconModelToTable(beacon);
            _beaconTdg.Update(beaconTable);
        }

        public Beacon GetBeacon(int beaconId)
        {
            Tables.Beacon beaconTable = _beaconTdg.GetBeacon(beaconId);
            Beacon beaconModel = BeaconTableToModel(beaconTable);
            return beaconModel;
        }

        public Tables.Beacon BeaconModelToTable(Beacon beacon)
        {
            Tables.Beacon beaconTable = new Tables.Beacon
            {
                ID = beacon._id,
                UUID = beacon._uuid.ToString(),
                major = beacon._major,
                minor = beacon._minor
            };

            return beaconTable;
        }

        public Beacon BeaconTableToModel(Tables.Beacon beacon)
        {
            Beacon beaconModel = new Beacon
            {
                _id = beacon.ID,
                _major = beacon.major,
                _minor = beacon.minor,
                _uuid = UUID.FromString(beacon.UUID)
            };
            return beaconModel;
        }

        public bool Equals(Beacon beacon1, Beacon beacon2)
        {
            if (beacon1._id == beacon2._id &&
                beacon1._major == beacon2._major &&
                beacon1._minor == beacon2._minor &&
                beacon1._uuid.Equals(beacon2._uuid))
                return true;
            else return false; 
        }

    }
}