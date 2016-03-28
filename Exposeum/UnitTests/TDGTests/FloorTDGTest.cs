
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class FloorTDGTest
    {
        private Floor _setObject;
        private Floor _testObject;
        private readonly FloorTdg _objectTdg = FloorTdg.GetInstance();
        private readonly SQLiteConnection _db = DbManager.GetInstance().GetConnection();

        [SetUp]
        public void Setup()
        {
            _setObject = new Floor
            {
                Id = 1,
                ImagePath = "EmileBerliner.png"
            };
        }

        [Test]
        public void GetInstanceFloorTdgTest()
        {
            Assert.NotNull(FloorTdg.GetInstance());
        }

        [Test()]
        public void AddFloorTest()
        {
            _objectTdg.Add(_setObject);
            _testObject = _db.Get<Floor>(_setObject.Id);
            Assert.IsTrue(_objectTdg.Equals(_testObject, _setObject));
        }

        [Test]
        public void GetFloorTest()
        {
            _objectTdg.Add(_setObject);
            _testObject = _objectTdg.GetFloor(_setObject.Id);
            Assert.IsTrue(_objectTdg.Equals(_testObject, _setObject));
        }

        [Test]
        public void UpdateFloorTest()
        {
            _testObject = new Floor
            {
                Id = 1,
                ImagePath = "thePath"
            };

            _objectTdg.Add(_testObject);

            _testObject.ImagePath = "thePathUpdated";
            _objectTdg.Update(_testObject);

            _testObject = _objectTdg.GetFloor(_testObject.Id);
            Assert.IsTrue("thePathUpdated".Equals(_testObject.ImagePath));
        }
    }
}