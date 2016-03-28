using System;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Exposeum.TempModels
{
    public class WayPoint:MapElement
    {
        public WaypointLabel Label { get; set; }

        private readonly float _iconScaleFactor = 0.2f;
        public Drawable Icon { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj != null)
            {
                WayPoint other = (WayPoint)obj;
                return Label.Equals(other.Label);
            }
            return false;
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Save();

            canvas.Translate(UCoordinate * Floor.FloorPlan.IntrinsicWidth, VCoordinate * Floor.FloorPlan.IntrinsicHeight);
            canvas.Scale(_iconScaleFactor, _iconScaleFactor);
            canvas.Translate(-Icon.IntrinsicWidth / 2.0f, -Icon.IntrinsicHeight / 2.0f);
            Icon.Draw(canvas);
            canvas.Restore();
        }
    }
}