
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
        public readonly MapElementsTdg _objectTDG = MapElementsTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        private readonly List<MapElements> _listOfEdgesTable = new List<MapElements>();

        [SetUp]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.UCoordinate = 12f;
            _setObject.VCoordinate = 15.2f;
            _setObject.Discriminator = "POI";
            _setObject.Visited = 1;
            _setObject.BeaconId = 12;
            _setObject.StoryLineId = 1;
            _setObject.PoiDescription = 1;
            _setObject.Label = "bathroom";
            _setObject.ExhibitionContent = 1;
            _setObject.FloorId = 1;
        }

        [Test]
        public void AddMapElementsTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<MapElements>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }

        [Test]
        public void GetMapElementsTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetMapElement(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }

        [Test]
        public void UpdateMapElementsTest()
        {
            _testObject.Id = 1;
            _testObject.UCoordinate = 12f;
            _testObject.VCoordinate = 15.2f;
            _testObject.Discriminator = "POI";
            _testObject.Visited = 1;
            _testObject.BeaconId = 12;
            _testObject.StoryLineId = 2;
            _testObject.PoiDescription = 1;
            _testObject.Label = "bathroom";
            _testObject.ExhibitionContent = 1;
            _testObject.FloorId = 1;
            _objectTDG.Add(_testObject);
            _testObject.UCoordinate = 0;
            double uCoordinate = _testObject.UCoordinate;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetMapElement(_testObject.Id);
            Assert.AreEqual(_testObject.UCoordinate, uCoordinate);
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