using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.TempModels;
using NUnit.Framework;
using MapEdge = Exposeum.TempModels.MapEdge;

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

            _mapEdgeModel = new MapEdge
            {
                Id = 1,
                Distance = 2.0,
      
                /*
                public int Id { get; set; }
                public double Distance { get; set; }
                public MapElement Start { get; set; }
                public MapElement End { get; set; }
                */
            };

            _mapEdgeTable = new Exposeum.Tables.MapEdge
            {
                Id = 1
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
            _mapEdgeModel = new MapEdge
            {
                
            };

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
}