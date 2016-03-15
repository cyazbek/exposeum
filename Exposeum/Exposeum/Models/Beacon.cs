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
            Uuid = uuid;
            Major = major;
            Minor = minor; 
        }
        public bool CompareBeacon(EstimoteSdk.Beacon beacon)
        {
            if (Uuid.Equals(beacon.ProximityUUID) && Minor == beacon.Minor && Major == beacon.Major)
                return true;
            else
                return false; 
        }
        public void ConvertFromData(BeaconData data)
        {
            Id = data.Id;
            Uuid = UUID.FromString(data.Uuid);
            Major = data.Major;
            Minor = data.Minor;  
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