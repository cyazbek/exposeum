using System;
using System.Collections.Generic;
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

        public static bool ListEquals(List<MapElement> contents, List<MapElement> expected)
        {
            if (contents.Count != expected.Count)
                return false;

            bool areEquals = false;

            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i].GetType().ToString() == "Exposeum.Models.PointOfInterest")
                    areEquals = ((PointOfInterest)contents[i]).AreEquals((PointOfInterest)expected[i]);
                else
                    areEquals = ((WayPoint)contents[i]).Equals((WayPoint)expected[i]);
            }

            return areEquals;
        }
    }
}