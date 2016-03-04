using System;
using Android.Graphics;

namespace Exposeum.Models
{
	public abstract class Node: MapElement
	{

		public int id { get; set; }
		public int storyID { get; set; }
		public Boolean visited { get; set; }

		public Node ()
		{
			visited = false;
		}

		public Node(float u, float v)
		{
			this._u = u;
			this._v = v;

			visited = false;
		}

		public Node(float u, float v, Floor floor)
		{
			this._u = u;
			this._v = v;
			visited = false;

			this.floor = floor;
		}

		public void SetTouched()
		{
			visited = true;
		}
	}
}

