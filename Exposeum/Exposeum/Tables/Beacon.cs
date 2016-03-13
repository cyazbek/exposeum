using SQLite;

namespace Exposeum.Tables
{
    [Table("Items")]
    class Beacon
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string UUID { get; set; }
        public int major { get; set; }
        public int minor { get; set; }
    }
}