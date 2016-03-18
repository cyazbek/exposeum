using Java.Util;

namespace Exposeum.TempModels
{
    public class Beacon
    {
        public int _id { get; set; }
        public UUID _uuid { get; set; }
        public int _minor { get; set; }
        public int _major { get; set; }

        public bool CompareBeacon(EstimoteSdk.Beacon beacon)
        {
            if (_uuid.Equals(beacon.ProximityUUID) && _minor == beacon.Minor && _major == beacon.Major)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                Beacon other = (Beacon)obj;
                return other._major == _major && other._minor == _minor;
            }
            return false;
        }
    }
}