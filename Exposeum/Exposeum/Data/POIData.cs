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
            this.StoryId = poi.StoryId;
            this.BeaconId = beaconId;
            this.Visited = poi.Visited;
            this.NameEn = poi.NameEn;
            this.NameFr = poi.NameFr;
            this.DscriptionEn = poi.DescriptionEn;
            this.DscriptionFr = poi.DescriptionFr;
            this.UCoord = poi.U;
            this.VCoord = poi.V; 
            return this.Id; 
        }
    }
}