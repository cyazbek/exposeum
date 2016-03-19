

using Java.Lang;

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
            MapElement other = (MapElement) obj;

            return _id == other._id &&
                   _iconId == other._iconId &&
                   Math.Abs(_uCoordinate - other._uCoordinate) < 0 &&
                   Math.Abs(_vCoordinate - other._vCoordinate) < 0;
        }
    }
}