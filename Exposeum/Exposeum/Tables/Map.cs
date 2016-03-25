using SQLite; 

namespace Exposeum.Tables
{
    public class Map
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public int CurrentStoryLineId { get; set; }
        public int CurrentFloorId { get; set; }

    }
}