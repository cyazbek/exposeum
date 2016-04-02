using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Android.Graphics.Drawables;
using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using Java.Util;
using NUnit.Framework;
using AudioContent = Exposeum.TempModels.AudioContent;
using Beacon = Exposeum.TempModels.Beacon;
using ExhibitionContent = Exposeum.TempModels.ExhibitionContent;
using Floor = Exposeum.TempModels.Floor;
using MapElement = Exposeum.TempModels.MapElement;
using PointOfInterest = Exposeum.TempModels.PointOfInterest;
using PointOfInterestDescription = Exposeum.TempModels.PointOfInterestDescription;
using User = Exposeum.Models.User;
using VideoContent = Exposeum.TempModels.VideoContent;
using WaypointLabel = Exposeum.TempModels.WaypointLabel;
using WayPoint = Exposeum.TempModels.WayPoint;

namespace UnitTests
{
    [TestFixture]
    public class MapElementsMapperTest
    {
        private static MapElementsMapper _instance;
        private PointOfInterestMapper _poiMapper;
        private WayPointMapper _wayPointMapper;

        private List<MapElement> _elements;
        private List<MapElement> _expectedElements;

        private readonly MapElementsTdg _mapElementsTdg = MapElementsTdg.GetInstance();
        

        [SetUp]
        public void SetUp()
        {
            _mapElementsTdg.Db.DeleteAll<MapElements>();

            _instance = MapElementsMapper.GetInstance();
            _poiMapper = PointOfInterestMapper.GetInstance();
            _wayPointMapper = WayPointMapper.GetInstance();

            User.GetInstance().Language = Language.En;

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

            PointOfInterestDescription pointOfInterestDescription = new PointOfInterestDescription
            {
                Id = 1,
                Title = "theTitle",
                Summary = "theSummary",
                Description = "theDescription",
                Language = User.GetInstance().Language
            };

            List<ExhibitionContent> contents = new List<ExhibitionContent>();
            List<ExhibitionContent> contents2 = new List<ExhibitionContent>();

            AudioContent audioContent = new AudioContent
            {
                Id = 1,
                Title = "AudioTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                PoiId = 1,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };

            VideoContent videoContent = new VideoContent
            {
                Id = 2,
                Title = "VideoTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                PoiId = 1,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
            };

            contents.Add(audioContent);
            contents.Add(videoContent);

            AudioContent audioContent2 = new AudioContent
            {
                Id = 3,
                Title = "AudioTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                PoiId = 2,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };

            VideoContent videoContent2 = new VideoContent
            {
                Id = 4,
                Title = "VideoTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                PoiId = 2,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
            };

            contents2.Add(audioContent2);
            contents2.Add(videoContent2);

            PointOfInterest poi1 = new PointOfInterest
            {
                Id = 1,
                Visited = true,
                IconPath = "imagePath",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = floor,
                Beacon = beacon,
                StoryLineId = 1,
                Description = pointOfInterestDescription,
                ExhibitionContent = contents
            };

            PointOfInterest poi2 = new PointOfInterest
            {
                Id = 2,
                Visited = true,
                IconPath = "imagePath",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = floor,
                Beacon = beacon,
                StoryLineId = 1,
                Description = pointOfInterestDescription,
                ExhibitionContent = contents2
            };

            WayPoint waypoint = new WayPoint
            {
                Id = 3,
                Visited = true,
                IconPath = "exit.png",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = floor,
                Label = WaypointLabel.Exit
            };

            _elements = new List<MapElement> {poi1, poi2, waypoint};
        }

        [Test]
        public void GetInstanceExhibitionContentMapperTest()
        {
            Assert.NotNull(MapElementsMapper.GetInstance());
        }

        [Test]
        public void AddListAndGetAllMapElementsTest()
        {
            _mapElementsTdg.Db.DeleteAll<MapElements>();

            _instance.AddList(_elements);
            _expectedElements = _instance.GetAllMapElements();

            Assert.AreEqual(_elements, _expectedElements);
            Assert.IsTrue(_elements.Count == _expectedElements.Count);

        }

    }
}