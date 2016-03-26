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
        readonly BeaconTdg _tdg = BeaconTdg.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = BeaconMapper.GetInstance();
            _tdg.Db.DeleteAll<Beacon>();

            _beaconModel = new Exposeum.TempModels.Beacon
            {
                Id = 1,
                Major = 123,
                Minor = 123,
                Uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            _beaconTable = new Beacon
            {
                Id = 1,
                Major = 123,
                Minor = 123,
                Uuid = "b9407f30-f5f8-466e-aff9-25556b57fe6d"
            };

        }

        [Test]
        public void GetInstanceBeaconMapperTest()
        {
            Assert.NotNull(BeaconMapper.GetInstance());
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
            _beaconModel = new Exposeum.TempModels.Beacon
            {
                Id = 2,
                Major = 123,
                Minor = 123,
                Uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            _instance.AddBeacon(_beaconModel);

            _beaconModel.Major = 543;
            _instance.UpdateBeacon(_beaconModel);

            _expected = _instance.GetBeacon(_beaconModel.Id);
            Assert.AreEqual(543, _expected.Major);
        }

        [Test]
        public void BeaconModelToTableTest()
        {
            Beacon expected = _instance.BeaconModelToTable(_beaconModel);
            Assert.IsTrue(BeaconTdg.GetInstance().Equals(_beaconTable, expected));
        }

        [Test()]
        public void BeaconTableToModelTest()
        {
            Exposeum.TempModels.Beacon expected = _instance.BeaconTableToModel(_beaconTable);
            Assert.IsTrue(_beaconModel.Equals(expected));
        }

    }
}