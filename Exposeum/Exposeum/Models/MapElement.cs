using System;
using Android.Graphics;

namespace Exposeum.Models
{
    public abstract class MapElement
    {
        public int Id { get; set; }
        public bool Visited { get; set; }
        public string IconPath { get; set; }
        public float UCoordinate { get; set; }
        public float VCoordinate { get; set; }
        public Floor Floor { get; set; }

        

        protected MapElement()
        {
        }

        protected MapElement(float uCoordinate, float vCoordinate, Floor floor)
        {
            UCoordinate = uCoordinate;
            VCoordinate = vCoordinate;
            Visited = false;

            Floor = floor;
        }

        public abstract void Draw(Canvas canvas);

        public MapElement ShallowCopy()
        {
            return (MapElement)MemberwiseClone();
        }
    }
}