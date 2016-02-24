namespace Exposeum.Models
{
    public class AudioContent : ExhibitionContent
    {
        private string filePath { get; set; }
        private int durationInSeconds { get; set; }
        private string encoding { get; set; }

        public override string htmlFormat()
        {
            throw new System.NotImplementedException();
        }
    }
}