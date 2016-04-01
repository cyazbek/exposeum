using SQLite;

namespace Exposeum.Tables
{   

    public class Floor
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public string ImagePath { get; set; }

    }
}