using Exposeum.TDGs;
using Exposeum.Models;
using System.Collections.Generic;

namespace Exposeum.Mappers
{
    public class PointOfInterestMapper
    {
        private static PointOfInterestMapper _instance;
        private readonly PointOfInterestDescriptionMapper _descriptionMapper;
        private readonly ExhibitionContentMapper _exhibitionMapper;
        private readonly BeaconMapper _beaconMapper;
        private readonly MapElementsTdg _mapElementsTdg;
        private readonly FloorMapper _floorMapper;
		private Dictionary<int, PointOfInterest> _pointOfInterestIdentityMap;

        private PointOfInterestMapper()
        {
            _descriptionMapper = PointOfInterestDescriptionMapper.GetInstance();
            _exhibitionMapper = ExhibitionContentMapper.GetInstance();
            _beaconMapper = BeaconMapper.GetInstance();
            _mapElementsTdg = MapElementsTdg.GetInstance();
            _floorMapper = FloorMapper.GetInstance();
			_pointOfInterestIdentityMap = new Dictionary<int, PointOfInterest>();

        }

        public static PointOfInterestMapper GetInstance()
        {
            if(_instance==null)
                _instance = new PointOfInterestMapper();
            return _instance;
        }

        public Tables.MapElements PoiModelToTable(PointOfInterest poi)
        {
            int vis;
            if (poi.Visited)
                vis = 1;
            else
                vis = 0;
            return new Tables.MapElements()
            {
                BeaconId = poi.Beacon.Id,
                Discriminator = "PointOfInterest",
                FloorId = poi.Floor.Id,
                Id = poi.Id,
                IconPath = poi.IconPath,
                PoiDescription = poi.Description.Id,
                StoryLineId = poi.StoryId,
                UCoordinate = poi.UCoordinate,
                VCoordinate = poi.VCoordinate,
                Visited = vis,
            };
        }

        public PointOfInterest PoiTableToModel(Tables.MapElements mapElement)
        {
			PointOfInterest pointOfInterest;

			//check if the POI is in the identity map
			if(_pointOfInterestIdentityMap.TryGetValue(mapElement.Id, out pointOfInterest)){
				return pointOfInterest;
			}

            Floor floor = _floorMapper.GetFloor(mapElement.FloorId);
            PointOfInterestDescription description =
                _descriptionMapper.GetPointOfInterestDescription(mapElement.PoiDescription);
            Beacon beacon = _beaconMapper.GetBeacon(mapElement.BeaconId);
            List<ExhibitionContent> content = _exhibitionMapper.GetExhibitionByPoiId(mapElement.Id);

            bool vis;
            if (mapElement.Visited == 0)
                vis = false;
            else
                vis = true;

			pointOfInterest = new PointOfInterest()
            {
                Beacon = beacon,
                Description = description,
                ExhibitionContent = content,
                Floor = floor,
                Id = mapElement.Id,
                IconPath = mapElement.IconPath,
                StoryId = mapElement.StoryLineId,
                UCoordinate = mapElement.UCoordinate,
                VCoordinate = mapElement.VCoordinate,
                Visited = vis
            };

			//add the POI to the identity map
			_pointOfInterestIdentityMap.Add (pointOfInterest.Id, pointOfInterest);

			return pointOfInterest;
        }
        public void Add(PointOfInterest poi)
        {
            var mapElement = PoiModelToTable(poi);
            _mapElementsTdg.Add(mapElement);
            _floorMapper.AddFloor(poi.Floor);
            _beaconMapper.AddBeacon(poi.Beacon);
            _descriptionMapper.AddPointOfInterestDescription(poi.Description);
            _exhibitionMapper.AddExhibitionContent(poi.ExhibitionContent);
        }

        public void Update(PointOfInterest poi)
        {
            Tables.MapElements mapElements = PoiModelToTable(poi);
            _mapElementsTdg.Update(mapElements);
            _beaconMapper.UpdateBeacon(poi.Beacon);
            _descriptionMapper.UpdatePointOfInterestDescription(poi.Description);
            _exhibitionMapper.UpdateExhibitionList(poi.ExhibitionContent);
            _floorMapper.UpdateFloor(poi.Floor);
        }

        public PointOfInterest Get(int id)
        {
            Tables.MapElements mapElement = _mapElementsTdg.GetMapElement(id);
            return PoiTableToModel(mapElement);
        }
    }
}