using QuickGraph;
using Java.Lang;

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
        public override bool Equals(object obj)
        {
            MapEdge other = (MapEdge)obj;

            return Id == other.Id &&
                  Math.Abs(Distance - other.Distance) < 0 &&
                  Source.Equals(other.Source) &&
                  Target.Equals(other.Target);
        }

    }
}
