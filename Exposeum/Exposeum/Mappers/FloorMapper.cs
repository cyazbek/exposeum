using Exposeum.TDGs;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using Floor = Exposeum.TempModels.Floor;

namespace Exposeum.Mappers
{
    public class FloorMapper
    {
        private static FloorMapper _instance;
        private readonly FloorTdg _floorTdg;

        private FloorMapper()
        {
            _floorTdg = FloorTdg.GetInstance();
        }

        public static FloorMapper GetInstance()
        {
            if (_instance == null)
                _instance = new FloorMapper();

            return _instance;
        }

        public void AddFloor(Floor floor)
        {
            Tables.Floor floorTable = FloorModelToTable(floor);
            _floorTdg.Add(floorTable);
        }

        public void UpdateFloor(Floor floor)
        {
            Tables.Floor floorTable = FloorModelToTable(floor);
            _floorTdg.Update(floorTable);
        }

        public Floor GetFloor(int floorId)
        {
            Tables.Floor floorTable = _floorTdg.GetFloor(floorId);
            Floor floor = FloorTableToModel(floorTable);
            return floor;
        }

        public List<Floor> GetAllFloors()
        {
            List<Tables.Floor> tableFloor = _floorTdg.GetAllFloors();
            List<Floor> modelFloor = new List<Floor>();

            foreach (var x in tableFloor)
            {
                modelFloor.Add(FloorTableToModel(x)); 
            }

            return modelFloor; 
        }

        public void UpdateFloorsList(List<Floor> list)
        {
            foreach(var x in list)
            {
                UpdateFloor(x);
            }
        }

        public Tables.Floor FloorModelToTable(Floor floor)
        {
            Tables.Floor floorTable = new Tables.Floor
            {
                Id = floor.Id,
                ImagePath = floor.ImagePath
            };

            return floorTable;
        }

        public Floor FloorTableToModel(Tables.Floor floorTable)
        {
            Floor modelFloor = new Floor
            {
                FloorPlan = (BitmapDrawable)Drawable.CreateFromStream(Android.App.Application.Context.Assets.Open(floorTable.ImagePath), null),
                ImagePath = floorTable.ImagePath,
                Id = floorTable.Id
            };
            return modelFloor;
        }

    }
}