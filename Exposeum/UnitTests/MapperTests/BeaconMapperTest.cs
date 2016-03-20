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
        readonly BeaconTDG _tdg = BeaconTDG.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = BeaconMapper.GetInstance();
            _tdg._db.DeleteAll<Beacon>();

            _beaconModel = new Exposeum.TempModels.Beacon
            {
                _id = 1,
                _major = 123,
                _minor = 123,
                _uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            _beaconTable = new Beacon
            {
                ID = 1,
                major = 123,
                minor = 123,
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
                _major = 123,
                _minor = 123,
                _uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            _instance.AddBeacon(_beaconModel);

            _beaconModel._major = 543;
            _instance.UpdateBeacon(_beaconModel);

            _expected = _instance.GetBeacon(_beaconModel._id);
            Assert.AreEqual(543, _expected._major);
        }

        [Test]
        public void BeaconModelToTableTest()
        {
            Beacon expected = _instance.BeaconModelToTable(_beaconModel);
            Assert.IsTrue(BeaconTDG.GetInstance().Equals(_beaconTable, expected));
        }

        [Test()]
        public void BeaconTableToModelTest()
        {
            Exposeum.TempModels.Beacon expected = _instance.BeaconTableToModel(_beaconTable);
            Assert.IsTrue(_beaconModel.Equals(expected));
        }

    }
}