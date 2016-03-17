
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class PoiDescriptionEnTDGTest
    {
        public readonly PoiDescriptionEn _setObject = new PoiDescriptionEn();
        public PoiDescriptionEn _testObject;
        public readonly PoiDescriptionEnTDG _objectTDG = PoiDescriptionEnTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().getConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.summary = "summary";
        }

        [Test()]
        public void AddPoiDescriptionEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<PoiDescriptionEn>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetPoiDescriptionEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetPoiDescriptionEn(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdatePoiDescriptionEnTest()
        {
            _testObject = new PoiDescriptionEn();
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.summary = "summary";
            _objectTDG.Add(_testObject);
            _testObject.title = "title2";
            string title = _testObject.title;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetPoiDescriptionEn(_testObject.ID);
            Assert.AreEqual(_testObject.title, title);
        }
    }
}