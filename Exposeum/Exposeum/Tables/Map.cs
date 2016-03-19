using SQLite; 

namespace Exposeum.Tables
{
    public class Map
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int currentStoryLineID { get; set; }
        public int currentFloorID { get; set; }

    }
}