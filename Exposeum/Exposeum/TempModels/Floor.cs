using System.Runtime.ConstrainedExecution;
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

        public override bool Equals(object obj)
        {
            Floor other = (Floor) obj;
            return Id == other.Id && ImagePath.Equals(other.ImagePath);
        }
    }
}