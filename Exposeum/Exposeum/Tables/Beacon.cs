using SQLite;

namespace Exposeum.Tables
{
  
    public class Beacon
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public string Uuid { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
    }
}