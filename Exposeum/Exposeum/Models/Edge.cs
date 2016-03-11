using Android.Graphics;

namespace Exposeum.Models
{
	public class Edge
	{
		private MapElement Start {get; set;}
		private MapElement End {get; set;}
		private double Distance {get; set;}

		public Edge (MapElement start, MapElement end)
		{
			this.Start = start;
			this.End = end;
		}
	}
}
