
using Java.Util;
using SQLite;
using System.Threading;

namespace Exposeum.Models
{
    public class POI
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }

        public Beacon beacon { get; set; }
        /*public UUID beaconId { get; set; }*/

        //public int QrCode { get; set; }

        public bool visited { get; set; }

        public string name_en { get; set; }

        public string name_fr { get; set; }

        public int uCoord { get; set; }

        public int vCoord { get; set; } 

        public string dscription_en { get; set; }

        public string dscription_fr { get; set; }

        public string getDescription()
        {
            string lang = Thread.CurrentThread.CurrentCulture.Name;
            string description;
            if (lang.Contains("fr"))
            {
                description = this.dscription_fr;
            }
            else description = this.dscription_en; 

            return description; 
        }
        public string getName()
        {
            string lang = Thread.CurrentThread.CurrentCulture.Name;
            string PoiName;
            if (lang.Contains("fr"))
            {
                PoiName = this.name_fr;
            }
            else PoiName = this.name_en;
            return PoiName;
        }

    }
}