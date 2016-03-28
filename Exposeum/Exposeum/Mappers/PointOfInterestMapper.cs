using System.Collections.Generic;
using Android.Hardware;
using Exposeum.Mappers;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.TempModels;
using Java.Lang.Annotation;

namespace Exposeum.Mappers
{
    public class PointOfInterestMapper
    {
        private static PointOfInterestMapper _instance;
        private BeaconMapper _beaconMapper;
        private PointOfInterestDescriptionMapper _descriptionMapper; 

        private PointOfInterestMapper()
        {
            _beaconMapper = BeaconMapper.GetInstance();
            _descriptionMapper = PointOfInterestDescriptionMapper.GetInstance();
        }

        public static PointOfInterestMapper GetInstance()
        {
            if(_instance==null)
                _instance = new PointOfInterestMapper();
            return _instance;
        }

        public Tables.MapElements ConvertFromModel(PointOfInterest poi)
        {
            int vis = 0;
            if (poi.Visited == true)
            {
                vis = 1;
            }
            else vis = 0; 

            return new MapElements()
            {
                Id = poi.Id,
                BeaconId = poi.Beacon.Id,
                Discriminator = "PointOfInterest",
                FloorId = poi.Floor.Id,
                IconId = poi.IconId,
                PoiDescription = poi.Description.Id,
                StoryLineId = poi.StoryLineId,
                UCoordinate = poi.UCoordinate,
                VCoordinate = poi.VCoordinate,
                Visited = vis
            };
        }

        public PointOfInterest ConvertFromTable(Tables.MapElements element)
        {
            bool vis = false;
            if (element.Visited == 1)
                vis = true;
            else vis = false;
            List<ExhibitionContent> exhibitionContent = new List<ExhibitionContent>();


            return new PointOfInterest
            {
                Id = element.Id,
                Beacon = _beaconMapper.GetBeacon(element.BeaconId),
                Description = _descriptionMapper.GetPointOfInterestDescription(element.PoiDescription)
            };

        }
    }
}