
namespace Exposeum.TempModels
{
    public class PointOfInterest:MapElement
    {
        public Beacon Beacon {get; set; }      
        public int StoryLineId { get; set; }
        public PointOfInterestDescription Description { get; set; }
        public ExhibitionContent ExhibitionContent { get; set; }
        public PointOfInterest()
        {
            Beacon = new Beacon();  
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                PointOfInterest other = (PointOfInterest)obj;
                return Beacon.Equals(other.Beacon) &&
                StoryLineId == other.StoryLineId &&
                Description.Equals(other.Description) &&
                ExhibitionContent.Equals(other.ExhibitionContent);
            }
            else return false; 
        }
    }
}