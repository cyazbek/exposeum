
using System.Collections.Generic;
using Android.Graphics;

namespace Exposeum.TempModels
{
    public class PointOfInterest:MapElement
    {
        public Beacon Beacon {get; set; }      
        public int StoryLineId { get; set; }
        public PointOfInterestDescription Description { get; set; }
        public List<ExhibitionContent> ExhibitionContent { get; set; }
        public PointOfInterest()
        {
            Beacon = new Beacon();  
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                PointOfInterest other = (PointOfInterest)obj;

                return TempModels.ExhibitionContent.ListEquals(ExhibitionContent, other.ExhibitionContent) &&
                       Beacon.Equals(other.Beacon) &&
                       StoryLineId == other.StoryLineId &&
                       Description.Equals(other.Description);
            }
            return false;
        }

        public override void Draw(Canvas canvas)
        {
            throw new System.NotImplementedException();
        }
    }
}