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
using MapEdge = Exposeum.Models.MapEdge;


namespace UnitTests
{
    
    [TestFixture]
    public class MapEdgeMapperTest
    {
        private static MapEdgeMapper _instance;
        private MapEdge _expected;
        private MapEdge _mapEdgeModel;
        private Exposeum.Tables.MapEdge _mapEdgeTable;
        private readonly MapEdgeTdg _tdg = MapEdgeTdg.GetInstance();

        [SetUp]
        public void SetUp()
        { 
            _instance = MapEdgeMapper.GetInstance();

            _tdg.Db.DeleteAll<Exposeum.Tables.MapEdge>();

            Exposeum.Models.User.GetInstance().Language = Language.En;

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
                Language = Exposeum.Models.User.GetInstance().Language
            };

            List<ExhibitionContent> contents = new List<ExhibitionContent>();

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

            WayPoint waypoint = new WayPoint
            {
                Id = 2,
                Visited = true,
                IconPath = "exit.png",
                Icon = (BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("exit.png"), null),
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = floor,
                Label = WaypointLabel.Exit
            };

            PointOfInterestMapper.GetInstance().Add(poi1);
            WayPointMapper.GetInstance().Add(waypoint);

            _mapEdgeModel = new MapEdge
            {
                Id = 1,
                Distance = 2.0,
                Source = poi1,
                Target = waypoint
            };

            _mapEdgeTable = new Exposeum.Tables.MapEdge
            {
                Id = 1,
                Distance = 2.0,
                StartMapElementId = 1,
                EndMapElementId = 2
            };
        }

        [Test]
        public void AddMapEdgeTest()
        {
            _instance.AddMapEdge(_mapEdgeModel);
            _expected = _instance.GetMapEdge(_mapEdgeModel.Id);
            Assert.True(_mapEdgeModel.Equals( _expected));
        }

        [Test]
        public void UpdateMapEdgeTest()
        {
            _instance.AddMapEdge(_mapEdgeModel);

            _mapEdgeModel.Distance = 5.0;
            _instance.UpdateMapEdge(_mapEdgeModel);

            _expected = _instance.GetMapEdge(_mapEdgeModel.Id);
            Assert.AreEqual(5.0, _mapEdgeModel.Distance);
        }

        [Test]
        public void MapEdgeModelToTableTest()
        {
            
        }
        
        [Test]
        public void MapEdgeTableToModelTest()
        {
            
        }
        
    }
    */
}