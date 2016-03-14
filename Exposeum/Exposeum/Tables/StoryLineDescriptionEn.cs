

using SQLite;

namespace Exposeum.Tables
{
    public class StoryLineDescriptionEn
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string title { get; set; }
        public string description { get; set; }

    }
}