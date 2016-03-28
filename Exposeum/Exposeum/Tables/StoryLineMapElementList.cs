using SQLite; 

namespace Exposeum.Tables
{
    public class StoryLineMapElementList
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public int StoryLineId { get; set; }
        public int MapElementId { get; set; }

    }
}