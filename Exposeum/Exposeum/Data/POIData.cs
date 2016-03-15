using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Exposeum.Data
{
    public class PoiData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(BeaconData))]
        public int BeaconId { get; set; }
        [ForeignKey(typeof(StoryData))]
        public int StoryId { get; set; }
        public bool Visited { get; set; }

        public string NameEn { get; set; }

        public string NameFr { get; set; }

        public float UCoord { get; set; }

        public float VCoord { get; set; }

        public string DscriptionEn { get; set; }

        public string DscriptionFr { get; set; }

        public int ConvertPoiToData(Models.PointOfInterest poi, int beaconId)
        {
            StoryId = poi.StoryId;
            BeaconId = beaconId;
            Visited = poi.Visited;
            NameEn = poi.NameEn;
            NameFr = poi.NameFr;
            DscriptionEn = poi.DescriptionEn;
            DscriptionFr = poi.DescriptionFr;
            UCoord = poi.U;
            VCoord = poi.V; 
            return Id; 
        }
    }
}