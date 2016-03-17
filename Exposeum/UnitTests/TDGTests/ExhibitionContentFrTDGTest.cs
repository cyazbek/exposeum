
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class ExhibitionContentFrTDGTest
    {
        public readonly ExhibitionContentFr _setObject = new ExhibitionContentFr();
        public ExhibitionContentFr _testObject;
        public readonly ExhibitionContentFrTDG _objectTDG = ExhibitionContentFrTDG.GetInstance(); 
        public SQLiteConnection _db = DBManager.GetInstance().GetConnection();
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
        public void AddExhibitionContentFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<ExhibitionContentFr>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetExhibitionContentFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetExhibitionContentFr(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateExhibitionContentFrTest()
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
            _testObject = _objectTDG.GetExhibitionContentFr(_testObject.ID);
            Assert.AreEqual(_testObject.duration, duration);
        }
    }
}