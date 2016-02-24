namespace Exposeum.Models
{
    public class Video : ExhibitionContent
    {
        public string filePath { get; set; }
        public int durationInSeconds { get; set; }
        public int resolution { get; set; }
        public string encoding { get; set; }
        public override string HtmlFormat()
        {
            throw new System.NotImplementedException();
        }
    }
}