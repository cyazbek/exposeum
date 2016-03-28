using Android.Graphics.Drawables;
using Exposeum.Mappers;
using Exposeum.TDGs;
using Java.IO;
using Java.Util;
using NUnit.Framework;
using Console = System.Console;
using Floor = Exposeum.Tables.Floor;

namespace UnitTests
{
    [TestFixture]
    public class FloorMapperTest
    {
        private static FloorMapper _instance;
        private Exposeum.TempModels.Floor _expected;
        private Exposeum.TempModels.Floor _floorModel;
        private Floor _floorTable;
        readonly FloorTdg _tdg = FloorTdg.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = FloorMapper.GetInstance();
            _tdg.Db.DeleteAll<Floor>();

            _floorModel = new Exposeum.TempModels.Floor
            {
                Id = 1,
                ImagePath = "EmileBerliner.png",
                _floorPlan = (BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("EmileBerliner.png"), null),
            };

            _floorTable = new Floor
            {
                Id = 1,
                ImagePath = "EmileBerliner.png"
            };

        }

        [Test]
        public void GetInstanceFloorMapperTest()
        {
            Assert.NotNull(FloorMapper.GetInstance());
        }

        [Test]
        public void AddFloorTest()
        {
            _instance.AddFloor(_floorModel);
            _expected = _instance.GetFloor(_floorModel.Id);
            Assert.True(_floorModel.Equals(_expected));
        }

        [Test]
        public void UpdateFloorTest()
        {
            _floorModel = new Exposeum.TempModels.Floor
            {
                Id = 2,
                ImagePath = "EmileBerliner.png",
                _floorPlan = (BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open("EmileBerliner.png"), null)
            };

            _instance.AddFloor(_floorModel);

            _floorModel.ImagePath = "icon.png";
            _instance.UpdateFloor(_floorModel);

            _expected = _instance.GetFloor(_floorModel.Id);
            Assert.AreEqual("icon.png", _expected.ImagePath);
        }

        [Test]
        public void FloorModelToTableTest()
        {
            Floor expected = _instance.FloorModelToTable(_floorModel);
            Assert.IsTrue(FloorTdg.GetInstance().Equals(_floorTable, expected));
        }

        [Test()]
        public void FloorTableToModelTest()
        {
            Exposeum.TempModels.Floor expected = _instance.FloorTableToModel(_floorTable);
            Assert.IsTrue(_floorModel.Equals(expected));
        }

    }
}