namespace Exposeum.TempModels
{
    public class WayPoint:MapElement
    {
        public WaypointLabel _label { get; set; } 
        public bool Equals(WayPoint obj)
        {
            if (base.Equals(obj) && this._label.Equals(obj._label))
                return true;
            else return false; 
        }
    }
}