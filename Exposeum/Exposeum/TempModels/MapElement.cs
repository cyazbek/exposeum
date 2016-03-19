

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
        public bool Equals(MapElement element2)
        {
            if (this._id == element2._id &&
                this._visited == element2._visited &&
                this._iconId == element2._iconId &&
                this._uCoordinate == element2._uCoordinate &&
                this._vCoordinate == element2._vCoordinate &&
                this.Equals(element2._floor))
               return true;
            else return false; 
        }

    }
}