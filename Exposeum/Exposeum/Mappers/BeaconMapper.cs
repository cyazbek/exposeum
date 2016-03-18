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

            Beacon beaconModel = new Beacon
            {
                _id = beaconTable.ID,
                _uuid = UUID.FromString(beaconTable.UUID),
                _major = beaconTable.major,
                _minor = beaconTable.minor

            };

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

    }
}