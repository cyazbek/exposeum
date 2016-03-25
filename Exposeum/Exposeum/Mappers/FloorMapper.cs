using Exposeum.TDGs;
using System.Collections.Generic;
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

        public Tables.Floor FloorModelToTable(Floor floor)
        {
            Tables.Floor floorTable = new Tables.Floor
            {
                Id = floor.Id,
                // imageId = ImageMapper.GetInstance().GetIdFromPath(floor._plan);
            };

            return floorTable;
        }

        public List<Floor> GetAllFloors()
        {
            List<Tables.Floor> tableFloor = _floorTdg.GetAllFloors();
            List<Floor> modelFloor = new List<Floor>(); 
            foreach(var x in tableFloor)
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
        public Floor FloorTableToModel(Tables.Floor floorTable)
        {
            string image = ImageMapper.GetInstance().GetImagePath(floorTable.Id);
            Floor modelFloor = new Floor
            {
                Plan = image,
                Id = floorTable.Id
            };
            return modelFloor;
        }

    }
}