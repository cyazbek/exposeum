using System.Collections.Generic;
using Android.Graphics.Drawables;
using Exposeum.Mappers;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.Models;
using Java.Util;
using NUnit.Framework;
using Beacon = Exposeum.Models.Beacon;
using Floor = Exposeum.Models.Floor;
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
        private Floor _floor;
        private Beacon _beacon;
        private List<ExhibitionContent> _contents;
        private PointOfInterestDescription _pointOfInterestDescription;
        readonly MapElementsTdg _tdg = MapElementsTdg.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = PointOfInterestMapper.GetInstance();
            _tdg.Db.DeleteAll<MapElements>();

            _floor = new Floor((BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("icon.png"), null))
            {
                Id = 1,
                ImagePath = "icon.png"
            };

            _beacon = new Beacon
            {
                Id = 1,
                Major = 1234,
                Minor = 1234,
                Uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            _pointOfInterestDescription = new PointOfInterestDescription("theTitle", "theSummary", "theDescription")
            {
                Id = 1,
                Title = "theTitle",
                Summary = "theSummary",
                Description = "theDescription",
                Language = User.GetInstance().Language
            };

            _contents = new List<ExhibitionContent>();

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

            _contents.Add(audioContent);
            _contents.Add(videoContent);

            _pointOfInterestModel = new PointOfInterest
            {
                Id = 1,  
                Visited = true,
                IconPath = "imagePath",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = _floor,
                Beacon = _beacon,
                StoryId = 1,
                Description = _pointOfInterestDescription,
                ExhibitionContent = _contents
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
                Discriminator = "PointOfInterest"
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
                Floor = _floor,
                Beacon = _beacon,
                StoryId = 1,
                Description = _pointOfInterestDescription,
                ExhibitionContent = _contents
            };

            _instance.Add(_pointOfInterestModel);

            _pointOfInterestModel.IconPath = "imagePathUpdated";
            _instance.Update(_pointOfInterestModel);

            _expected = _instance.Get(_pointOfInterestModel.Id);
            Assert.AreEqual("imagePathUpdated", _expected.IconPath);
        }

        [Test]
        public void PointOfInterestModelToTableTest()
        {
            MapElements expected = _instance.PoiModelToTable(_pointOfInterestModel);

            Assert.IsTrue(_tdg.Equals(_pointOfInterestTable, expected));
        }

        [Test]
        public void PointOfInterestTableToModelTest()
        {
            PointOfInterest expected = _instance.PoiTableToModel(_pointOfInterestTable);
            Assert.IsTrue(_pointOfInterestModel.Equals(expected));
        }

    }
}
 