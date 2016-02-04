using System;
using Android.Graphics;

namespace Exposeum.Models
{
	public class PointOfInterest
	{
		private float u;
		private float v;
		private String label = "no label";
		private float radius = 30;
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

		public PointOfInterest (float u, float v, string label)
		{
			this.u = u;
			this.v = v;

			this.label = label;

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

		public float Radius
		{
			set { this.radius = value; }
			get { return this.radius; }
		}

		public void Draw(Canvas canvas, float mapWidth, float mapHeight){
			canvas.DrawCircle (u * mapWidth, v * mapHeight, radius, paint);
		}

		public void SetTouched(){
			paint.Color = Color.ForestGreen;
		}
	}
}
