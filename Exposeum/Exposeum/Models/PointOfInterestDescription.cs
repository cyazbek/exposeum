using Android.Graphics;

namespace Exposeum.Models
{
    public class PointOfInterestDescription
    {

        public string title { get; set; }
        public string  summary { get; set; }
        public string description { get; set; }

        public PointOfInterestDescription(string title, string summary, string description)
        {
            this.title = title;
            this.summary = summary;
            this.description = description;
        }

        public string htmlFormat()
        {
            return string.Format("<html><body><h1>{0}</h1><h2>{1}</h2></body></html>", title,summary);
        }

    }
}