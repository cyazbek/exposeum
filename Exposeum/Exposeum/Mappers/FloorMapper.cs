using Exposeum.TDGs;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using Floor = Exposeum.Models.Floor;
using System.IO;

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
                ImagePath = floor.ImagePath,
                Height = floor.Height,
                Width = floor.Width
            };

            return floorTable;
        }

        public Floor FloorTableToModel(Tables.Floor floorTable)
        {
			var floorImageStream = System.IO.File.OpenRead (floorTable.ImagePath);
			Drawable floorImageDrawable = (BitmapDrawable)Drawable.CreateFromStream (floorImageStream, null);

			Floor modelFloor = new Floor(floorImageDrawable)
            {
                ImagePath = floorTable.ImagePath,
                Id = floorTable.Id,
                Height = floorTable.Height,
                Width = floorTable.Width,
				Image = new ColorDrawable() //floorImageDrawable
            };
            return modelFloor;
        }

    }
}