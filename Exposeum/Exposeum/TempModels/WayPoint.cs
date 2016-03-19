using System;

namespace Exposeum.TempModels
{
    public class WayPoint:MapElement
    {
        public WaypointLabel _label { get; set; } 
        public override bool Equals(Object obj)
        {
            if (obj != null)
            {
                WayPoint other = (WayPoint)obj;
                return _label.Equals(other._label);
            }
            else return false; 
        }
    }
}