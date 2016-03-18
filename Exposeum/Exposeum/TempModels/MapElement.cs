

namespace Exposeum.TempModels
{
    public abstract class MapElement
    {
        public int _MapElementId { get; set; }
        public bool _visited { get; set; }
        public bool _iconId { get; set; }
        public float _uCoordinate { get; set; }
        public float _vCoordinate { get; set; }
        public Floor _floor { get; set; }
    }
}