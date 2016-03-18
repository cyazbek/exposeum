using SQLite; 
namespace Exposeum.Tables
{
    public class MapElements
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public float uCoordinate { get; set; }
        public float vCoordinate { get; set; }
        public int iconId { get; set; }
        public string discriminator { get; set; }
        public int visited { get; set; }
        public int beaconId { get; set; }
        public int poiDescription { get; set; }
        public string label { get; set; }
        public int exhibitionContent { get; set; }
        public int floorId { get; set; }

    }
}