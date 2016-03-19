
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class StoryLineDescriptionFrTDGTest
    {
        public readonly StoryLineDescriptionFr _setObject = new StoryLineDescriptionFr();
        public StoryLineDescriptionFr _testObject;
        public readonly StoryLineDescriptionFrTDG _objectTDG = StoryLineDescriptionFrTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().getConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.description = "description";
        }

        [Test()]
        public void AddStoryLineDescriptionFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<StoryLineDescriptionFr>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetStoryLineDescriptionFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetStoryLineDescriptionFr(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateStoryLineDescriptionFrTest()
        {
            _testObject = new StoryLineDescriptionFr();
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.description = "description";
            _objectTDG.Add(_testObject);
            _testObject.title = "title2";
            string title = _testObject.title;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetStoryLineDescriptionFr(_testObject.ID);
            Assert.AreEqual(_testObject.title, title);
        }
    }
}