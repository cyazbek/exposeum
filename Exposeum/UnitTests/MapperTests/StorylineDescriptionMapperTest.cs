using Exposeum.TempModels;
using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.Tables;
using NUnit.Framework;
using Exposeum.TDGs;
using PointOfInterestDescription = Exposeum.TempModels.PointOfInterestDescription;

namespace UnitTests.MapperTests
{
    [TestFixture]
    public class StorylineDescriptionMapperTest
    {
        private static StoryLineDescriptionMapper _instance;
        private StorylineDescription _expected;
        private StorylineDescription _storylineDescriptionModel;
        private StoryLineDescriptionEn _storylineDescriptionEnTable;
        private StoryLineDescriptionFr _storylineDescriptionFrTable;
        readonly StoryLineDescriptionEnTDG _tdgEn = StoryLineDescriptionEnTDG.GetInstance();
        readonly StoryLineDescriptionFrTDG _tdgFr = StoryLineDescriptionFrTDG.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = StoryLineDescriptionMapper.GetInstance();
            _tdgEn._db.DeleteAll<PoiDescriptionEn>();
            _tdgFr._db.DeleteAll<PoiDescriptionFr>();

            _storylineDescriptionModel = new StorylineDescription
            {
                _storyLineDescriptionId = 1,
                _title = "theTitle",
                _description = "theDescription",
                _language = User.GetInstance()._language
            };

            _storylineDescriptionEnTable = new StoryLineDescriptionEn
            {
                ID = 1,
                title = "theTitleEn",
                description = "theDescriptionEn"
            };

            _storylineDescriptionFrTable = new StoryLineDescriptionFr
            {
                ID = 1,
                title = "theTitleFr",
                description = "theDescriptionFr"
            };
        }

        [Test]
        public void GetInstanceStorylineDescriptionMapperTest()
        {
            Assert.NotNull(StoryLineDescriptionMapper.GetInstance());
        }

        [Test]
        public void AddPointOfInterestDescriptionTest()
        {
            _instance.AddStoryLineDescription(_storylineDescriptionModel);
            _expected = _instance.GetStoryLineDescription(_storylineDescriptionModel._storyLineDescriptionId);
            Assert.True(_storylineDescriptionModel.Equals(_expected));
        }

        [Test]
        public void UpdatePointOfInterestDescriptionTest()
        {
            _storylineDescriptionModel = new StorylineDescription
            {
                _storyLineDescriptionId = 2,
                _title = "theTitle",
                _description = "theDescription",
                _language = User.GetInstance()._language
            };

            _instance.AddStoryLineDescription(_storylineDescriptionModel);

            _storylineDescriptionModel._title = "titleUpdated";
            _instance.UpdateStoryLineDescription(_storylineDescriptionModel);

            _expected = _instance.GetStoryLineDescription(_storylineDescriptionModel._storyLineDescriptionId);
            Assert.AreEqual("titleUpdated", _expected._title);
        }

        [Test]
        public void PointOfInterestDescriptionModelToTableEnTest()
        {
            _storylineDescriptionModel = new StorylineDescription
            {
                _storyLineDescriptionId = 1,
                _title = "theTitleEn",
                _description = "theDescriptionEn",
                _language = User.GetInstance()._language
            };

            StoryLineDescriptionEn expectedEn = _instance.StoryLineDescriptionModelToTableEn(_storylineDescriptionModel);
            Assert.IsTrue(_tdgEn.Equals(_storylineDescriptionEnTable, expectedEn));
        }

        [Test]
        public void PointOfInterestDescriptionModelToTableFrTest()
        {
            _storylineDescriptionModel = new StorylineDescription
            {
                _storyLineDescriptionId = 1,
                _title = "theTitleFr",
                _description = "theDescriptionFr",
                _language = User.GetInstance()._language
            };

            StoryLineDescriptionFr expectedFr = _instance.StoryLineDescriptionModelToTableFr(_storylineDescriptionModel);
            Assert.IsTrue(_tdgFr.Equals(_storylineDescriptionFrTable, expectedFr));
        }

        [Test]
        public void PointOfInterestDescriptionTableToModelFrTest()
        {
            _storylineDescriptionModel = new StorylineDescription
            {
                _storyLineDescriptionId = 1,
                _title = "theTitleFr",
                _description = "theDescriptionFr",
                _language = User.GetInstance()._language
            };

            StorylineDescription expectedFr = _instance.StorylineDescriptionTableToModelFr(_storylineDescriptionFrTable);
            Assert.IsTrue(_storylineDescriptionModel.Equals(expectedFr));
        }

        [Test]
        public void PointOfInterestDescriptionTableToModelEnTest()
        {
            _storylineDescriptionModel = new StorylineDescription
            {
                _storyLineDescriptionId = 1,
                _title = "theTitleEn",
                _description = "theDescriptionEn",
                _language = User.GetInstance()._language
            };

            StorylineDescription expectedEn = _instance.StorylineDescriptionTableToModelEn(_storylineDescriptionEnTable);
            Assert.IsTrue(_storylineDescriptionModel.Equals(expectedEn));
        }
    }
}