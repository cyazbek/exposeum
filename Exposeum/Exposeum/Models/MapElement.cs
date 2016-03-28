using System;
using Android.Graphics;

namespace Exposeum.Models
{
    public abstract class MapElement
	{
		public float UCoordinate, V;
        public Floor Floor;

        public Boolean Visited { get; set; }

        protected MapElement ()
		{
		}

		protected MapElement(float UCoordinate, float v, Floor floor)
		{
			UCoordinate = UCoordinate;
			V = v;
			Visited = false;

			Floor = floor;
		}

        public void SetVisited()
        {
            Visited = true;
        }

        public abstract void Draw(Canvas canvas);

		public MapElement ShallowCopy(){
			return (MapElement)MemberwiseClone ();
		}
	}
}
