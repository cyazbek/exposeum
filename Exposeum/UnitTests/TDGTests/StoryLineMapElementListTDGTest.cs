
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;
using System.Collections.Generic;

namespace UnitTests
{
    [TestFixture]
    public class StoryLineMapElementListTDGTest
    {
        public readonly StoryLineMapElementList _setObject = new StoryLineMapElementList();
        public readonly StoryLineMapElementList _setObject2 = new StoryLineMapElementList();
        public readonly StoryLineMapElementList _setObject3 = new StoryLineMapElementList();
        public StoryLineMapElementList _testObject;
        public readonly StoryLineMapElementListTdg _objectTDG = StoryLineMapElementListTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.StoryLineId = 1;
            _setObject.MapElementId = 1;

            _setObject2.Id = 2;
            _setObject2.StoryLineId = 2;
            _setObject2.MapElementId = 2;

            _setObject3.Id = 3;
            _setObject3.StoryLineId = 3;
            _setObject3.MapElementId = 3;

        }

        [Test]
        public void GetInstanceStorylineMapElementListTdgTest()
        {
            Assert.NotNull(StoryLineMapElementListTdg.GetInstance());
        }

        [Test()]
        public void AddStoryLineMapElementListTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<StoryLineMapElementList>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetStoryLineMapElementListTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetStoryLineMapElementList(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetAllStoryLineMapElementListTest()
        {
            _objectTDG.Add(_setObject);
            _objectTDG.Add(_setObject2);
            _objectTDG.Add(_setObject3);
            _testObject = new StoryLineMapElementList();
            _testObject.Id = 4;
            _testObject.MapElementId = 4;
            _testObject.StoryLineId = 2;
            _objectTDG.Add(_testObject);
            List<StoryLineMapElementList> numberOfEntries = _db.Query<StoryLineMapElementList>("SELECT * from StoryLineMapElementList where storyLineId = ?", _testObject.StoryLineId);
            List<int> list = _objectTDG.GetAllStorylineMapElements(2);
            Assert.AreEqual(list.Count, numberOfEntries.Count);
        }
        [Test()]
        public void UpdateStoryLineMapElementListTest()
        {
            _objectTDG.Add(_setObject);
            _setObject.StoryLineId = -10;
            int storyLineId = _setObject.StoryLineId;
            _objectTDG.Update(_setObject);
            _testObject = _objectTDG.GetStoryLineMapElementList(_testObject.Id);
            Assert.AreEqual(_setObject.StoryLineId, storyLineId);
        }
    }
}