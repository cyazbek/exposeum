
using SQLite;

namespace Exposeum
{
    public class POI
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string name_en { get; set; }

        public string name_fr { get; set; }

        public string dscription_en { get; set; }

        public string dscription_fr { get; set; }
    }
}