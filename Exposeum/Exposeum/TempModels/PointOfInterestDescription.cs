

namespace Exposeum.TempModels
{
    public class PointOfInterestDescription
    {
        public int _id { get; set; }
        public string _title { get; set; }
        public string _summary { get; set; }
        public string _description { get; set; }
        public Models.Language _language { get; set; }
        public override bool Equals(object obj)
        {
            if(obj!=null)
            {
                PointOfInterestDescription other = (PointOfInterestDescription)obj;
                return other._id == _id && other._description.Equals(_description) && other._language.Equals(_language) && other._summary.Equals(_summary); 
            }
            return false; 
        }
    }
}