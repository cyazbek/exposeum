

using Java.Lang;

namespace Exposeum.TempModels
{
    public class MapEdge
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public MapElement Start { get; set; }
        public MapElement End { get; set; }

        public override bool Equals(object obj)
        {
            MapEdge other = (MapEdge) obj;

            return Id == other.Id &&
                  Math.Abs(Distance - other.Distance) < 0 &&
                  Start.Equals(other.Start) &&
                  End.Equals(other.End);
        }

    }
}