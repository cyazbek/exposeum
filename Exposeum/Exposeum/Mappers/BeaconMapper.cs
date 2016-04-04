using Exposeum.TDGs;
using Exposeum.Models;
using Java.Util;

namespace Exposeum.Mappers
{
    public class BeaconMapper
    {
        private static BeaconMapper _instance;
        private readonly BeaconTdg _beaconTdg;

        private BeaconMapper()
        {
            _beaconTdg = BeaconTdg.GetInstance();
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
                Id = beacon.Id,
                Uuid = beacon.Uuid.ToString(),
                Major = beacon.Major,
                Minor = beacon.Minor
            };

            return beaconTable;
        }

        public Beacon BeaconTableToModel(Tables.Beacon beacon)
        {
            Beacon beaconModel = new Beacon
            {
                Id = beacon.Id,
                Major = beacon.Major,
                Minor = beacon.Minor,
                Uuid = UUID.FromString(beacon.Uuid)
            };
            return beaconModel;
        }

    }
}