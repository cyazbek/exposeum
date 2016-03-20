
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class StoryLineDescriptionEnTDGTest
    {
        public readonly StoryLineDescriptionEnMapper _setObject = new StoryLineDescriptionEnMapper();
        public StoryLineDescriptionEnMapper _testObject;
        public readonly StoryLineDescriptionEnTDG _objectTDG = StoryLineDescriptionEnTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.description = "description";
        }

        [Test()]
        public void AddStoryLineDescriptionEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<StoryLineDescriptionEnMapper>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetStoryLineDescriptionEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetStoryLineDescriptionEn(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateStoryLineDescriptionEnTest()
        {
            _testObject = new StoryLineDescriptionEnMapper();
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.description = "description";
            _objectTDG.Add(_testObject);
            _testObject.title = "title2";
            string title = _testObject.title;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetStoryLineDescriptionEn(_testObject.ID);
            Assert.AreEqual(_testObject.title, title);
        }
    }
}