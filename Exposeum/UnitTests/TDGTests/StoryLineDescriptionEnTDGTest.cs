
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class StoryLineDescriptionEnTDGTest
    {
        public readonly StoryLineDescriptionEn _setObject = new StoryLineDescriptionEn();
        public StoryLineDescriptionEn _testObject;
        public readonly StoryLineDescriptionEnTdg _objectTDG = StoryLineDescriptionEnTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.Title = "title";
            _setObject.Description = "description";
        }

        [Test()]
        public void AddStoryLineDescriptionEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<StoryLineDescriptionEn>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetStoryLineDescriptionEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetStoryLineDescriptionEn(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateStoryLineDescriptionEnTest()
        {
            _testObject = new StoryLineDescriptionEn();
            _setObject.Id = 1;
            _setObject.Title = "title";
            _setObject.Description = "description";
            _objectTDG.Add(_testObject);
            _testObject.Title = "title2";
            string title = _testObject.Title;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetStoryLineDescriptionEn(_testObject.Id);
            Assert.AreEqual(_testObject.Title, title);
        }
    }
}