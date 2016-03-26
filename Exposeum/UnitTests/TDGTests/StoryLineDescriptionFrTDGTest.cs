
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
        public readonly StoryLineDescriptionFrTdg _objectTDG = StoryLineDescriptionFrTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.Title = "title";
            _setObject.Description = "description";
        }

        [Test]
        public void GetInstanceStorylineDescrptionFrTdgTest()
        {
            Assert.NotNull(StoryLineDescriptionFrTDG.GetInstance());
        }

        [Test()]
        public void AddStoryLineDescriptionFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<StoryLineDescriptionFr>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetStoryLineDescriptionFrTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetStoryLineDescriptionFr(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateStoryLineDescriptionFrTest()
        {
            _testObject = new StoryLineDescriptionFr();
            _setObject.Id = 1;
            _setObject.Title = "title";
            _setObject.Description = "description";
            _objectTDG.Add(_testObject);
            _testObject.Title = "title2";
            string title = _testObject.Title;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetStoryLineDescriptionFr(_testObject.Id);
            Assert.AreEqual(_testObject.Title, title);
        }
    }
}