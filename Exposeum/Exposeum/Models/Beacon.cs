using Java.Util;
using EstimoteSdk;
using Exposeum.Data;

namespace Exposeum.Models
{
    public class Beacon
    {
        public int id { get; set; }

        public Beacon()
        { }
        public UUID uuid
        {
            get; set;
        } 
        public int major
        {
            get; set;
        } 
        public int minor
        {
            get; set;
        }
        public Beacon (UUID uuid, int major, int minor)
        {
            this.uuid = uuid;
            this.major = major;
            this.minor = minor; 
        }
        public bool compareBeacon(EstimoteSdk.Beacon beacon)
        {
            if (this.uuid.Equals(beacon.ProximityUUID) && this.minor == beacon.Minor && this.major == beacon.Major)
                return true;
            else
                return false; 
        }
        public void convertFromData(BeaconData data)
        {
            this.id = data.ID;
            this.uuid = UUID.FromString(data.uuid);
            this.major = data.major;
            this.minor = data.minor;  
        }
    }
}