using System.Collections.Generic;
using Android.Graphics.Drawables;
using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using Java.Util;
using NUnit.Framework;
using Beacon = Exposeum.Models.Beacon;
using Floor = Exposeum.Models.Floor;
using User = Exposeum.Models.User;

namespace UnitTests
{
    public class StorylineMapperTests
    {
        private readonly StorylineMapper _storylineMapper = StorylineMapper.GetInstance();
        private StorylineTdg _storyTdg = StorylineTdg.GetInstance();

        private StoryLine _storyLine;
        private StoryLine _storyLine2;
        private StoryLine _storyLine3;
        private Storyline _storylineTable;
        int _storylineMapElementId;

        [SetUp]
        public void SetUp()
        {
            User.GetInstance().Language = Language.En;

            _storyTdg.Db.DeleteAll<Storyline>();
            StoryLineMapElementListTdg.GetInstance().Db.DeleteAll<StoryLineMapElementList>();

            StorylineDescription storyDescription = new StorylineDescription
            {
                StoryLineDescriptionId = 1,
                Title = "theTitle",
                Description = "theDescription",
                Language = User.GetInstance().Language
            };

            List<MapElement> mapElements = SetMapElements();

            _storyLine = new StoryLine
            {
                StorylineId = 1,
                ImgPath = "icon.png",
                Duration = 20,
                FloorsCovered = 3,
                Status = Status.IsNew,
                StorylineDescription = storyDescription,
                MapElements = mapElements,
                LastPointOfInterestVisited = (PointOfInterest) mapElements[0],
            };

            _storyLine2 = new StoryLine
            {
                StorylineId = 2,
                ImgPath = "icon.png",
                Duration = 20,
                FloorsCovered = 3,
                Status = Status.IsNew,
                StorylineDescription = storyDescription,
                MapElements = mapElements,
                LastPointOfInterestVisited = (PointOfInterest)mapElements[0],
            };

            _storyLine3 = new StoryLine
            {
                StorylineId = 3,
                ImgPath = "icon.png",
                Duration = 20,
                FloorsCovered = 3,
                Status = Status.IsNew,
                StorylineDescription = storyDescription,
                MapElements = mapElements,
                LastPointOfInterestVisited = (PointOfInterest)mapElements[0],
            };

            _storylineTable = new Storyline
            {
                Id = 1,
                ImagePath = "icon.png",
                Duration = 20,
                FloorsCovered = 3,
                Status = 2,
                DescriptionId = 1,
                LastVisitedPoi = 1
            };
        }

        [Test]
        public void GetInstanceStorylineMapperTest()
        {
            Assert.NotNull(StorylineMapper.GetInstance());
        }

        [Test]
        public void AddStorylineTest()
        {
            _storylineMapper.AddStoryline(_storyLine);
            StoryLine expected = _storylineMapper.GetStoryline(_storyLine.StorylineId);
            Assert.IsTrue(_storyLine.AreEquals(expected));
        }

        [Test]
        public void UpdateStorylineTest()
        {
            _storylineMapper.AddStoryline(_storyLine);
            StoryLine expected = _storylineMapper.GetStoryline(_storyLine.StorylineId);
            Assert.IsTrue(_storyLine.AreEquals(expected));

            _storyLine.Duration = 10;
            _storylineMapper.UpdateStoryline(_storyLine);

            expected = _storylineMapper.GetStoryline(_storyLine.StorylineId);
            Assert.IsTrue(_storyLine.AreEquals(expected));
            Assert.AreEqual(10, expected.Duration);
        }

        [Test]
        public void StorylineModelToTableTest()
        {
            _storyLine = _storylineMapper.StorylineTableToModel(_storylineTable);
            Storyline expectedTable = _storylineMapper.StorylineModelToTable(_storyLine);

            Assert.AreEqual(_storylineTable.Id, expectedTable.Id);
            Assert.AreEqual(_storylineTable.DescriptionId, expectedTable.DescriptionId);
            Assert.AreEqual(_storylineTable.Duration, expectedTable.Duration);
            Assert.AreEqual(_storylineTable.FloorsCovered, expectedTable.FloorsCovered);
            Assert.AreEqual(_storylineTable.ImagePath, expectedTable.ImagePath);
            Assert.AreEqual(_storylineTable.LastVisitedPoi, expectedTable.LastVisitedPoi);
            Assert.AreEqual(_storylineTable.Status, expectedTable.Status);
        }

