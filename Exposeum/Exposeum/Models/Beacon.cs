using Java.Util;
using EstimoteSdk;

namespace Exposeum.Models
{
    public class Beacon
    {
        private UUID uuid
        {
            get; set;
        } 
        private int major
        {
            get; set;
        } 
        private int minor
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
    }
}