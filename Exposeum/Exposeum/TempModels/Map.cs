using System.Collections.Generic;

namespace Exposeum.TempModels
{
    public class Map
    {
        public int Id; 
        private static Map _instance;
        public List<MapEdge> Edges { get; set; }
        public List<Storyline> Storylines { get; set; }
        public List<MapElement> MapElements { get; set; }
        public Storyline CurrentStoryline { get; set; }
        public List<Floor> Floors { get; set; }
        public Floor CurrentFloor;  

        private Map()
        {
            Edges = new List<MapEdge>();
            Storylines = new List<Storyline>();
            Floors = new List<Floor>();
            MapElements = new List<MapElement>();
        }
        
        public static Map GetInstance()
        {
            if (_instance == null)
                _instance = new Map();
            return _instance; 
        }
    }
}