
using System.Collections.Generic;

namespace Exposeum.TempModels
{
    public class Storyline
    {
        public int StorylineId { get; set; }
        public int ImageId { get; set; }
        public int Duration { get; set; }
        public int FloorsCovered { get; set; }
        public List<MapElement> MapElements { get; set; }
        public string IntendedAudience { get; set;}
        public MapElement LastVisitedMapElement { get; set; }
        public Models.Status Status; 
        public StorylineDescription StorylineDescription { get; set; }

        public Storyline()
        {
            MapElements = new List<MapElement>();
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                Storyline other = (Storyline)obj;
                var result = false;
                for (int i = 0; i < other.MapElements.Count; i++)
                {
                    if (other.MapElements[i].Equals(this.MapElements[i]))
                        result = true;
                    else return false;
                }
                return result && other.StorylineId == StorylineId && other.ImageId == ImageId && other.Duration == Duration && other.FloorsCovered == FloorsCovered &&
                    IntendedAudience == other.IntendedAudience && LastVisitedMapElement.Equals(other.LastVisitedMapElement) && Status.Equals(other.Status) &&
                    StorylineDescription == other.StorylineDescription;
            }
            return false;
        }
    }
}