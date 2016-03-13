using SQLite;

namespace Exposeum.Tables
{
    [Table("Items")]
    class PoiDescriptionFr
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string title { get; set; }
        public string sumary { get; set; }
    }
}