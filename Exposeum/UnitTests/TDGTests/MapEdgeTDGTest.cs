
using System.Collections.Generic;
using Android.Provider;
using Android.Widget;
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using Java.IO;
using SQLite;
using Console = System.Console;

namespace UnitTests
{
    [TestFixture]
    public class MapEdgeTDGTest
    {
        public readonly MapEdge _setObject = new MapEdge();
        public MapEdge _testObject;
        public readonly MapEdgeTdg _objectTDG = MapEdgeTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        private readonly List<MapEdge> _listOfEdgesTable = new List<MapEdge>();

        [Test]
        public void GetInstanceEdgeTdgTest()
        {
            Assert.NotNull(BeaconTdg.GetInstance());
        }

        [SetUp]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.Distance = 12.5;
            _setObject.StartMapElementId = 12345;
            _setObject.EndMapElementId = 12345;
        }

        [Test]
        public void AddEdgeTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<MapEdge>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject,_setObject));
        }

        [Test]
        public void GetEdgeTest()
        {
            _objectTDG.Add(_setObject);            
            _testObject = _objectTDG.GetEdge(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }

        [Test]
        public void UpdateEdgeTest()
        {
            _testObject = new MapEdge();
            _testObject.Id = 1;
            _testObject.Distance = 12.5;
            _testObject.StartMapElementId = 12345;
            _testObject.EndMapElementId = 12345;
            _objectTDG.Add(_testObject);
            _testObject.Distance = 0;
            double distance = _testObject.Distance;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetEdge(_testObject.Id);
            Assert.AreEqual(_testObject.Distance, distance);
        }

        [Test]
        public void GetAllEdgesTest()
        {
            int listOfEdgesSize = _db.Table<MapEdge>().Count();

            for (int i = 0; i < listOfEdgesSize; i++)
            {
                _listOfEdgesTable.Add(new MapEdge());
            }

            Assert.AreEqual(_listOfEdgesTable.Count, _objectTDG.GetAllEdges().Count);
        }
        
    }
}