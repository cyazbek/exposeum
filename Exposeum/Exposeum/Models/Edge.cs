using Android.Graphics;

namespace Exposeum.Models
{
	public class Edge
	{
		private MapElement start {get; set;}
		private MapElement end {get; set;}
		private double distance {get; set;}

		public Edge (MapElement start, MapElement end)
		{
			this.start = start;
			this.end = end;
		}
	}
}
