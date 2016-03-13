using SQLite;

namespace Exposeum.Tables
{
    [Table("Items")]
    public class ExhibitionContentFr
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string filepath { get; set; }
        public int duration { get; set; }
        public int resolution { get; set; }
        public string encoding { get; set; }
        public string discriminator { get; set; }
    }
}