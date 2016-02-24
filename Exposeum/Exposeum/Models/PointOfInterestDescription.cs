namespace Exposeum.Models
{
    public class PointOfInterestDescription
    {
        public string title { get; set; }
        public string summary { get; set; }
        public string language { get; set; }
        public PointOfInterestDescription()
        {
            this.title = "Title not provided";
            this.summary = "Summary not provided";
            this.language = "English";
        }
    }
}