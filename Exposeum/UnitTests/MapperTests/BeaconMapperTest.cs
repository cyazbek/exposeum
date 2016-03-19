using Exposeum.Mappers;
using Exposeum.TDGs;
using Java.Util;
using NUnit.Framework;
using Beacon = Exposeum.Tables.Beacon;

namespace UnitTests
{
    [TestFixture]
    public class BeaconMapperTest
    {
        private static BeaconMapper _instance;
        private Exposeum.TempModels.Beacon _expected;
        private Exposeum.TempModels.Beacon _beaconModel;
        private Beacon _beaconTable;

        [SetUp]
        public void SetUp()
        {
            _instance = BeaconMapper.GetInstance();

            _beaconModel = new Exposeum.TempModels.Beacon
            {
                _id = 1,
                _major = 12345,
                _minor = 12345,
                _uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            _beaconTable = new Beacon
            {
                ID = 3,
                major = 3333,
                minor = 3333,
                UUID = "b9407f30-f5f8-466e-aff9-25556b57fe6d"
            };

        }

        [Test]
        public void AddBeaconTest()
        {
            _instance.AddBeacon(_beaconModel);
            _expected = _instance.GetBeacon(_beaconModel._id);
            Assert.True(_beaconModel.Equals(_expected));

        }

        [Test]
        public void UpdateBeaconTest()
        {
            _beaconModel = new Exposeum.TempModels.Beacon
            {
                _id = 2,
                _major = 12345,
                _minor = 12345,
                _uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            _instance.AddBeacon(_beaconModel);

            _beaconModel._major = 54321;
            _instance.UpdateBeacon(_beaconModel);

            _expected = _instance.GetBeacon(_beaconModel._id);
            Assert.AreEqual(54321, _expected._major);
        }

        [Test]
        public void BeaconModelToTableTest()
        {
            _beaconModel = new Exposeum.TempModels.Beacon
            {
                _id = 3,
                _major = 3333,
                _minor = 3333,
                _uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            Beacon expected = _instance.BeaconModelToTable(_beaconModel);

            Assert.IsTrue(BeaconTDG.GetInstance().Equals(_beaconTable, expected));
        }

        [Test()]
        public void BeaconTableToModelTest()
        {
            Exposeum.TempModels.Beacon modelBeacon = new Exposeum.TempModels.Beacon
            {
                _id = _beaconTable.ID,
                _major = _beaconTable.major,
                _minor = _beaconTable.minor,
                _uuid = UUID.FromString(_beaconTable.UUID)
            };
            Assert.IsTrue(modelBeacon.Equals(_instance.BeaconTableToModel(_beaconTable)));
        }

    }
}