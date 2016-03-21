

using SQLite;

namespace Exposeum.Tables
{
    public class StoryLineDescriptionEn
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}