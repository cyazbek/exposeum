using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.TempModels;

namespace Exposeum.Mappers
{
    public class WayPointMapper
    {
        private static WayPointMapper _instance;
        private FloorMapper _floorMapper;
        private MapElementsTdg _mapElementsTdg; 

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

        public Tables.MapElements ConvertFromModel(PointOfTravel element)
        {
            int vis;
            if (element.Visited == true)
                vis = 1;
            else vis = 0;


            return new MapElements()
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

        public PointOfTravel ConvertFromTable(MapElements element)
        {
            bool vis;
            if (element.Visited == 0)
                vis = false;
            else vis = true;

            return new PointOfTravel()
            {
                Id = element.Id,
                Visited = vis,
                Floor = _floorMapper.GetFloor(element.FloorId),
                Label = ConvertStringToLabel(element.Label),
                IconPath = element.IconPath,
                UCoordinate = element.UCoordinate,
                VCoordinate = element.VCoordinate
            };
        }

        public void Add(PointOfTravel element)
        {
            Tables.MapElements mapElement = ConvertFromModel(element);
            _mapElementsTdg.Add(mapElement);
            _floorMapper.AddFloor(element.Floor);
        }

        public PointOfTravel Get(int id)
        {
            MapElements mapElement = _mapElementsTdg.GetMapElement(id);
            return ConvertFromTable(mapElement);
        }

        public void Update(PointOfTravel element)
        {
            _mapElementsTdg.Update(ConvertFromModel(element));
            _floorMapper.UpdateFloor(element.Floor);
        }

    }

        
}


