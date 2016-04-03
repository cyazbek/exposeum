
using System.Collections.Generic;

namespace Exposeum.TempModels
{
    public class Storyline
    {
        public int StorylineId { get; set; }
        public string ImgPath { get; set; }
        public int Duration { get; set; }
        public int FloorsCovered { get; set; }
        public Models.Status Status { get; set; }
        public StorylineDescription StorylineDescription { get; set; }

        public List<MapElement> MapElements { get; set; }
        public PointOfInterest LastVisitedPointOfInterest { get; set; }


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
                    if (other.MapElements[i].Equals(MapElements[i]))
                        result = true;
                    else return false;
                }
                return result && other.StorylineId == StorylineId && other.ImgPath == ImgPath && other.Duration == Duration && other.FloorsCovered == FloorsCovered &&
                     LastVisitedPointOfInterest.Equals(other.LastVisitedPointOfInterest) && Status.Equals(other.Status) &&
                    StorylineDescription == other.StorylineDescription;
            }
            return false;
        }
    }
}