        [Test]
        public void StorylineTableToModelTest()
        {
            AddStorylineTest();

            _storylineTable = _storylineMapper.StorylineModelToTable(_storyLine);
            StoryLine expectedModel = _storylineMapper.StorylineTableToModel(_storylineTable);

            Assert.IsTrue(_storyLine.AreEquals(expectedModel));
        }

        [Test]
        public void GetAllStorylineMapElementIdsTest()
        {
            AddStorylineTest();

            List<int> mapElementsIds = new List<int> {1,2,3,4};
            List<int> expected = _storylineMapper.GetAllStorylineMapElementIds(1);

            Assert.AreEqual(mapElementsIds, expected);
        }

        [Test]
        public void GetAllStorylinesTest()
        {
            _storylineMapper.AddStoryline(_storyLine);
            _storylineMapper.AddStoryline(_storyLine2);
            _storylineMapper.AddStoryline(_storyLine3);

            List<StoryLine> storyLinesList = new List<StoryLine> {_storyLine, _storyLine2, _storyLine3};
            List<StoryLine> expected = _storylineMapper.GetAllStorylines();

            Assert.IsTrue(_storylineMapper.ListEquals(storyLinesList, expected));
        }

        [Test]
        public void UpdateStorylinesList()
        {
            _storylineMapper.AddStoryline(_storyLine);
            _storylineMapper.AddStoryline(_storyLine2);
            _storylineMapper.AddStoryline(_storyLine3);

            List<StoryLine> storyLinesList = new List<StoryLine> { _storyLine, _storyLine2, _storyLine3 };
            List<StoryLine> expected = _storylineMapper.GetAllStorylines();

            Assert.IsTrue(_storylineMapper.ListEquals(storyLinesList, expected));

            _storyLine.FloorsCovered = 10;
            _storyLine2.Status = Status.IsVisited;
            _storyLine3.Status = Status.InProgress;

            _storylineMapper.UpdateStorylinesList(storyLinesList);
            expected = _storylineMapper.GetAllStorylines();

            Assert.IsTrue(_storylineMapper.ListEquals(storyLinesList, expected));
            Assert.AreEqual(10, expected[0].FloorsCovered);
            Assert.AreEqual(Status.IsVisited, expected[1].Status);
            Assert.AreEqual(Status.InProgress, expected[2].Status);
        }

        public List<MapElement> SetMapElements()
        {
            List<MapElement> mapElements = new List<MapElement>();

            User.GetInstance().Language = Exposeum.Models.Language.En;

            Floor floor =
                new Floor(
                    (BitmapDrawable)
                        Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("icon.png"), null))
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

            PointOfInterestDescription pointOfInterestDescription = new PointOfInterestDescription("theTitle",
                "theSummary", "theDescription")
            {
                Id = 1,
                Language = User.GetInstance().Language
            };

            List<ExhibitionContent> contents = new List<ExhibitionContent>();

            AudioContent audioContent = new AudioContent
            {
                Id = 1,
                Title = "AudioTitle",
                Language = Exposeum.Models.Language.En,
                PoiId = 1,
                StorylineId = 1,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };

            VideoContent videoContent = new VideoContent
            {
                Id = 2,
                Title = "VideoTitle",
                Language = Exposeum.Models.Language.En,
                PoiId = 1,
                StorylineId = 1,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
            };

            contents.Add(audioContent);
            contents.Add(videoContent);

            PointOfInterest poi1 = new PointOfInterest
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

            PointOfInterest poi2 = new PointOfInterest
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
                ExhibitionContent = contents
            };

            PointOfInterest poi3 = new PointOfInterest
            {
                Id = 3,
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

            WayPoint wayPoint = new WayPoint(2f, 2f, floor)
            {
                Id = 4,
                Visited = true,
                IconPath = "exit.png",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = floor,
                Label = WaypointLabel.Exit
            };

            mapElements.Add(poi1);
            mapElements.Add(poi2);
            mapElements.Add(poi3);
            mapElements.Add(wayPoint);

            return mapElements;
        }
    }
}