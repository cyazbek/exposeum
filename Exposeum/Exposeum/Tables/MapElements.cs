using SQLite; 
namespace Exposeum.Tables
{
    public class MapElements
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public float UCoordinate { get; set; }
        public float VCoordinate { get; set; }
        public int IconId { get; set; }
        public string Discriminator { get; set; }
        public int Visited { get; set; }
        public int BeaconId { get; set; }
        public int StoryLineId { get; set; }
        public int PoiDescription { get; set; }
        public string Label { get; set; }
        public int ExhibitionContent { get; set; }
        public int FloorId { get; set; }

    }
}