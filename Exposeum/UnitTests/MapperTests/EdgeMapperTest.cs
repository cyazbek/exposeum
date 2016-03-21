using Exposeum.TempModels;
using Java.Util;
using NUnit.Framework;
using Exposeum.Mappers;
using Exposeum.TDGs;

namespace UnitTests.MapperTests
{
    [TestFixture]
    public class EdgeMapperTest
    {
        public Edge _edge;
        public Edge _edge1;
        public MapElement _element;
        public MapElement _element1;
        public Floor _floor;
        public EdgeMapper _edgeMapper;
        public EdgeTDG _edgeTdg; 
        [SetUp]
        public void SetUp()
        {
            _edgeTdg.DeleteAll();
            _edgeMapper = EdgeMapper.GetInstance();
            _element1 = new WayPoint
            {
                _id = 1, 
                _visited = true,
                _iconId = 1,
                _uCoordinate = 12,
                _vCoordinate = 14,
                _floor = _floor
            };
            _element = new WayPoint
            {
                _id = 2,
                _visited = true,
                _iconId = 1,
                _uCoordinate = 12,
                _vCoordinate = 14,
                _floor = _floor
            };
            _edge1 = new Edge
            {
                _distance = 12.5,
                _id = 1, 
                _end = _element,
                _start = _element1
            };
            _edge = new Edge
            {
                _distance = 15,
                _id = 2,
                _end = _element1,
                _start = _element
            };
            _floor = new Floor
            {
                _id = 1,
                _plan = "plan"
            };
        }
        [Test()]
        public void AddEdgeTest()
        {
            _edgeMapper.AddEdge(_edge);
            _edge1 = _edgeMapper.GetEdge(_edge._id);
            Assert.IsTrue(_edge1.Equals(_edge));
        }

    }
}