
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class IconTDGTest
    {
        public readonly Icon _setObject = new Icon();
        public Icon _testObject;
        public readonly IconTDG _objectTDG = IconTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.path = "Path";
        }

        [Test()]
        public void AddIconTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<Icon>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetIconTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetIcon(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateIconTest()
        {
            _testObject = new Icon();
            _testObject.ID = 1;
            _testObject.path = "path 2";
            _objectTDG.Add(_testObject);
            _testObject.path = "path0";
            string imageId = _testObject.path;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetIcon(_testObject.ID);
            Assert.AreEqual(_testObject.path, imageId);
        }
    }
}