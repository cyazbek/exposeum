using SQLite; 

namespace Exposeum.Tables
{
    public class Storyline
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string audience { get; set; }
        public int duration { get; set; }
        public int images { get; set; } 
        public int floorsCovered { get; set; }
        public int lastVisitedPoi { get; set; }
        public string status { get; set; } 

    }
}