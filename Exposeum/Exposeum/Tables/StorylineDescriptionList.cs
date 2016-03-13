using SQLite;

namespace Exposeum.Tables
{

    public class StorylineDescriptionList
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int storyLineId { get; set; }
        public int descriptionId { get; set; }

    }
}