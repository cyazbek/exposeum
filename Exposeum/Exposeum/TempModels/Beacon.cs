using Java.Util;

namespace Exposeum.TempModels
{
    public class Beacon
    {
        public int Id { get; set; }
        public UUID Uuid { get; set; }
        public int Minor { get; set; }
        public int Major { get; set; }

        public bool CompareBeacon(EstimoteSdk.Beacon beacon)
        {
            if (Uuid.Equals(beacon.ProximityUUID) && Minor == beacon.Minor && Major == beacon.Major)
                return true;
            else
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