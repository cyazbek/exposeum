

using SQLite;

namespace Exposeum.Tables
{
    public class Edge
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public double distance { get; set; }
        public int startMapElementId { get; set; }
        public int endMapElementId { get; set; }

    }
}