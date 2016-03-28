
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
        public readonly StorylineTdg _objectTDG = StorylineTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.Audience = "audience";
            _setObject.Duration = 120;
            _setObject.ImagePath = "path";
            _setObject.FloorsCovered = 3;
            _setObject.LastVisitedPoi = 5;
            _setObject.Status = 0;
            _setObject.DescriptionId = 5;
        }

        [Test]
        public void GetInstanceStorylineTdgTest()
        {
            Assert.NotNull(StorylineTdg.GetInstance());
        }

        [Test()]
        public void AddStorylineTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<Storyline>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetStorylineTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetStoryline(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void UpdateStorylineTest()
        {
            _setObject.Id = 1;
            _setObject.Audience = "audience";
            _setObject.Duration = 120;
            _setObject.ImagePath = "path";
            _setObject.FloorsCovered = 3;
            _setObject.LastVisitedPoi = 5;
            _setObject.Status = 0;
            _setObject.DescriptionId = 5;
            _objectTDG.Add(_testObject);
            _testObject.Status = 0;
            int status = _testObject.Status;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetStoryline(_testObject.Id);
            Assert.AreEqual(_testObject.Status, status);
        }
    }
}