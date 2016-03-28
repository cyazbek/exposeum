using QuickGraph;

namespace Exposeum.Models
{
	public class MapEdge : IEdge<MapElement>
	{
        public int Id { get; set; }
        public double Distance {get; set;}
        public MapElement Source { get; }
        public MapElement Target { get; }

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


	}
}
