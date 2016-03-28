using Android.Graphics.Drawables;
using Android.Graphics;

namespace Exposeum.Models
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
        public Floor(Drawable floorPlan)
        {
            FloorPlan = floorPlan;
            FloorPlan.SetBounds(0, 0, FloorPlan.IntrinsicWidth, FloorPlan.IntrinsicHeight);

            Paint.SetStyle(Paint.Style.Fill);
            Paint.Color = Color.Purple;
            Paint.StrokeWidth = 20; //magic number, should extract static constant
        }
        public override bool Equals(object obj)
        {
            Floor other = (Floor)obj;
            return Id == other.Id && ImagePath.Equals(other.ImagePath);
        }
		public Drawable Image
		{
			get { return FloorPlan; }
		}
	}
}
