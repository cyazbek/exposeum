
namespace Exposeum.TempModels
{
    public class PointOfInterest:MapElement
    {
        public Beacon _beacon {get; set; }      
        public int _storyLineId { get; set; }
        public PointOfInterestDescription _description { get; set; }
        public ExhibitionContent _exhibitionContent { get; set; }
        public PointOfInterest()
        {
            _beacon = new Beacon();  
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                PointOfInterest other = (PointOfInterest)obj;
                return _beacon.Equals(other._beacon) &&
                _storyLineId == other._storyLineId &&
                _description.Equals(other._description) &&
                _exhibitionContent.Equals(other._exhibitionContent);
            }
            else return false; 
        }
    }
}