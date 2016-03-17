
using Exposeum.Models;
using Exposeum.TDGs;
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

            UUID uuid = UUID.FromString(beaconTable.UUID);
            int minor = beaconTable.minor;
            int major = beaconTable.major;
            int id = beaconTable.ID;

            Beacon beaconModel = new Beacon(uuid, major, minor) {Id = id};

            return beaconModel;
        }

        public Tables.Beacon BeaconModelToTable(Beacon beacon)
        {
            int id = beacon.Id;
            string uuid = beacon.Uuid.ToString();
            int major = beacon.Major;
            int minor = beacon.Minor;

            Tables.Beacon beaconTable = new Tables.Beacon
            {
                ID = id,
                UUID = uuid,
                major = major,
                minor = minor
            };

            return beaconTable;
        }

    }
}