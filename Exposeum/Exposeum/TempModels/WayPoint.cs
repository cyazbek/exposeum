using System;

namespace Exposeum.TempModels
{
    public class WayPoint:MapElement
    {
        public WaypointLabel Label { get; set; } 
        public override bool Equals(Object obj)
        {
            if (obj != null)
            {
                WayPoint other = (WayPoint)obj;
                return Label.Equals(other.Label);
            }
            return false;
        }
    }
}