namespace Exposeum.Models
{
    public class PointOfInterestDescription
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public Models.Language Language { get; set; }

        public PointOfInterestDescription()
        {
            
        }
        public PointOfInterestDescription(string title, string summary, string description)
        {
            Title = title;
            Summary = summary;
            Description = description;
        }

        public string GetFullDescriptionHtml()
        {
            return string.Format("<html><body><h1 style=\"color:#c91215;\" >{0}</h1><h2>{1}</h2>" +
                                 "</body></html>"
                , Title,Description);
        }

        public string GetOnlySummaryHtml()
        {
            return string.Format("<html><body><h1>{0}</h1><h2>{1}</h2></body></html>"
                , Title, Summary);
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                PointOfInterestDescription other = (PointOfInterestDescription)obj;
                return other.Id == Id && other.Description.Equals(Description) && other.Language.Equals(Language) && other.Summary.Equals(Summary);
            }
            return false;
        }

    }
}