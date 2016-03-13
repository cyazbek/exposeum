using SQLite; 
namespace Exposeum.Tables
{
    class MapElements
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public double uCoordinate { get; set; }
        public double vCoordinate { get; set; }
        public int icon { get; set; }
        public string discriminator { get; set; }
        public int visited { get; set; }
        public int beaconId { get; set; }
        public int poiDescription { get; set; }
        public string label { get; set; }
        public int exhibitionContent { get; set; }

    }
}