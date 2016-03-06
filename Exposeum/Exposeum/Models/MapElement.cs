using System;
using Android.Graphics;

namespace Exposeum.Models
{
    public abstract class MapElement
	{
		public float _u, _v;
        public Floor floor;

        public Boolean Visited { get; set; }

        protected MapElement ()
		{
		}

		protected MapElement(float u, float v, Floor floor)
		{
			this._u = u;
			this._v = v;
			Visited = false;

			this.floor = floor;
		}

        public void SetVisited()
        {
            Visited = true;
        }

        public abstract void Draw(Canvas canvas);
	}
}
