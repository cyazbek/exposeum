

using Java.Lang;

namespace Exposeum.TempModels
{
    public class Edge
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public MapElement Start { get; set; }
        public MapElement End { get; set; }

        public override bool Equals(object obj)
        {
            Edge other = (Edge) obj;

            return Id == other.Id &&
                  Math.Abs(Distance - other.Distance) < 0 &&
                  Start.Equals(other.Start) &&
                  End.Equals(other.End);
        }

    }
}