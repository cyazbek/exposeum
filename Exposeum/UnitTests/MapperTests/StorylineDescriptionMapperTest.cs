using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.TempModels;
using NUnit.Framework;
using User = Exposeum.Models.User;

namespace UnitTests
{
    [TestFixture]
    public class StorylineDescriptionTest
    {
        private static StoryLineDescriptionMapper _instance;
        private StorylineDescription _expected;
        private StorylineDescription _storylineDescriptionModel;
        private StoryLineDescriptionEn _storylineDescriptionEnTable;
        private StoryLineDescriptionFr _storylineDescriptionFrTable;
        readonly StoryLineDescriptionEnTdg _tdgEn = StoryLineDescriptionEnTdg.GetInstance();
        readonly StoryLineDescriptionFrTdg _tdgFr = StoryLineDescriptionFrTdg.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = StoryLineDescriptionMapper.GetInstance();
            _tdgEn.Db.DeleteAll<PoiDescriptionEn>();
            _tdgFr.Db.DeleteAll<PoiDescriptionFr>();

            _storylineDescriptionModel = new StorylineDescription
            {
                StoryLineDescriptionId = 1,
                Title = "theTitle",
                Description = "theDescription",
                Language = User.GetInstance().Language
            };

            _storylineDescriptionEnTable = new StoryLineDescriptionEn
            {
                Id = 1,
                Title = "theTitleEn",
                Description = "theDescriptionEn"
            };

            _storylineDescriptionFrTable = new StoryLineDescriptionFr
            {
                Id = 1,
                Title = "theTitleFr",
                Description = "theDescriptionFr"
            };
        }

        [Test]
        public void AddPointOfInterestDescriptionTest()
        {
            _instance.AddStoryLineDescription(_storylineDescriptionModel);
            _expected = _instance.GetStoryLineDescription(_storylineDescriptionModel.StoryLineDescriptionId);
            Assert.True(_storylineDescriptionModel.Equals(_expected));
        }

        [Test]
        public void UpdatePointOfInterestDescriptionTest()
        {
            _storylineDescriptionModel = new StorylineDescription
            {
                StoryLineDescriptionId = 2,
                Title = "theTitle",
                Description = "theDescription",
                Language = User.GetInstance().Language
            };

            _instance.AddStoryLineDescription(_storylineDescriptionModel);

            _storylineDescriptionModel.Title = "titleUpdated";
            _instance.UpdateStoryLineDescription(_storylineDescriptionModel);

            _expected = _instance.GetStoryLineDescription(_storylineDescriptionModel.StoryLineDescriptionId);
            Assert.AreEqual("titleUpdated", _expected.Title);
        }

        [Test]
        public void PointOfInterestDescriptionModelToTableEnTest()
        {
            _storylineDescriptionModel = new StorylineDescription
            {
                StoryLineDescriptionId = 1,
                Title = "theTitleEn",
                Description = "theDescriptionEn",
                Language = User.GetInstance().Language
            };

            StoryLineDescriptionEn expectedEn = _instance.StoryLineDescriptionModelToTableEn(_storylineDescriptionModel);
            Assert.IsTrue(_tdgEn.Equals(_storylineDescriptionEnTable, expectedEn));
        }

        [Test]
        public void PointOfInterestDescriptionModelToTableFrTest()
        {
            _storylineDescriptionModel = new StorylineDescription
            {
                StoryLineDescriptionId = 1,
                Title = "theTitleFr",
                Description = "theDescriptionFr",
                Language = User.GetInstance().Language
            };

            StoryLineDescriptionFr expectedFr = _instance.StoryLineDescriptionModelToTableFr(_storylineDescriptionModel);
            Assert.IsTrue(_tdgFr.Equals(_storylineDescriptionFrTable, expectedFr));
        }

        [Test]
        public void PointOfInterestDescriptionTableToModelFrTest()
        {
            Exposeum.Models.User.GetInstance().Language = Exposeum.Models.Language.Fr;
            _storylineDescriptionModel = new StorylineDescription
            {
                StoryLineDescriptionId = 1,
                Title = "theTitleFr",
                Description = "theDescriptionFr",
                Language = User.GetInstance().Language
            };

            StorylineDescription expectedFr = _instance.StorylineDescriptionTableToModelFr(_storylineDescriptionFrTable);
            Assert.IsTrue(_storylineDescriptionModel.Equals(expectedFr));
        }

        [Test]
        public void PointOfInterestDescriptionTableToModelEnTest()
        {
            Exposeum.Models.User.GetInstance().Language = Exposeum.Models.Language.En;
            _storylineDescriptionModel = new StorylineDescription
            {
                StoryLineDescriptionId = 1,
                Title = "theTitleEn",
                Description = "theDescriptionEn",
                Language = User.GetInstance().Language
            };

            StorylineDescription expectedEn = _instance.StorylineDescriptionTableToModelEn(_storylineDescriptionEnTable);
            Assert.IsTrue(_storylineDescriptionModel.Equals(expectedEn));
        }
    }
}