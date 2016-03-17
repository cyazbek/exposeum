
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class PoiDescriptionFrTDGTest
    {
        public readonly PoiDescriptionFr _setObject = new PoiDescriptionFr();
        public PoiDescriptionFr _testObject;
        public readonly PoiDescriptionFrTDG _objectTDG = PoiDescriptionFrTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.summary = "summary";
        }

        [Test()]
        public void AddPoiDescriptionFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<PoiDescriptionFr>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetPoiDescriptionFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetPoiDescriptionFr(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdatePoiDescriptionFrTest()
        {
            _testObject = new PoiDescriptionFr();
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.summary = "summary";
            _objectTDG.Add(_testObject);
            _testObject.title = "title2";
            string title = _testObject.title;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetPoiDescriptionFr(_testObject.ID);
            Assert.AreEqual(_testObject.title, title);
        }
    }
}