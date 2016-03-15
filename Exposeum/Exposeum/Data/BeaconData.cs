using SQLite;

namespace Exposeum.Data
{
    public class BeaconData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Uuid
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
        public int ConvertBeaconToData(Models.Beacon passedBeacon)
        {
            Uuid = passedBeacon.Uuid.ToString();
            Major = passedBeacon.Major;
            Minor = passedBeacon.Minor;
            return Id; 
        }
    }
}