using SQLite; 

namespace Exposeum.Tables
{
    public class ExhibitionContentList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int PoiId { get; set; }
        public int ExhibitionContentId { get; set; }
    }