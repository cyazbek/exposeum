using Java.Util;

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
        public bool compareBeacon(Beacon beacon)
        {
            if (this.uuid == beacon.uuid && this.minor == beacon.minor && this.major == beacon.major)
                return true;
            else
                return false; 
        }
    }
}