
namespace Exposeum.TempModels
{
    public class PointOfInterest:MapElement
    {
        public int _pointOfInterestId { get; set; }
        public Beacon _beacon {get; set; }      
        public int _storyLineId { get; set; }
        public PointOfInterestDescription _description { get; set; }
        public ExhibitionContent _exhibitionContent { get; set; }
        public PointOfInterest()
        {
            _beacon = new Beacon();  
        }
    }
}