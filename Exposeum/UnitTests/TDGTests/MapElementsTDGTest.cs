
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
    public class MapElementsTDGTest
    {
        public readonly MapElements _setObject = new MapElements();
        public MapElements _testObject;
        public readonly MapElementsTDG _objectTDG = MapElementsTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().GetConnection();
        private readonly List<MapElements> _listOfEdgesTable = new List<MapElements>();

        [SetUp]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.uCoordinate = 12f;
            _setObject.vCoordinate = 15.2f;
            _setObject.discriminator = "POI";
            _setObject.visited = 1;
            _setObject.beaconId = 12;
            _setObject._storyLineId = 1;
            _setObject.poiDescription = 1;
            _setObject.label = "bathroom";
            _setObject.exhibitionContent = 1;
            _setObject.floorId = 1;
        }

        [Test]
        public void AddMapElementsTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<MapElements>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }

        [Test]
        public void GetMapElementsTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetMapElement(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }

        [Test]
        public void UpdateMapElementsTest()
        {
            _testObject.ID = 1;
            _testObject.uCoordinate = 12f;
            _testObject.vCoordinate = 15.2f;
            _testObject.discriminator = "POI";
            _testObject.visited = 1;
            _testObject.beaconId = 12;
            _testObject._storyLineId = 2;
            _testObject.poiDescription = 1;
            _testObject.label = "bathroom";
            _testObject.exhibitionContent = 1;
            _testObject.floorId = 1;
            _objectTDG.Add(_testObject);
            _testObject.uCoordinate = 0;
            double uCoordinate = _testObject.uCoordinate;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetMapElement(_testObject.ID);
            Assert.AreEqual(_testObject.uCoordinate, uCoordinate);
        }

        [Test]
        public void GetAllMapElementsTest()
        {
            int listOfMapElementsSize = _db.Table<MapElements>().Count();

            for (int i = 0; i < listOfMapElementsSize; i++)
            {
                _listOfEdgesTable.Add(new MapElements());
            }

            Assert.AreEqual(_listOfEdgesTable.Count, _objectTDG.GetAllMapElements().Count);
        }

    }
}