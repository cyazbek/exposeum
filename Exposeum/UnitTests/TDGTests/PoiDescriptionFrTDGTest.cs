
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
        public readonly PoiDescriptionFrTdg _objectTDG = PoiDescriptionFrTdg.GetInstance();
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
        public void AddPoiDescriptionFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<PoiDescriptionFr>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetPoiDescriptionFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetPoiDescriptionFr(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdatePoiDescriptionFrTest()
        {
            _testObject = new PoiDescriptionFr();
            _setObject.Id = 1;
            _setObject.Title = "title";
            _setObject.Summary = "summary";
            _setObject.Description = "description";
            _objectTDG.Add(_testObject);
            _testObject.Title = "title2";
            string title = _testObject.Title;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetPoiDescriptionFr(_testObject.Id);
            Assert.AreEqual(_testObject.Title, title);
        }
    }
}