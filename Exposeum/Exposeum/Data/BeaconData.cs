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
            this.Uuid = passedBeacon.Uuid.ToString();
            this.Major = passedBeacon.Major;
            this.Minor = passedBeacon.Minor;
            return this.Id; 
        }
    }
}