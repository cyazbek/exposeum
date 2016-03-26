using SQLite;

namespace Exposeum.Tables
{

    public class PoiDescriptionFr
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}