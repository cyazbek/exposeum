using SQLite; 

namespace Exposeum.Tables
{
    public class Storyline
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public string Audience { get; set; }
        public int Duration { get; set; }
        public string ImagePath { get; set; } 
        public int FloorsCovered { get; set; }
        public int LastVisitedPoi { get; set; }
        public int Status { get; set; } 
        public int DescriptionId { get; set; }

    }
}