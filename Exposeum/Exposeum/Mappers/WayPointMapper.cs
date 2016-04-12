using Android.Graphics.Drawables;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.Models;

namespace Exposeum.Mappers
{
    public class WayPointMapper
    {
        private static WayPointMapper _instance;
        private readonly FloorMapper _floorMapper;
        private readonly MapElementsTdg _mapElementsTdg; 

        private WayPointMapper()
        {
            _floorMapper = FloorMapper.GetInstance();
            _mapElementsTdg = MapElementsTdg.GetInstance();
        }

        public static WayPointMapper GetInstance()
        {
            if (_instance == null)
                _instance = new WayPointMapper();
            return _instance;
        }
        public string ConvertLabelToString(WaypointLabel label)
        {
            switch (label)
            {
                case WaypointLabel.Elevator:
                    return "Elevator";
                case WaypointLabel.EmergencyExit:
                    return "EmergencyExit";
                case WaypointLabel.Entrance:
                    return "Entrance";
                case WaypointLabel.Exit:
                    return "Exit";
                case WaypointLabel.Ramp:
                    return "Ramp";
                case WaypointLabel.Stairs:
                    return "Stairs";
                case WaypointLabel.None:
                    return "None";
                default:
                    return "Washroom";
            }
        }

        public WaypointLabel ConvertStringToLabel(string label)
        {
            switch (label)
            {
                case "Elevator":
                    return WaypointLabel.Elevator;
                case "EmergencyExit":
                    return WaypointLabel.EmergencyExit;
                case "Entrance":
                    return WaypointLabel.Entrance;
                case "Exit":
                    return WaypointLabel.Exit;
                case "Ramp":
                    return WaypointLabel.Ramp;
                case "Stairs":
                    return WaypointLabel.Stairs;
                case "None":
                    return WaypointLabel.None;
                default:
                    return WaypointLabel.Washroom;
            }
        }

        public MapElements WaypointModelToTable(WayPoint element)
        {
            int vis;
            if (element.Visited == true)
                vis = 1;
            else vis = 0;

            return new MapElements
            {
                Id = element.Id,
                Visited = vis,
                UCoordinate = element.UCoordinate,
                VCoordinate = element.VCoordinate,
                FloorId = element.Floor.Id,
                IconPath = element.IconPath,
                Label = ConvertLabelToString(element.Label)
            };
        }

        public WayPoint WaypointTableToModel(MapElements element)
        {
            bool vis;
            if (element.Visited == 0)
                vis = false;
            else vis = true;

            return new WayPoint(element.UCoordinate, element.VCoordinate, _floorMapper.GetFloor(element.FloorId))
            {
                Id = element.Id,
                Visited = vis,
                Label = ConvertStringToLabel(element.Label),
                IconPath = element.IconPath,
				//Icon = (BitmapDrawable)Drawable.CreateFromStream (System.IO.File.OpenRead (element.IconPath), null) //not yet implemented in JSON
            };
        }

        public void Add(WayPoint element)
        {
            MapElements mapElement = WaypointModelToTable(element);
            _mapElementsTdg.Add(mapElement);
            _floorMapper.AddFloor(element.Floor);
        }

        public WayPoint Get(int id)
        {
            MapElements mapElement = _mapElementsTdg.GetMapElement(id);
            return WaypointTableToModel(mapElement);
        }

        public void Update(WayPoint element)
        {
            _mapElementsTdg.Update(WaypointModelToTable(element));
            _floorMapper.UpdateFloor(element.Floor);
        }
    }   
}


