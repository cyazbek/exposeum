
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class MapTDGTest
    {
        private readonly Map _setObject = new Map();
        private Map _testObject;
        private readonly MapTdg _mapTdg = MapTdg.GetInstance();
        private readonly SQLiteConnection _db = DbManager.GetInstance().GetConnection();

        [SetUp]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.CurrentFloorId = 1;
            _setObject.CurrentStoryLineId = 1;
        }

        [Test]
        public void GetInstanceMapTdgTest()
        {
            Assert.NotNull(MapTdg.GetInstance());
        }

        [Test]
        public void AddMapTest()
        {
            _mapTdg.AddMap(_setObject);
            _testObject = _db.Get<Map>(_setObject.Id);
            Assert.IsTrue(_mapTdg.Equals(_testObject, _setObject));
        }

        [Test]
        public void GetMapTest()
        {
            _mapTdg.AddMap(_setObject);
            _testObject = _mapTdg.GetMap(_setObject.Id);
            Assert.IsTrue(_mapTdg.Equals(_testObject, _setObject));
        }

        [Test]
        public void UpdateMapTest()
        {
            _testObject = new Map
            {
                Id = 3,
                CurrentFloorId = 2,
                CurrentStoryLineId = 2
            };

            _mapTdg.AddMap(_testObject);

            _testObject.CurrentFloorId = 5;
            _mapTdg.UpdateMap(_testObject);

            _testObject = _mapTdg.GetMap(_testObject.Id);
            Assert.AreEqual(5, _testObject.CurrentFloorId);
        }
    }
}