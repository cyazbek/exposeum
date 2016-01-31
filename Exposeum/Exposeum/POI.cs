using SQLite;

namespace Exposeum
{
    public class POI
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description_en { get; set;}
        public string description_fr{ get; set;} 
    }
}
