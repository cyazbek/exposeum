using SQLite;

namespace Exposeum.Tables
{
    public class Images
    {   
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id{ get; set; }
        public string Path { get; set; }

    }
}