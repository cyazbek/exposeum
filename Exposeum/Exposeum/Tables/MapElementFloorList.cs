using SQLite;

namespace Exposeum.Tables
{
    public class MapElementFloorList
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int mapElementId { get; set; }
        public int floorId { get; set; }
        
    }
}