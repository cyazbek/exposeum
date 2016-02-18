using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Exposeum.Data
{
    public class POIData
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

        public float uCoord { get; set; }

        public float vCoord { get; set; }

        public string dscription_en { get; set; }

        public string dscription_fr { get; set; }

        public int convertPOIToData(Models.PointOfInterest poi, int beaconId)
        {
            this.storyId = poi.storyID;
            this.beaconID = beaconId;
            this.visited = poi.visited;
            this.name_en = poi.name_en;
            this.name_fr = poi.name_fr;
            this.dscription_en = poi.description_en;
            this.dscription_fr = poi.description_fr;
            this.uCoord = poi._u;
            this.vCoord = poi._v; 
            return this.ID; 
        }
    }
}