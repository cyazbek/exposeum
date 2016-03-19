
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
        public bool Equals(PointOfInterest obj)
        {
            if (base.Equals(obj) &&
                _beacon.Equals(obj._beacon) &&
                _storyLineId == obj._storyLineId &&
                _description.Equals(obj._description) &&
                _exhibitionContent.Equals(obj._exhibitionContent))
                return true;
            else return false; 
        }
    }
}