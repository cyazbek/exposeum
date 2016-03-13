using SQLite; 

namespace Exposeum.Tables
{
    public class StoryLineMapElementList
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int storyLineId { get; set; }
        public int mapElementId { get; set; }

    }
}