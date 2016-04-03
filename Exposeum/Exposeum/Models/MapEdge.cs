using QuickGraph;

namespace Exposeum.Models
{
	public class MapEdge : IEdge<MapElement>
	{
		public double Distance {get; set;}

        public MapEdge(MapElement start, MapElement end)
        {
            Source = start;
            Target = end;
            Distance = 1;
        }

        public MapEdge (MapElement start, MapElement end, double distance)
		{
			Source = start;
			Target = end;
		    Distance = distance;
		}

		public MapElement Source { get; set;}
		public MapElement Target { get; set;}
	}
}
