

using Java.Lang;

namespace Exposeum.TempModels
{
    public class Edge
    {
        public int _id { get; set; }
        public double _distance { get; set; }
        public MapElement _start { get; set; }
        public MapElement _end { get; set; }

        public override bool Equals(object obj)
        {
            Edge other = (Edge) obj;

            return _id == other._id &&
                  Math.Abs(_distance - other._distance) < 0 &&
                  _start.Equals(other._start) &&
                  _end.Equals(other._end);
        }

    }
}