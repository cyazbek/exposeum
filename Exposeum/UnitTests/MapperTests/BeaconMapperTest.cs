using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.Tables;
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
        private Exposeum.Models.Beacon _expected;
        private Exposeum.Models.Beacon _beaconModel;
        private Beacon _beaconTable;

        [SetUp]
        public void SetUp()
        {
            _instance = BeaconMapper.GetInstance();

            _beaconModel = new Exposeum.Models.Beacon
            {
                Id = 1,
                Major = 12345,
                Minor = 12345,
                Uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
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
            _expected = _instance.GetBeacon(_beaconModel.Id);
            Assert.True(_beaconModel.Equals(_expected));

        }

        [Test]
        public void UpdateBeaconTest()
        {
            _beaconModel = new Exposeum.Models.Beacon
            {
                Id = 2,
                Major = 12345,
                Minor = 12345,
                Uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            _instance.AddBeacon(_beaconModel);

            _beaconModel.Major = 54321;
            _instance.UpdateBeacon(_beaconModel);

            _expected = _instance.GetBeacon(_beaconModel.Id);
            Assert.AreEqual(54321, _expected.Major);
        }

        [Test]
        public void BeaconModelToTableTest()
        {
            _beaconModel = new Exposeum.Models.Beacon
            {
                Id = 3,
                Major = 3333,
                Minor = 3333,
                Uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            Beacon expected = _instance.BeaconModelToTable(_beaconModel);

            Assert.IsTrue(BeaconTDG.GetInstance().Equals(_beaconTable, expected));
        }

    }
}