using System.Collections.Generic;

namespace Exposeum.TempModels
{
    public class Map
    {
        private static Map _instance;
        public List<Edge> _edges { get; set; }
        public List<Storyline> _storylines { get; set; }
        public Storyline _currentStoryline { get; set; }
        public List<Floor> _floors { get; set; }
        public Floor _currentFloor;  

        private Map()
        {
            _edges = new List<Edge>();
            _storylines = new List<Storyline>();
            _floors = new List<Floor>(); 
        }
        
        public static Map GetInstance()
        {
            if (_instance == null)
                _instance = new Map();
            return _instance; 
        }
    }
}