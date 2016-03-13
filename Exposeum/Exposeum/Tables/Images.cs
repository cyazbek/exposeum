using SQLite;

namespace Exposeum.Tables
{
    [Table("Items")]
    public class Images
    {   
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID{ get; set; }
        public string path { get; set; }

    }
}