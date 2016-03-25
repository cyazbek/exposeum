using SQLite;

namespace Exposeum.Tables
{
    public class Icon
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public string Path { get; set; }

    }
}