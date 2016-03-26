
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
        public readonly FloorTdg _objectTDG = FloorTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.ImageId = 12;
        }

        [Test]
        public void GetInstanceFloorTdgTest()
        {
            Assert.NotNull(FloorTdg.GetInstance());
        }

        [Test()]
        public void AddFloorTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<Floor>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetFloorTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetFloor(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateFloorTest()
        {
            _testObject = new Floor();
            _testObject.Id = 1;
            _testObject.ImageId = 12;
            _objectTDG.Add(_testObject);
            _testObject.ImageId = 0;
            int imageId = _testObject.ImageId;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetFloor(_testObject.Id);
            Assert.AreEqual(_testObject.ImageId, imageId);
        }
    }
}