

using SQLite;

namespace Exposeum.Tables
{
    public class Edge
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public double Distance { get; set; }
        public int StartMapElementId { get; set; }
        public int EndMapElementId { get; set; }

    }
}