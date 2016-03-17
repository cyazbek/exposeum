
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class StorylineTDGTest
    {
        public readonly Storyline _setObject = new Storyline();
        public Storyline _testObject;
        public readonly StorylineTDG _objectTDG = StorylineTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().getConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.audience = "audience";
            _setObject.duration = 120;
            _setObject.image = 120;
            _setObject.floorsCovered = 3;
            _setObject.lastVisitedPoi = 5;
            _setObject.status = "visited";
            _setObject.descriptionId = 5;
        }

        [Test()]
        public void AddStorylineTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<Storyline>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetStorylineTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetStoryline(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateStorylineTest()
        {
            _setObject.ID = 1;
            _setObject.audience = "audience";
            _setObject.duration = 120;
            _setObject.image = 120;
            _setObject.floorsCovered = 3;
            _setObject.lastVisitedPoi = 5;
            _setObject.status = "visited";
            _setObject.descriptionId = 5;
            _objectTDG.Add(_testObject);
            _testObject.status = "new";
            string status = _testObject.status;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetStoryline(_testObject.ID);
            Assert.AreEqual(_testObject.status, status);
        }
    }
}