namespace Exposeum.Models
{
    public class PointOfInterestDescription
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }


        public PointOfInterestDescription(string Title, string Summary, string Description)
        {
            this.Title = Title;
            this.Summary = Summary;
            this.Description = Description;
        }

        public string getFullDescriptionHTML()
        {
            return string.Format("<html><body><h1>{0}</h1><h2>{1}</h2><p>{2}</p>" +
                                 "<img src=\"EmileBerliner.png\"/></body></html>"
                , Title,Summary,Description);
        }

        public string getOnlySummaryHTML()
        {
            return string.Format("<html><body><h1>{0}</h1><h2>{1}</h2></body></html>"
                , Title, Summary);
        }
    }
}