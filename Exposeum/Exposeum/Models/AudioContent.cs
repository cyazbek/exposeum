namespace Exposeum.Models
{
    public class AudioContent : ExhibitionContent
    {
        private string FilePath { get; set; }
        private int DurationInSeconds { get; set; }
        private string Encoding { get; set; }

        public override string HtmlFormat()
        {
            throw new System.NotImplementedException();
        }
    }
}