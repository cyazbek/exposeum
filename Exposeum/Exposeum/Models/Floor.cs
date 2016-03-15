using Android.Graphics.Drawables;
using Android.Graphics;

namespace Exposeum.Models
{
	public class Floor
	{
		private Drawable _floorPlan;
		private Paint _paint = new Paint();

		public Floor (Drawable floorPlan)
		{
			_floorPlan = floorPlan;
			_floorPlan.SetBounds (0, 0, _floorPlan.IntrinsicWidth, _floorPlan.IntrinsicHeight);

			_paint.SetStyle (Paint.Style.Fill);
			_paint.Color = Color.Purple;
			_paint.StrokeWidth = 20; //magic number, should extract static constant
		}

		public Drawable Image
		{
			get { return this._floorPlan; }
		}
	}
}
