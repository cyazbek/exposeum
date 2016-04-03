using Android.Graphics;
using Android.Graphics.Drawables;

namespace Exposeum.TempModels
{
    public class Floor
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Drawable FloorPlan { get; set; }
        public Paint Paint { get; set; }

        public Floor()
        {
            Paint = new Paint(); 
        }

        public bool IsEqual(Floor floor)
        {
            return Id == floor.Id && ImagePath== floor.ImagePath;
        }
    }
}
//transferred to Models. 