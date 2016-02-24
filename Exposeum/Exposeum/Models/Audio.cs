namespace Exposeum.Models
{
    public class Audio : ExhibitionContent
    {
        public string filePath { get; set; }
        public int durationInSeconds { get; set; }
        public string encoding { get; set; }

        public override string HtmlFormat()
        {
            throw new System.NotImplementedException();
        }
    }
}