using System;
using Android.App;
using Android.Graphics;

namespace Exposeum.Models
{
	public class PointOfInterest
	{
		private float _u;
		private float _v;
		private String _label = "no label";
		private float _radius = 30;
		private readonly Paint _paint = new Paint();

		public PointOfInterest (float u, float v, string label)
		{
			this._u = u;
			this._v = v;

			this._label = label;

			_paint.SetStyle (Paint.Style.Fill);
			_paint.Color = Color.OrangeRed;
		}

		public string Label
		{
			set { this._label = value; }
			get { return this._label; }
		}

		public float U
		{
			set { this._u = value; }
			get { return this._u; }
		}

		public float V
		{
			set { this._v = value; }
			get { return this._v; }
		}

		public float Radius
		{
			set { this._radius = value; }
			get { return this._radius; }
		}

		public void Draw(Canvas canvas, float mapWidth, float mapHeight)
		{
			canvas.DrawCircle (_u * mapWidth, _v * mapHeight, _radius, _paint);
		}

		public void SetTouched()
		{
			_paint.Color = Color.ForestGreen;
		}

		public String getHTML()
		{
			String summary = String.Format ("<html><body>You selected POI {0}!<br><br></body></html>", _label);
			return summary;
		}
	}
}
