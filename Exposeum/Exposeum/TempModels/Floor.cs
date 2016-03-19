using Android.Graphics;

namespace Exposeum.TempModels
{
    public class Floor
    {
        public int _id { get; set; }
        public string _plan { get; set; } 
        public Paint _paint { get; set; }

        public Floor()
        {
            this._paint = new Paint(); 
        }

        public override bool Equals(object obj)
        {
            Floor other = (Floor) obj;
            return _id == other._id && _plan == other._plan;
        }
    }
}