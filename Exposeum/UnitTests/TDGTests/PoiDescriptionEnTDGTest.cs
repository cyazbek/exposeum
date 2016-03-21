
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
        public readonly PoiDescriptionEnTdg _objectTDG = PoiDescriptionEnTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.Title = "title";
            _setObject.Summary = "summary";
            _setObject.Description = "description";
        }

        [Test()]
        public void AddPoiDescriptionEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<PoiDescriptionEn>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetPoiDescriptionEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetPoiDescriptionEn(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdatePoiDescriptionEnTest()
        {
            _testObject = new PoiDescriptionEn();
            _setObject.Id = 1;
            _setObject.Title = "title";
            _setObject.Summary = "summary";
            _setObject.Description = "description";
            _objectTDG.Add(_testObject);
            _testObject.Title = "title2";
            string title = _testObject.Title;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetPoiDescriptionEn(_testObject.Id);
            Assert.AreEqual(_testObject.Title, title);
        }
    }
}