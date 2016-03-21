
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
        public readonly ImagesTdg _objectTDG = ImagesTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.Path = "Path";
        }

        [Test()]
        public void AddImagesTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<Images>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetImagesTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetImages(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateImagesTest()
        {
            _testObject = new Images();
            _testObject.Id = 1;
            _testObject.Path = "path 2";
            _objectTDG.Add(_testObject);
            _testObject.Path = "path0";
            string imageId = _testObject.Path;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetImages(_testObject.Id);
            Assert.AreEqual(_testObject.Path, imageId);
        }
    }
}