
using System.Collections.Generic;

namespace Exposeum.TempModels
{
    public class Storyline
    {
        public int _storylineId { get; set; }
        public int _imageId { get; set; }
        public int _duration { get; set; }
        public int _floorsCovered { get; set; }
        public List<MapElement> _mapElements { get; set; }
        public string _intendedAudience { get; set;}
        public MapElement _lastVisitedMapElement { get; set; }
        public Models.Status _status; 
        public StorylineDescription _storylineDescription { get; set; }

        public Storyline()
        {
            _mapElements = new List<MapElement>();
        }
    }
}