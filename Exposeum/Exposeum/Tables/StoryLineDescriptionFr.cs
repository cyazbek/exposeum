using SQLite;

namespace Exposeum.Tables
{
    public class StoryLineDescriptionFr
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IntendedAudience { get; set; }
    }
}