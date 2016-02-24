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

		public void Draw(Canvas canvas)
		{
			_floorPlan.Draw (canvas);

			//draw edges on top of map
			for (int i = 0; i < _floorMapElements.Count - 1; i++) {

				MapElement start = _floorMapElements [i];
				MapElement end = _floorMapElements [i + 1];
					
				canvas.DrawLine (start._u * _floorPlan.IntrinsicWidth, start._v * _floorPlan.IntrinsicHeight, end._u * _floorPlan.IntrinsicWidth, end._v * _floorPlan.IntrinsicHeight, paint);
			}

			//draw mapelements on top of edges
			foreach (PointOfInterest poi in this.PointsOfInterest())
				poi.Draw (canvas, _floorPlan.IntrinsicWidth, _floorPlan.IntrinsicHeight);
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
