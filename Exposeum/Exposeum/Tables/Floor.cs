using SQLite;

namespace Exposeum.Tables
{   

    public class Floor
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int imageId { get; set; }

    }
}