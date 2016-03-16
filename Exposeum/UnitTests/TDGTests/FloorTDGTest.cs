
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class FloorTDGTest
    {
        public readonly Floor _setObject = new Floor();
        public Floor _testObject;
        public readonly FloorTDG _objectTDG = FloorTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().getConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.imageId = 12;
        }

        [Test()]
        public void AddBeaconTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<Floor>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetBeaconTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetFloor(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateBeaconTest()
        {
            _testObject = new Floor();
            _testObject.ID = 1;
            _testObject.imageId = 12;
            _objectTDG.Add(_testObject);
            _testObject.imageId = 0;
            int imageId = _testObject.imageId;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetFloor(_testObject.ID);
            Assert.AreEqual(_testObject.imageId, imageId);
        }
    }
}