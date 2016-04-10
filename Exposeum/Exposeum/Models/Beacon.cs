using Java.Util;

namespace Exposeum.Models
{
    public class Beacon
    {
        public int Id { get; set; }
        public UUID Uuid { get; set; }
        public int Minor { get; set; }
        public int Major { get; set; }

        public Beacon()
        { }

        public Beacon(EstimoteSdk.Beacon beacon)
        {
            Uuid = beacon.ProximityUUID;
            Major = beacon.Major;
            Minor = beacon.Minor;
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
            return false;
        }

		public bool CompareBeacon(Beacon beacon)
		{
			if (Uuid.Equals(beacon.Uuid) && Minor == beacon.Minor && Major == beacon.Major)
				return true;
			return false;
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