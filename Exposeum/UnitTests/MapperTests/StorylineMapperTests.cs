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