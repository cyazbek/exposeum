using System;
using System.Collections.Generic;

using Android.Graphics.Drawables;
using Android.Graphics;
using System.Linq;

namespace Exposeum.Models
{
	public class Floor
	{
		private Drawable _floorPlan;
		private Paint paint = new Paint();

		public Floor (Drawable floorPlan)
		{
			_floorPlan = floorPlan;
			_floorPlan.SetBounds (0, 0, _floorPlan.IntrinsicWidth, _floorPlan.IntrinsicHeight);

			paint.SetStyle (Paint.Style.Fill);
			paint.Color = Color.Purple;
			paint.StrokeWidth = 20; //magic number, should extract static constant
		}

		public Drawable Image
		{
			get { return this._floorPlan; }
		}
	}
}
