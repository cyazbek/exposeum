using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Exposeum.Data
{
    class POIData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(BeaconData))]
        public int beaconID { get; set; }
        [ForeignKey(typeof(StoryData))]
        public int storyId { get; set; }
        public bool visited { get; set; }

        public string name_en { get; set; }

        public string name_fr { get; set; }

        public int uCoord { get; set; }

        public int vCoord { get; set; }

        public string dscription_en { get; set; }

        public string dscription_fr { get; set; }

        public int convertPOIToData(Models.POI poi, int beaconId)
        {
            this.beaconID = beaconId;
            this.visited = poi.visited;
            this.name_en = poi.name_en;
            this.name_fr = poi.name_fr;
            this.dscription_en = poi.dscription_en;
            this.dscription_fr = poi.dscription_fr;
            this.uCoord = poi.uCoord;
            this.vCoord = poi.vCoord; 
            return this.ID; 
        }
    }
}