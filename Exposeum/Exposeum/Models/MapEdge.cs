using Android.Graphics;
using Java.Lang;
using QuickGraph;

namespace Exposeum.Models
{
	public class MapEdge : IEdge<MapElement>
	{
		private double Distance {get; set;}

        public MapEdge(MapElement start, MapElement end)
        {
            this.Source = start;
            this.Target = end;
            this.Distance = 0;
        }

        public MapEdge (MapElement start, MapElement end, double distance)
		{
			this.Source = start;
			this.Target = end;
		    this.Distance = distance;
		}

	    public MapElement Source { get; }
	    public MapElement Target { get; }
	}
}
