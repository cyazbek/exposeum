using System;

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

        public void SetVisited()
        {
            Visited = true;
        }
	}
}
