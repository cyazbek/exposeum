
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class EdgeTDGTest
    {
        public readonly Edge _setObject = new Edge();
        public Edge _testObject;
        public readonly EdgeTDG _objectTDG = EdgeTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().getConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.distance = 12.5;
            _setObject.startMapElementId = 12345;
            _setObject.endMapElementId = 12345;
        }

        [Test()]
        public void AddBeaconTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<Edge>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject,_setObject));
        }
        [Test()]
        public void GetBeaconTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetEdge(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateBeaconTest()
        {
            _testObject = new Edge();
            _testObject.ID = 1;
            _testObject.distance = 12.5;
            _testObject.startMapElementId = 12345;
            _testObject.endMapElementId = 12345;
            _objectTDG.Add(_testObject);
            _testObject.distance = 0;
            double distance = _testObject.distance;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetEdge(_testObject.ID);
            Assert.AreEqual(_testObject.distance, distance);
        }
    }
}