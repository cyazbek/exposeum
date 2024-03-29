﻿using Android.Graphics;

namespace Exposeum.Models
{
	public class Edge
	{
		private PointOfInterest start, end;
		private Paint paint = new Paint();
		private int strokeWidth = 20;

		public Edge (PointOfInterest start, PointOfInterest end)
		{
			this.start = start;
			this.end = end;

			paint.SetStyle (Paint.Style.Fill);
			paint.Color = Color.Purple;
			paint.StrokeWidth = strokeWidth;
		}

		public void Draw(Canvas canvas, float mapWidth, float mapHeight){
			canvas.DrawLine (start.U * mapWidth, start.V* mapHeight, end.U * mapWidth, end.V * mapHeight, paint);
		}
	}
}
