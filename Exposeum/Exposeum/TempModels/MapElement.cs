using Android.Graphics;
using System;
using Android.Graphics;

namespace Exposeum.TempModels
{
    public abstract class MapElement
    {
        public int Id { get; set; }
        public bool Visited { get; set; }
        public string IconPath { get; set; }
        public float UCoordinate { get; set; }
        public float VCoordinate { get; set; }
        public Floor Floor { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                MapElement other = (MapElement)obj;
                return other.Id == Id &&
                    other.Visited == Visited &&
                    other.IconPath.Equals(IconPath) &&
                    Math.Abs(other.UCoordinate - UCoordinate) < 0 &&
                    Math.Abs(other.VCoordinate - VCoordinate) < 0 &&
                    other.Floor.IsEqual(Floor);
            }
            return false;
        }

        public abstract void Draw(Canvas canvas);

    }
}
//transferred to models
