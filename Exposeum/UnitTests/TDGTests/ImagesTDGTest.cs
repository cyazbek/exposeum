
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class ImagesTDGTest
    {
        public readonly Images _setObject = new Images();
        public Images _testObject;
        public readonly ImagesTDG _objectTDG = ImagesTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.path = "Path";
        }

        [Test()]
        public void AddImagesTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<Images>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetImagesTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetImages(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateImagesTest()
        {
            _testObject = new Images();
            _testObject.ID = 1;
            _testObject.path = "path 2";
            _objectTDG.Add(_testObject);
            _testObject.path = "path0";
            string imageId = _testObject.path;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetImages(_testObject.ID);
            Assert.AreEqual(_testObject.path, imageId);
        }
    }
}