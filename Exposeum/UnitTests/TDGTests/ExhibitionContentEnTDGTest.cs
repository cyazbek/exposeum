
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class ExhibitionContentEnTDGTest
    {
        public readonly ExhibitionContentEn _setObject = new ExhibitionContentEn();
        public ExhibitionContentEn _testObject;
        public readonly ExhibitionContentEnTDG _objectTDG = ExhibitionContentEnTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().getConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.description = "description";
            _setObject.filepath = "filepath";
            _setObject.duration = 1;
            _setObject.resolution = 560;
            _setObject.encoding = "encoding";
            _setObject.discriminator = "discriminator";
        }

        [Test()]
        public void AddExhibitionContentEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<ExhibitionContentEn>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetExhibitionContentEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetExhibitionContentEn(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateExhibitionContentEnTest()
        {
            _testObject.ID = 1;
            _testObject.title = "title";
            _testObject.description = "description";
            _testObject.filepath = "filepath";
            _testObject.duration = 1;
            _testObject.resolution = 560;
            _testObject.encoding = "encoding";
            _testObject.discriminator = "discriminator";
            _objectTDG.Add(_testObject);
            _testObject.duration = 0;
            double duration = _testObject.duration;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetExhibitionContentEn(_testObject.ID);
            Assert.AreEqual(_testObject.duration, duration);
        }
    }
}