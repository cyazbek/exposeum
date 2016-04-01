
using Android.Graphics.Drawables;
using Exposeum.Mappers;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.TempModels;
using NUnit.Framework;
using Floor = Exposeum.TempModels.Floor;

namespace UnitTests
{
    [TestFixture]
    public class WayPointMapperTest
    {
        private static WayPointMapper _instance;
        private WayPoint _expected;
        private WayPoint _wayPointModel;
        private MapElements _wayPointTable;
        private Floor _floor;
        readonly MapElementsTdg _tdg = MapElementsTdg.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = WayPointMapper.GetInstance();
            _tdg.Db.DeleteAll<MapElements>();

            _floor = new Floor
            {
                Id = 1,
                ImagePath = "icon.png",
                FloorPlan = (BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("icon.png"), null)
            };

            _wayPointModel = new WayPoint
            {
                Id = 1,
                Visited = true,
                IconPath = "exit.png",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = _floor,
                Label = WaypointLabel.Exit
            };

            _wayPointTable = new MapElements
            {
                Id = 1,
                Visited = 1,
                IconPath = "exit.png",
                UCoordinate = 2f,
                VCoordinate = 2f,
                FloorId = 1,
                Label = "Exit"
            };

        }

        [Test]
        public void GetInstanceWayPointMapperTest()
        {
            Assert.NotNull(WayPointMapper.GetInstance());
        }

        [Test]
        public void AddWayPointTest()
        {
            _instance.Add(_wayPointModel);
            _expected = _instance.Get(_wayPointModel.Id);
            Assert.True(_wayPointModel.Equals(_expected));
        }

        [Test]
        public void UpdateWayPointTest()
        {
            _wayPointModel = new WayPoint
            {
                Id = 2,
                Visited = true,
                IconPath = "exit.png",
                UCoordinate = 2f,
                VCoordinate = 2f,
                Floor = _floor,
                Label = WaypointLabel.Exit
            };

            _instance.Add(_wayPointModel);

            _wayPointModel.Label = WaypointLabel.Stairs;
            _instance.Update(_wayPointModel);

            _expected = _instance.Get(_wayPointModel.Id);
            Assert.AreEqual(WaypointLabel.Stairs, _expected.Label);
        }

        [Test]
        public void WayPointModelToTableTest()
        {
            MapElements expected = _instance.WaypointModelToTable(_wayPointModel);
            Assert.IsTrue(_tdg.Equals(_wayPointTable, expected));
        }

        [Test]
        public void WayPointTableToModelTest()
        {
            WayPoint expected = _instance.WaypointTableToModel(_wayPointTable);
            Assert.IsTrue(_wayPointModel.Equals(expected));
        }

    }
}