using System.Collections.Generic;
using Android.Graphics.Drawables;
using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using Java.Util;
using NUnit.Framework;
using AudioContent = Exposeum.Models.AudioContent;
using Beacon = Exposeum.Models.Beacon;
using ExhibitionContent = Exposeum.Models.ExhibitionContent;
using Floor = Exposeum.Models.Floor;
using MapElement = Exposeum.Models.MapElement;
using PointOfInterest = Exposeum.Models.PointOfInterest;
using PointOfInterestDescription = Exposeum.Models.PointOfInterestDescription;
using User = Exposeum.Models.User;
using VideoContent = Exposeum.Models.VideoContent;
using WaypointLabel = Exposeum.Models.WaypointLabel;
using WayPoint = Exposeum.Models.WayPoint;

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

        private PointOfInterest _poi1;
        private PointOfInterest _poi2;
        private WayPoint _waypoint;
        private WayPoint _wayPoint2;

        [SetUp]
        public void SetUp()
        {
            _mapElementsTdg.Db.DeleteAll<MapElements>();

            _instance = MapElementsMapper.GetInstance();
            _poiMapper = PointOfInterestMapper.GetInstance();
            _wayPointMapper = WayPointMapper.GetInstance();

            User.GetInstance().Language = Language.En;

            Floor floor = new Floor ((BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("icon.png"), null))
            {
                Id = 1,
                ImagePath = "icon.png"
            };

            Beacon beacon = new Beacon
            {
                Id = 1,
                Major = 1234,
                Minor = 1234,
                Uuid = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d")
            };

            PointOfInterestDescription pointOfInterestDescription = new PointOfInterestDescription("theTitle", "theSummary", "theDescription")
            {
                Id = 1,
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

            _poi1 = new PointOfInterest
            {
                Id = 1,
                Visited = true,
                IconPath = "imagePath",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = floor,
                Beacon = beacon,
                StoryId = 1,
                Description = pointOfInterestDescription,
                ExhibitionContent = contents
            };

            _poi2 = new PointOfInterest
            {
                Id = 2,
                Visited = true,
                IconPath = "imagePath",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = floor,
                Beacon = beacon,
                StoryId = 1,
                Description = pointOfInterestDescription,
                ExhibitionContent = contents2
            };

            _waypoint = new WayPoint(2f,2f,floor)
            {
                Id = 3,
                Visited = true,
                IconPath = "exit.png",
                Label = WaypointLabel.Exit
            };

            _wayPoint2 = new WayPoint(2f, 2f, floor)
            {
                Id = 4,
                Visited = true,
                IconPath = "exit.png",
                Label = WaypointLabel.Exit
            };

            _elements = new List<MapElement> {_poi1, _poi2, _waypoint};
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

        [Test]
        public void UpdateListTest()
        {
            _mapElementsTdg.Db.DeleteAll<MapElements>();

            _instance.AddList(_elements);
            _expectedElements = _instance.GetAllMapElements();

            Assert.AreEqual(_elements, _expectedElements);
            Assert.IsTrue(_elements.Count == _expectedElements.Count);

            _poi1.StoryId = 2;
            _waypoint.Label = WaypointLabel.Elevator;

            _instance.UpdateList(_elements);
            _expectedElements = _instance.GetAllMapElements();

            Assert.AreEqual(_elements, _expectedElements);
            Assert.AreEqual(2, ((PointOfInterest)_expectedElements[0]).StoryId);
            Assert.AreEqual(WaypointLabel.Elevator, ((WayPoint)_expectedElements[2]).Label);
        }

        [Test]
        public void GetMapElementTest()
        {
            _poiMapper.Add(_poi1);
            _wayPointMapper.Add(_waypoint);

            MapElement expected = _instance.Get(_poi1.Id);
            Assert.AreEqual(_poi1, expected);

            expected = _instance.Get(_waypoint.Id);
            Assert.AreEqual(_waypoint, expected);
        }

        [Test]
        public void GetAllElementFromListOfIdsTest()
        {
            _elements.Add(_wayPoint2);
            _instance.AddList(_elements);

            List<int> mapElementIds = new List<int> {1, 2, 3, 4};
            _expectedElements = _instance.GetAllElementsFromListOfMapElementIds(mapElementIds);

            Assert.AreEqual(_elements, _expectedElements);
        }

    }
}