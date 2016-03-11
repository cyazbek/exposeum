using System;
using Android.Graphics;

namespace Exposeum.Models
{
    public abstract class MapElement
	{
		public float U, V;
        public Floor Floor;

        public Boolean Visited { get; set; }

        protected MapElement ()
		{
		}

		protected MapElement(float u, float v, Floor floor)
		{
			this.U = u;
			this.V = v;
			Visited = false;

			this.Floor = floor;
		}

        public void SetVisited()
        {
            Visited = true;
        }

        public abstract void Draw(Canvas canvas);
	}
}
