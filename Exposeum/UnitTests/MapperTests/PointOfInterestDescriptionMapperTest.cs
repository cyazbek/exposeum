using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.Mappers;
using Exposeum.Models;
using NUnit.Framework;
using PointOfInterestDescription = Exposeum.TempModels.PointOfInterestDescription;


namespace UnitTests.MapperTests
{
    public class PointOfInterestDescriptionMapperTest
    {
        private static PointOfInterestDescriptionMapper _instance;
        private PointOfInterestDescription _expected;
        private PointOfInterestDescription _pointOfInterestDescriptionModel;
        private PoiDescriptionEn _pointOfInterestDescriptionEnTable;
        private PoiDescriptionFr _pointOfInterestDescriptionFrTable;
        readonly PoiDescriptionEnTdg _tdgEn = PoiDescriptionEnTdg.GetInstance();
        readonly PoiDescriptionFrTdg _tdgFr = PoiDescriptionFrTdg.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = PointOfInterestDescriptionMapper.GetInstance();
            _tdgEn.Db.DeleteAll<PoiDescriptionEn>();
            _tdgFr.Db.DeleteAll<PoiDescriptionFr>();

            _pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                Id = 1,
                Title = "theTitle",
                Summary = "theSummary",
                Description = "theDescription",
                Language = User.GetInstance().Language
            };

            _pointOfInterestDescriptionEnTable = new PoiDescriptionEn
            {
                Id = 1,
                Title = "theTitleEn",
                Summary = "theSummaryEn",
                Description = "theDescriptionEn"
            };

            _pointOfInterestDescriptionFrTable = new PoiDescriptionFr
            {
                Id = 1,
                Title = "theTitleFr",
                Summary = "theSummaryFr",
                Description = "theDescriptionFr"
            };
        }

        [Test]
        public void GetInstancePointOfInterestDescriptionMapperTest()
        {
            Assert.NotNull(PointOfInterestDescriptionMapper.GetInstance());
        }

        [Test]
        public void AddPointOfInterestDescriptionTest()
        {
            _instance.AddPointOfInterestDescription(_pointOfInterestDescriptionModel);
            _expected = _instance.GetPointOfInterestDescription(_pointOfInterestDescriptionModel.Id);
            Assert.True(_pointOfInterestDescriptionModel.Equals(_expected));
        }

        [Test]
        public void UpdatePointOfInterestDescriptionTest()
        {
            _pointOfInterestDescriptionModel = new Exposeum.TempModels.PointOfInterestDescription
            {
                Id = 2,
                Title = "theTitle",
                Summary = "theSummary",
                Description = "theDescription",
                Language = User.GetInstance().Language
            };

            _instance.AddPointOfInterestDescription(_pointOfInterestDescriptionModel);

            _pointOfInterestDescriptionModel.Title = "titleUpdated";
            _instance.UpdatePointOfInterestDescription(_pointOfInterestDescriptionModel);

            _expected = _instance.GetPointOfInterestDescription(_pointOfInterestDescriptionModel.Id);
            Assert.AreEqual("titleUpdated", _expected.Title);
        }

        [Test]
        public void PointOfInterestDescriptionModelToTableEnTest()
        {
            _pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                _id = 1,
                _title = "theTitleEn",
                _summary = "theSummaryEn",
                _description = "theDescriptionEn",
                _language = User.GetInstance()._language
            };

            PoiDescriptionEn expectedEn = _instance.PoiDescriptionModelToTableEn(_pointOfInterestDescriptionModel);
            Assert.IsTrue(_tdgEn.Equals(_pointOfInterestDescriptionEnTable, expectedEn));
        }

        [Test]
        public void PointOfInterestDescriptionModelToTableFrTest()
        {
            _pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                _id = 1,
                _title = "theTitleFr",
                _summary = "theSummaryFr",
                _description = "theDescriptionFr",
                _language = User.GetInstance()._language
            };

            PoiDescriptionFr expectedFr = _instance.PoiDescriptionModelToTableFr(_pointOfInterestDescriptionModel);
            Assert.IsTrue(_tdgFr.Equals(_pointOfInterestDescriptionFrTable, expectedFr));
        }

        [Test]
        public void PointOfInterestDescriptionTableToModelFrTest()
        {
            _pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                _id = 1,
                _title = "theTitleFr",
                _summary = "theSummaryFr",
                _description = "theDescriptionFr",
                _language = User.GetInstance()._language
            };

            PointOfInterestDescription expectedFr = _instance.PointOfInterestDescriptionTableToModelFr(_pointOfInterestDescriptionFrTable);
            Assert.IsTrue(_pointOfInterestDescriptionModel.Equals(expectedFr));
        }

        [Test]
        public void PointOfInterestDescriptionTableToModelEnTest()
        {
            _pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                _id = 1,
                _title = "theTitleEn",
                _summary = "theSummaryEn",
                _description = "theDescriptionEn",
                _language = User.GetInstance()._language
            };

            PointOfInterestDescription expectedEn = _instance.PointOfInterestDescriptionTableToModelEn(_pointOfInterestDescriptionEnTable);
            Assert.IsTrue(_pointOfInterestDescriptionModel.Equals(expectedEn));
        }
    }
}