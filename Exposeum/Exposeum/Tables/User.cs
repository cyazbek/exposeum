using SQLite; 

namespace Exposeum.Tables
{
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public int Language { get; set; }
        public int Visitor { get; set; }
    }
}