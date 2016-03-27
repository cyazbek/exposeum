
using System.Collections.Generic;

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
                bool listComparator = false;
                for (int i = 0; i < ExhibitionContent.Count; i++)
                {
                    if (other.ExhibitionContent[i].Equals(this.ExhibitionContent[i]))
                        listComparator = true;
                    else return false;
                }
                return Beacon.Equals(other.Beacon) &&
                StoryLineId == other.StoryLineId &&
                Description.Equals(other.Description) &&
                listComparator &&
                ExhibitionContent.Equals(other.ExhibitionContent);
            }
            return false;
        }
    }
}