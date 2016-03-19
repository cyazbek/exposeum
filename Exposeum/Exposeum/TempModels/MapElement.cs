

namespace Exposeum.TempModels
{
    public abstract class MapElement
    {
        public int _id { get; set; }
        public bool _visited { get; set; }
        public int _iconId { get; set; }
        public float _uCoordinate { get; set; }
        public float _vCoordinate { get; set; }
        public Floor _floor { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                MapElement other = (MapElement)obj;
                return other._id == _id &&
                    other._visited == _visited &&
                    other._iconId == _iconId &&
                    other._uCoordinate == _uCoordinate &&
                    other._vCoordinate == _vCoordinate &&
                    other._floor.Equals(_floor);
            }
            else return false; 
        }

    }
}