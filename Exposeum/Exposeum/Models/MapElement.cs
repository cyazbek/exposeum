using System;
using Android.Graphics;

namespace Exposeum.Models
{
    public abstract class MapElement
    {
        public int Id { get; set; }
        public bool Visited { get; set;}
        public string IconId { get; set; }
        public float UCoordinate { get; set; }
        public float VCoordinate { get; set; }
        public Floor Floor { get; set; }

        protected MapElement()
        {
            
        }
        protected MapElement(float uCoordinate, float vCoordinate, Floor floor)
		{
			this.UCoordinate = UCoordinate;
			this.VCoordinate = VCoordinate;
			Visited = false;
			Floor = floor;
		}
        public abstract void Draw(Canvas canvas);

		public MapElement ShallowCopy(){
			return (MapElement)MemberwiseClone ();
		}
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                MapElement other = (MapElement)obj;
                return other.Id == Id &&
                    other.Visited == Visited &&
                    other.IconId == IconId &&
                    Math.Abs(other.UCoordinate - UCoordinate) < 0 &&
                    Math.Abs(other.VCoordinate - VCoordinate) < 0 &&
                    other.Floor.Equals(Floor);
            }
            return false;
        }
    }
}
