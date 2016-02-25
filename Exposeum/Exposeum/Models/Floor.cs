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
		private List<MapElement> _floorMapElements = new List<MapElement>();
		private Paint paint = new Paint();

		public Floor (Drawable floorPlan)
		{
			_floorPlan = floorPlan;
			_floorPlan.SetBounds (0, 0, _floorPlan.IntrinsicWidth, _floorPlan.IntrinsicHeight);

			paint.SetStyle (Paint.Style.Fill);
			paint.Color = Color.Purple;
			paint.StrokeWidth = 20; //magic number, should extract static constant
		}

		public void addMapElement(MapElement e)
		{
			_floorMapElements.Add (e);
		}

		public Drawable Image
		{
			get { return this._floorPlan; }
		}

		public List<MapElement> MapElements
		{
			get { return this._floorMapElements; }
		}

		public List<MapElement> PointsOfInterest()
		{
			return _floorMapElements.OfType<MapElement>().ToList();
		}
	}
}
