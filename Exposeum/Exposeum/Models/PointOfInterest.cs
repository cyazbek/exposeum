using System;
using Android.Graphics;

namespace Exposeum
{
	public class PointOfInterest
	{
		private float u;
		private float v;
		private String label;
		private float radius = 50;
		private Paint paint = new Paint();

		public PointOfInterest ()
		{
			u = 0.0f;
			v = 0.0f;

			paint.SetStyle (Paint.Style.Fill);
			paint.Color = Color.OrangeRed;
		}

		public PointOfInterest (float u, float v)
		{
			this.u = u;
			this.v = v;

			paint.SetStyle (Paint.Style.Fill);
			paint.Color = Color.OrangeRed;
		}

		public string Label
		{
			set { this.label = value; }
			get { return this.label; }
		}

		public float U
		{
			set { this.u = value; }
			get { return this.u; }
		}

		public float V
		{
			set { this.v = value; }
			get { return this.v; }
		}

		public void Draw(Canvas canvas, float mapWidth, float mapHeight){
			canvas.DrawCircle (u * mapWidth, v * mapHeight, radius, paint);
		}
	}
}
