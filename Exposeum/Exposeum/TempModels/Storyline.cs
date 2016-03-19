
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
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                Storyline other = (Storyline)obj;
                var result = false;
                for (int i = 0; i < other._mapElements.Count; i++)
                {
                    if (other._mapElements[i].Equals(this._mapElements[i]))
                        result = true;
                    else return false;
                }
                return result && other._storylineId == _storylineId && other._imageId == _imageId && other._duration == _duration && other._floorsCovered == _floorsCovered &&
                    _intendedAudience == other._intendedAudience && _lastVisitedMapElement.Equals(other._lastVisitedMapElement) && _status.Equals(other._status) &&
                    _storylineDescription == other._storylineDescription;
            }
            else return false; 
        }
    }
}