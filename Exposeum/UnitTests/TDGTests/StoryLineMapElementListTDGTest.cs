
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
        public readonly StoryLineMapElementListTDG _objectTDG = StoryLineMapElementListTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.storyLineId = 1;
            _setObject.mapElementId = 1;

            _setObject2.ID = 2;
            _setObject2.storyLineId = 2;
            _setObject2.mapElementId = 2;

            _setObject3.ID = 3;
            _setObject3.storyLineId = 3;
            _setObject3.mapElementId = 3;

        }

        [Test()]
        public void AddStoryLineMapElementListTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<StoryLineMapElementList>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetStoryLineMapElementListTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetStoryLineMapElementList(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetAllStoryLineMapElementListTest()
        {
            _objectTDG.Add(_setObject);
            _objectTDG.Add(_setObject2);
            _objectTDG.Add(_setObject3);
            _testObject = new StoryLineMapElementList();
            _testObject.ID = 4;
            _testObject.mapElementId = 4;
            _testObject.storyLineId = 2;
            _objectTDG.Add(_testObject);
            List<StoryLineMapElementList> numberOfEntries = _db.Query<StoryLineMapElementList>("SELECT * from StoryLineMapElementList where storyLineId = ?", _testObject.storyLineId);
            List<int> list = _objectTDG.GetAllStorylineMapElements(2);
            Assert.AreEqual(list.Count, numberOfEntries.Count);
        }
        [Test()]
        public void UpdateStoryLineMapElementListTest()
        {
            _objectTDG.Add(_setObject);
            _setObject.storyLineId = -10;
            int storyLineId = _setObject.storyLineId;
            _objectTDG.Update(_setObject);
            _testObject = _objectTDG.GetStoryLineMapElementList(_testObject.ID);
            Assert.AreEqual(_setObject.storyLineId, storyLineId);
        }
    }
}