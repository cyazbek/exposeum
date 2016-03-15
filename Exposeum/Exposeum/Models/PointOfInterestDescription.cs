namespace Exposeum.Models
{
    public class PointOfInterestDescription
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }


        public PointOfInterestDescription(string title, string summary, string description)
        {
            Title = title;
            Summary = summary;
            Description = description;
        }

        public string GetFullDescriptionHtml()
        {
            return string.Format("<html><body><h1 style=\"color:#c91215;\" >{0}</h1><h2>{1}</h2>" +
                                 "<img src=\"EmileBerliner.png\"/></body></html>"
                , Title,Description);
        }

        public string GetOnlySummaryHtml()
        {
            return string.Format("<html><body><h1>{0}</h1><h2>{1}</h2></body></html>"
                , Title, Summary);
        }
    }
}