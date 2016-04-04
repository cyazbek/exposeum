

using Java.Lang;

namespace Exposeum.TempModels
{
    public class MapEdge
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public MapElement Source { get; set; }
        public MapElement Target { get; set; }

        public override bool Equals(object obj)
        {
            MapEdge other = (MapEdge) obj;

            return Id == other.Id &&
                  Math.Abs(Distance - other.Distance) < 0 &&
                  Source.Equals(other.Source) &&
                  Target.Equals(other.Target);
        }

    }
}
//transferred to Models