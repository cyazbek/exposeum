using System.Collections.Generic;
using Android.Graphics.Drawables;
using Exposeum.Mappers;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.TempModels;
using Java.Util;
using NUnit.Framework;
using Beacon = Exposeum.TempModels.Beacon;
using Floor = Exposeum.TempModels.Floor;
using User = Exposeum.Models.User;

namespace UnitTests
{
    [TestFixture]
    public class PointOfInterestMapperTest
    {
        private static PointOfInterestMapper _instance;
        private PointOfInterest _expected;
        private PointOfInterest _pointOfInterestModel;
        private MapElements _pointOfInterestTable;
        readonly MapElementsTdg _tdg = MapElementsTdg.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = PointOfInterestMapper.GetInstance();
            _tdg.Db.DeleteAll<MapElements>();

            Floor floor = new Floor
            {
                Id = 1,
                ImagePath = "icon.png",
                FloorPlan = (BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("icon.png"), null)
            };

            Beacon beacon = new Beacon
            {
                Id = 1,
                Major = 1234,
                Minor = 1234,
                Uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            PointOfInterestDescription poiDescription = new PointOfInterestDescription()
            {
                Id = 1,
                Title = "theTitle",
                Summary = "theSummary",
                Description = "theDescription",
                Language = User.GetInstance().Language
            };

            List<ExhibitionContent> contents = new List<ExhibitionContent>();

            AudioContent audioContent = new AudioContent
            {
                Id = 1,
                Title = "AudioTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };

            VideoContent videoContent = new VideoContent
            {
                Id = 1,
                Title = "VideoTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
           };  

            contents.Add(audioContent);
            contents.Add(videoContent);

            _pointOfInterestModel = new PointOfInterest
            {
                Id = 1,  
                Visited = true,
                IconPath = "imagePath",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = floor,
                Beacon = beacon,
                StoryLineId = 1,
                Description = poiDescription,
                ExhibitionContent = contents
            };

            _pointOfInterestTable = new MapElements
            {
                Id = 1,
                Visited = 1,
                IconPath = "imagePath",
                UCoordinate = 2f,
                VCoordinate = 2f,
                FloorId = 1,
                BeaconId = 1,
                StoryLineId = 1,
                PoiDescription = 1,
            };

        }

        [Test]
        public void GetInstancePointOfInterestMapperTest()
        {
            Assert.NotNull(PointOfInterestMapper.GetInstance());
        }

        [Test]
        public void AddPointOfInterestTest()
        {
            _instance.Add(_pointOfInterestModel);
            _expected = _instance.Get(_pointOfInterestModel.Id);
            Assert.True(_pointOfInterestModel.Equals(_expected));
        }

        [Test]
        public void UpdatePointOfInterestTest()
        {
            _pointOfInterestModel = new PointOfInterest
            {
                Id = 1,
                Visited = true,
                IconPath = "imagePath",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = new Floor(),
                Beacon = new Beacon(),
                StoryLineId = 1,
                Description = new PointOfInterestDescription(),
                ExhibitionContent = new List<ExhibitionContent>()
            };

            _instance.Add(_pointOfInterestModel);

            _pointOfInterestModel.StoryLineId = 3;
            _instance.Update(_pointOfInterestModel);

            _expected = _instance.Get(_pointOfInterestModel.Id);
            Assert.AreEqual(3, _expected.StoryLineId);
        }

        [Test]
        public void PointOfInterestModelToTableTest()
        {
            MapElements expected = _instance.ConvertFromModel(_pointOfInterestModel);

            Assert.IsTrue(_tdg.Equals(_pointOfInterestTable, expected));
        }

        [Test]
        public void PointOfInterestTableToModelTest()
        {
            PointOfInterest expected = _instance.ConvertToModel(_pointOfInterestTable);
            Assert.IsTrue(_pointOfInterestModel.Equals(expected));
        }

    }
}