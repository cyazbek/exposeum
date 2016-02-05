
using SQLite;

namespace Exposeum.Models
{
    public class StoryLine
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }

        public string name_en { get; set; }

        public string name_fr { get; set; }

        public string preview_en { get; set; }

        public string preview_fr { get; set; }

        public string target_en { get; set; }

        public string target_fr { get; set; }

        public string duration { get; set; }
    }
}