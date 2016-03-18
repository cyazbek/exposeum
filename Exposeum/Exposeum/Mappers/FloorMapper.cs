using Exposeum.TDGs;
using Floor = Exposeum.TempModels.Floor;

namespace Exposeum.Mappers
{
    public class FloorMapper
    {
        private static FloorMapper _instance;
        private readonly FloorTDG _floorTdg;
        private readonly ImageMapper _imagesMapper;

        private FloorMapper()
        {
            _floorTdg = FloorTDG.GetInstance();
            _imagesMapper = ImageMapper.GetInstance();
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
            string image = _imagesMapper.GetImagePath(floorTable.ID);

            Floor floor = new Floor
            {
                _plan = image,
                _id = floorTable.ID
            };

            return floor;
        }

        public Tables.Floor FloorModelToTable(Floor floor)
        {
            int id = floor._id;
            int image = _floorTdg.GetFloor(floor._id).imageId;

            Tables.Floor floorTable = new Tables.Floor
            {
                ID = id,
                imageId = image
            };

            return floorTable;
        }
    }
}