namespace Exposeum.Models
{
    public class VideoContent : ExhibitionContent
    {
        private string filePath { get; set; }
        private int durationInSeconds { get; set; }
        private int resolution { get; set; }
        private string encoding { get; set; }

        public override string htmlFormat()
        {
            throw new System.NotImplementedException();
        }
    }
}