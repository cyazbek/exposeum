
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class BeaconTDGTest
    {
        public readonly Beacon _setObject = new Beacon();
        public Beacon _testObject; 
        public readonly BeaconTdg _beaconTDG = BeaconTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.Uuid = "test";
            _setObject.Minor = 12345;
            _setObject.Major = 12345; 
        }

        [Test]
        public void GetInstanceBeaconTdgTest()
        {
            Assert.NotNull(BeaconTdg.GetInstance());    
        }

        [Test()]
        public void AddBeaconTest()
        {
            _beaconTDG.Add(_setObject);
            _testObject = _db.Get<Beacon>(_setObject.Id);
            Assert.IsTrue(_beaconTDG.Equals(_testObject,_setObject));
        }
        [Test()]
        public void GetBeaconTest()
        {
            _beaconTDG.Add(_setObject);
            _testObject = _beaconTDG.GetBeacon(_setObject.Id);
            Assert.IsTrue(_beaconTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateBeaconTest()
        {
            _testObject = new Beacon(); 
            _testObject.Id = 3;
            _testObject.Uuid = "test";
            _testObject.Major = 1234;
            _testObject.Minor = 1234;
            _beaconTDG.Add(_testObject);
            _testObject.Uuid = "test2";
            string uuid2 = _testObject.Uuid;
            _beaconTDG.Update(_testObject);
            _testObject = _beaconTDG.GetBeacon(_testObject.Id); 
            Assert.AreEqual(_testObject.Uuid, uuid2); 
        }
    }
}