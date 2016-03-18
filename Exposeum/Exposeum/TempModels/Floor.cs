

using Android.Graphics;

namespace Exposeum.TempModels
{
    public class Floor
    {
        public int _floorPlan { get; set; } 
        public Paint _paint { get; set}

        public Floor()
        {
            this._paint = new Paint(); 
        }

    }
}