
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
        public readonly BeaconTDG _beaconTDG = BeaconTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().getConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.UUID = "test";
            _setObject.minor = 12345;
            _setObject.major = 12345; 
        }
        
        [Test()]
        public void AddBeaconTest()
        {
            _beaconTDG.Add(_setObject);
            _testObject = _db.Get<Beacon>(_setObject.ID);
            Assert.IsTrue(_beaconTDG.Equals(_testObject,_setObject));
        }
        [Test()]
        public void GetBeaconTest()
        {
            _beaconTDG.Add(_setObject);
            _testObject = _beaconTDG.GetBeacon(_setObject.ID);
            Assert.IsTrue(_beaconTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateBeaconTest()
        {
            _testObject = new Beacon(); 
            _testObject.ID = 3;
            _testObject.UUID = "test";
            _testObject.major = 1234;
            _testObject.minor = 1234;
            _beaconTDG.Add(_testObject);
            _testObject.UUID = "test2";
            string uuid2 = _testObject.UUID;
            _beaconTDG.Update(_testObject);
            _testObject = _beaconTDG.GetBeacon(_testObject.ID); 
            Assert.AreEqual(_testObject.UUID, uuid2); 
        }
    }
}