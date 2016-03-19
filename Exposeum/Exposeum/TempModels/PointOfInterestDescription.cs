

namespace Exposeum.TempModels
{
    public class PointOfInterestDescription
    {
        public int _id { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public Models.Language _language { get; set; }
    }
}