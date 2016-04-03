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
        private MapEdge _mapEdgeModel2;
        private MapEdge _mapEdgeModel3;
        private Exposeum.Tables.MapEdge _mapEdgeTable;
        private readonly MapEdgeTdg _tdg = MapEdgeTdg.GetInstance();
        private PointOfInterest _poi1;
        private WayPoint _waypoint;

        [SetUp]
        public void SetUp()
        {
            _instance = MapEdgeMapper.GetInstance();

            MapElementsTdg.GetInstance().Db.DeleteAll<MapElements>();
            _tdg.Db.DeleteAll<Exposeum.Tables.MapEdge>();

            Exposeum.Models.User.GetInstance().Language = Language.En;

            Floor floor = new Floor((BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("icon.png"), null))
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

            _waypoint = new WayPoint(2f, 2f, floor)
            {
                Id = 2,
                Visited = true,
                IconPath = "exit.png",
                Icon = (BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("exit.png"), null),
                Label = WaypointLabel.Exit
            };

            PointOfInterestMapper.GetInstance().Add(_poi1);
            WayPointMapper.GetInstance().Add(_waypoint);

            _mapEdgeModel = new MapEdge(_poi1, _waypoint)
            {
                Id = 1,
                Distance = 2.0,
            };

            _mapEdgeModel2 = new MapEdge(_poi1, _waypoint)
            {
                Id = 2,
                Distance = 2.0,
            };

            _mapEdgeModel3 = new MapEdge(_poi1, _waypoint)
            {
                Id = 3,
                Distance = 2.0,
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
            Assert.True(_mapEdgeModel.Equals(_expected));
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
            _mapEdgeModel = _instance.MapEdgeTableToModel(_mapEdgeTable);
            Exposeum.Tables.MapEdge expectedTable = _instance.MapEdgeModelToTable(_mapEdgeModel);

            Assert.AreEqual(_mapEdgeTable.Id, expectedTable.Id);
            Assert.AreEqual(_mapEdgeTable.Distance, expectedTable.Distance);
            Assert.AreEqual(_mapEdgeTable.StartMapElementId, expectedTable.StartMapElementId);
            Assert.AreEqual(_mapEdgeTable.EndMapElementId, expectedTable.EndMapElementId);
        }
        
        [Test]
        public void MapEdgeTableToModelTest()
        {
            _mapEdgeTable = _instance.MapEdgeModelToTable(_mapEdgeModel);
            MapEdge expectedModel = _instance.MapEdgeTableToModel(_mapEdgeTable);

            Assert.IsTrue(_mapEdgeModel.Equals(expectedModel));
        }

        [Test]
        public void GetAllMapEdgesTest()
        {
            
        }

        [Test]
        public void UpdateMapEdgesListTest()
        {
            
        }
    }
}