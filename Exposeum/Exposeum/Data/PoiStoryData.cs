using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Exposeum.Data
{
    public class PoiStoryData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(StoryData))]
        public int StoryId { get; set; }
        [ForeignKey(typeof(PoiData))]
        public int PoiId { get; set; }

    }
}