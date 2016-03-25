
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
        public readonly ExhibitionContentEnTdg _objectTDG = ExhibitionContentEnTdg.GetInstance();
        public SQLiteConnection _db = DbManager.GetInstance().GetConnection();
        [SetUp()]
        public void Setup()
        {
            _setObject.Id = 1;
            _setObject.Title = "title";
            _setObject.Description = "description";
            _setObject.Filepath = "filepath";
            _setObject.Duration = 1;
            _setObject.Resolution = 560;
            _setObject.Encoding = "encoding";
            _setObject.Discriminator = "discriminator";
            _setObject.StoryLineId = 1; 

            _setObject1.Id = 2;
            _setObject1.Title = "title";
            _setObject1.Description = "description";
            _setObject1.Filepath = "filepath";
            _setObject1.Duration = 1;
            _setObject1.Resolution = 560;
            _setObject1.Encoding = "encoding";
            _setObject1.Discriminator = "discriminator";
            _setObject1.StoryLineId = 1;

            _setObject2.Id = 3;
            _setObject2.Title = "title";
            _setObject2.Description = "description";
            _setObject2.Filepath = "filepath";
            _setObject2.Duration = 1;
            _setObject2.Resolution = 560;
            _setObject2.Encoding = "encoding";
            _setObject2.Discriminator = "discriminator";
            _setObject2.StoryLineId = 3;

            _setObject3.Id = 4;
            _setObject3.Title = "title";
            _setObject3.Description = "description";
            _setObject3.Filepath = "filepath";
            _setObject3.Duration = 1;
            _setObject3.Resolution = 560;
            _setObject3.Encoding = "encoding";
            _setObject3.Discriminator = "discriminator";
            _setObject3.StoryLineId = 2;
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
            _testObject = _db.Get<ExhibitionContentEn>(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }
        [Test()]
        public void GetExhibitionContentEnTest()
        {
            _objectTDG.Add(_setObject);
            _testObject = _objectTDG.GetExhibitionContentEn(_setObject.Id);
            Assert.IsTrue(_objectTDG.Equals(_testObject, _setObject));
        }

        [Test()]
        public void GetExhibitionContentEnByStorylineIdTest()
        {
            _objectTDG.Add(_setObject);
            _objectTDG.Add(_setObject1);
            _objectTDG.Add(_setObject2);
            _objectTDG.Add(_setObject3);

            List<ExhibitionContentEn> numberOfEntries = _db.Query<ExhibitionContentEn>("SELECT * from ExhibitionContentEn where storyLineId = ?", _setObject.StoryLineId);
            List<int> list = _objectTDG.GetExhibitionContentEnByStorylineId(_setObject.StoryLineId);
            Assert.AreEqual(list.Count, numberOfEntries.Count);
        }

        [Test()]
        public void UpdateExhibitionContentEnTest()
        {
            _testObject.Id = 1;
            _testObject.Title = "title";
            _testObject.Description = "description";
            _testObject.Filepath = "filepath";
            _testObject.Duration = 1;
            _testObject.Resolution = 560;
            _testObject.Encoding = "encoding";
            _testObject.Discriminator = "discriminator";
            _objectTDG.Add(_testObject);
            _testObject.Duration = 0;
            double duration = _testObject.Duration;
            _objectTDG.Update(_testObject);
            _testObject = _objectTDG.GetExhibitionContentEn(_testObject.Id);
            Assert.AreEqual(_testObject.Duration, duration);
        }
    }
}