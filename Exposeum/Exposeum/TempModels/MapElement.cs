

namespace Exposeum.TempModels
{
    public abstract class MapElement
    {
        public int Id { get; set; }
        public bool Visited { get; set; }
        public int IconId { get; set; }
        public float UCoordinate { get; set; }
        public float VCoordinate { get; set; }
        public Floor Floor { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                MapElement other = (MapElement)obj;
                return other.Id == Id &&
                    other.Visited == Visited &&
                    other.IconId == IconId &&
                    other.UCoordinate == UCoordinate &&
                    other.VCoordinate == VCoordinate &&
                    other.Floor.Equals(Floor);
            }
            return false;
        }
    }
}