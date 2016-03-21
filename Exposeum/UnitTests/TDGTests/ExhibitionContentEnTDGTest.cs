
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;
using System.Collections.Generic;

namespace UnitTests
{
    [TestFixture]
    public class ExhibitionContentEnTDGTest
    {
        public readonly ExhibitionContentEn _setObject = new ExhibitionContentEn();
        public readonly ExhibitionContentEn _setObject1 = new ExhibitionContentEn();
        public readonly ExhibitionContentEn _setObject2 = new ExhibitionContentEn();
        public readonly ExhibitionContentEn _setObject3 = new ExhibitionContentEn();
        public ExhibitionContentEn _testObject;
        public readonly ExhibitionContentEnTDG _objectTDG = ExhibitionContentEnTDG.GetInstance();
        public SQLiteConnection _db = DBManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.ID = 1;
            _setObject.title = "title";
            _setObject.description = "description";
            _setObject.filepath = "filepath";
            _setObject.duration = 1;
            _setObject.resolution = 560;
            _setObject.encoding = "encoding";
            _setObject.discriminator = "discriminator";
            _setObject.storyLineId = 1; 

            _setObject1.ID = 2;
            _setObject1.title = "title";
            _setObject1.description = "description";
            _setObject1.filepath = "filepath";
            _setObject1.duration = 1;
            _setObject1.resolution = 560;
            _setObject1.encoding = "encoding";
            _setObject1.discriminator = "discriminator";
            _setObject1.storyLineId = 1;

            _setObject2.ID = 3;
            _setObject2.title = "title";
            _setObject2.description = "description";
            _setObject2.filepath = "filepath";
            _setObject2.duration = 1;
            _setObject2.resolution = 560;
            _setObject2.encoding = "encoding";
            _setObject2.discriminator = "discriminator";
            _setObject2.storyLineId = 3;

            _setObject3.ID = 4;
            _setObject3.title = "title";
            _setObject3.description = "description";
            _setObject3.filepath = "filepath";
            _setObject3.duration = 1;
            _setObject3.resolution = 560;
            _setObject3.encoding = "encoding";
            _setObject3.discriminator = "discriminator";
            _setObject3.storyLineId = 2;
        }

        [Test]
        public void GetInstanceExhibitionContentEnTdgTest()
        {
            Assert.NotNull(ExhibitionContentEnTDG.GetInstance());
        }

        [Test()]
        public void AddExhibitionContentEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _db.Get<ExhibitionContentEn>(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetExhibitionContentEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetExhibitionContentEn(_setObject.ID);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }

        [Test()]
        public void GetExhibitionContentEnByStorylineIdTest()
        {
            _objectTDG.Add(_setObject);
            _objectTDG.Add(_setObject1);
            _objectTDG.Add(_setObject2);
            _objectTDG.Add(_setObject3);

            List<ExhibitionContentEn> numberOfEntries = _db.Query<ExhibitionContentEn>("SELECT * from ExhibitionContentEn where storyLineId = ?", _setObject.storyLineId);
            List<int> list = _objectTDG.GetExhibitionContentEnByStorylineId(_setObject.storyLineId);
            Assert.AreEqual(list.Count, numberOfEntries.Count);
        }

        [Test()]
        public void UpdateExhibitionContentEnTest()
        {
            _testObject.ID = 1;
            _testObject.title = "title";
            _testObject.description = "description";
            _testObject.filepath = "filepath";
            _testObject.duration = 1;
            _testObject.resolution = 560;
            _testObject.encoding = "encoding";
            _testObject.discriminator = "discriminator";
            _objectTDG.Add(_testObject);
            _testObject.duration = 0;
            double duration = _testObject.duration;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetExhibitionContentEn(_testObject.ID);
            Assert.AreEqual(_testObject.duration, duration);
        }
    }
}