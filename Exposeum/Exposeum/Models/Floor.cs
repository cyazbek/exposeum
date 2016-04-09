using Android.Graphics.Drawables;
using Android.Graphics;

namespace Exposeum.Models
{
    public class Floor
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Drawable FloorPlan { get; set; }
        private readonly Paint _paint = new Paint();
        public float Width { get; set; }
        public float Height { get; set; }

        public Floor(Drawable floorPlan)
        {
            FloorPlan = floorPlan;
            FloorPlan.SetBounds(0, 0, FloorPlan.IntrinsicWidth, FloorPlan.IntrinsicHeight);

            _paint.SetStyle(Paint.Style.Fill);
            _paint.Color = Color.Purple;
            _paint.StrokeWidth = 20; //magic number, should extract static constant
        }

        public Drawable Image
        {
            get { return FloorPlan; }
        }

        public bool IsEqual(Floor floor)
        {
            return Id == floor.Id && ImagePath == floor.ImagePath;
        }

    }
}