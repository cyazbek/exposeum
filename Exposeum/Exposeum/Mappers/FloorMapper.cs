using Exposeum.TDGs;
using Floor = Exposeum.TempModels.Floor;

namespace Exposeum.Mappers
{
    public class FloorMapper
    {
        private static FloorMapper _instance;
        private readonly FloorTDG _floorTdg;

        private FloorMapper()
        {
            _floorTdg = FloorTDG.GetInstance();
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
                ID = floor._id,
                // imageId = ImageMapper.GetInstance().GetIdFromPath(floor._plan);
            };

            return floorTable;
        }

        public Floor FloorTableToModel(Tables.Floor floorTable)
        {
            string image = ImageMapper.GetInstance().GetImagePath(floorTable.ID);
            Floor modelFloor = new Floor
            {
                _plan = image,
                _id = floorTable.ID
            };
            return modelFloor;
        }

        public bool Equals(Floor floor1, Floor floor2)
        {
            return (floor1._id == floor2._id && floor1._plan == floor2._plan);
        }

    }
}