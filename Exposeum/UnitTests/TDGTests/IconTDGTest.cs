
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
        public readonly IconTdg _objectTDG = IconTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.Path = "Path";
        }

        [Test]
        public void GetInstanceIconTdgTest()
        {
            Assert.NotNull(IconTdg.GetInstance());
        }

        [Test()]
        public void AddIconTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<Icon>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetIconTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetIcon(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateIconTest()
        {
            _testObject = new Icon();
            _testObject.Id = 1;
            _testObject.Path = "path 2";
            _objectTDG.Add(_testObject);
            _testObject.Path = "path0";
            string imageId = _testObject.Path;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetIcon(_testObject.Id);
            Assert.AreEqual(_testObject.Path, imageId);
        }
    }
}