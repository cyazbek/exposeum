

namespace Exposeum.TempModels
{
    public class PointOfInterestDescription
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public Models.Language Language { get; set; }
        public override bool Equals(object obj)
        {
            if(obj!=null)
            {
                PointOfInterestDescription other = (PointOfInterestDescription)obj;
                return other.Id == Id && other.Description.Equals(Description) && other.Language.Equals(Language) && other.Summary.Equals(Summary); 
            }
            return false; 
        }
    }
}