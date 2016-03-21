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
        readonly PoiDescriptionEnTDG _tdgEn = PoiDescriptionEnTDG.GetInstance();
        readonly PoiDescriptionFrTDG _tdgFr = PoiDescriptionFrTDG.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = PointOfInterestDescriptionMapper.GetInstance();
            _tdgEn._db.DeleteAll<PoiDescriptionEn>();
            _tdgFr._db.DeleteAll<PoiDescriptionFr>();

            _pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                _id = 1,
                _title = "theTitle",
                _summary = "theSummary",
                _description = "theDescription",
                _language = User.GetInstance()._language
            };

            _pointOfInterestDescriptionEnTable = new PoiDescriptionEn
            {
                ID = 1,
                title = "theTitleEn",
                summary = "theSummaryEn",
                description = "theDescriptionEn"
            };

            _pointOfInterestDescriptionFrTable = new PoiDescriptionFr
            {
                ID = 1,
                title = "theTitleFr",
                summary = "theSummaryFr",
                description = "theDescriptionFr"
            };
        }

        [Test]
        public void AddPointOfInterestDescriptionTest()
        {
            _instance.AddPointOfInterestDescription(_pointOfInterestDescriptionModel);
            _expected = _instance.GetPointOfInterestDescription(_pointOfInterestDescriptionModel._id);
            Assert.True(_pointOfInterestDescriptionModel.Equals(_expected));
        }

        [Test]
        public void UpdatePointOfInterestDescriptionTest()
        {
            _pointOfInterestDescriptionModel = new Exposeum.TempModels.PointOfInterestDescription
            {
                _id = 2,
                _title = "theTitle",
                _summary = "theSummary",
                _description = "theDescription",
                _language = User.GetInstance()._language
            };

            _instance.AddPointOfInterestDescription(_pointOfInterestDescriptionModel);

            _pointOfInterestDescriptionModel._title = "titleUpdated";
            _instance.UpdatePointOfInterestDescription(_pointOfInterestDescriptionModel);

            _expected = _instance.GetPointOfInterestDescription(_pointOfInterestDescriptionModel._id);
            Assert.AreEqual("titleUpdated", _expected._title);
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