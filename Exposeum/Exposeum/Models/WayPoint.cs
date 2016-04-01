using System;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Exposeum.Models
{
	public class WayPoint : MapElement
	{
        public WaypointLabel Label { get; set; }
        private readonly float _iconScaleFactor = 0.2f;
        public Drawable _Icon { get; set; }
        public WayPoint(float UCoordinate, float vCoordinate, Floor floor) : base (UCoordinate, vCoordinate, floor)
		{
		}
        public bool Equals(WayPoint other)
        {
                return Label.Equals(other.Label);
        }
        public override void Draw(Canvas canvas)
        {
            /*canvas.Save();

            canvas.Translate(UCoordinate * Floor.FloorPlan.IntrinsicWidth, VCoordinate * Floor.FloorPlan.IntrinsicHeight);
            canvas.Scale(_iconScaleFactor, _iconScaleFactor);
            canvas.Translate(-_Icon.IntrinsicWidth / 2.0f, -_Icon.IntrinsicHeight / 2.0f);
            _Icon.Draw(canvas);
            canvas.Restore();*/
        }

    }
}

