using SQLite;

namespace Exposeum.Tables
{
    [Table("Items")]
    public class Icon
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string path { get; set; }

    }
}