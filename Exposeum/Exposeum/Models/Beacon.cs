using Java.Util;
using Exposeum.Data;

namespace Exposeum.Models
{
    public class Beacon
    {
        public int Id { get; set; }

        public Beacon()
        { }
        public UUID Uuid
        {
            get; set;
        } 
        public int Major
        {
            get; set;
        } 
        public int Minor
        {
            get; set;
        }
        public Beacon (UUID uuid, int major, int minor)
        {
            this.Uuid = uuid;
            this.Major = major;
            this.Minor = minor; 
        }
        public bool CompareBeacon(EstimoteSdk.Beacon beacon)
        {
            if (this.Uuid.Equals(beacon.ProximityUUID) && this.Minor == beacon.Minor && this.Major == beacon.Major)
                return true;
            else
                return false; 
        }
        public void ConvertFromData(BeaconData data)
        {
            this.Id = data.Id;
            this.Uuid = UUID.FromString(data.Uuid);
            this.Major = data.Major;
            this.Minor = data.Minor;  
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                Beacon other = (Beacon)obj;
                return other.Major == Major && other.Minor == Minor;
            }
            return false;
        }
    }
}