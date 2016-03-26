using SQLite;

namespace Exposeum.Tables
{
      public class ExhibitionContentFr
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Filepath { get; set; }
        public int Duration { get; set; }
        public int Resolution { get; set; }
        public string Encoding { get; set; }
        public string Discriminator { get; set; }
        public int StoryLineId { get; set; }
    }
}