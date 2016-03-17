using SQLite;

namespace Exposeum.Tables
{

    public class PoiDescriptionFr
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
    }
}