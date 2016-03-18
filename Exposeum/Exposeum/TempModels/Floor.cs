using Android.Graphics;
using Android.Graphics.Drawables;

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

    }
}