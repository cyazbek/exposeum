
using SQLite;

namespace Exposeum.Models
{
    public class POI
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }

        public int beaconId { get; set; }

        public int QrCodeId { get; set; }

        public bool visited { get; set; }

        public string name_en { get; set; }

        public string name_fr { get; set; }

        public int uCoord { get; set; }

        public int vCoord { get; set; } 

        public string dscription_en { get; set; }

        public string dscription_fr { get; set; }
    }
}