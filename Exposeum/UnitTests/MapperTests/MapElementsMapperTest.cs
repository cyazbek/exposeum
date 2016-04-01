using System.Collections.Generic;
using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class MapElementsMapperTest
    {
        private static MapElementsMapper _instance;
        private PointOfInterestMapper _poiMapper;
        private WayPointMapper _wayPointMapper;

        private readonly List<MapElement> _elements = new List<MapElement>();
        private List<MapElement> _expectedElements;

        private readonly MapElementsTdg _mapElementsTdg = MapElementsTdg.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _mapElementsTdg.Db.DeleteAll<MapElements>();

            _instance = MapElementsMapper.GetInstance();
            _poiMapper = PointOfInterestMapper.GetInstance();
            _wayPointMapper = WayPointMapper.GetInstance();
        }

        [Test]
        public void GetInstanceExhibitionContentMapperTest()
        {
            Assert.NotNull(MapElementsMapper.GetInstance());
        }

    }
}