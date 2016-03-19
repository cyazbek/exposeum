namespace Exposeum.TempModels
{
    public abstract class ExhibitionContent
    {
        public int _id { get; set; }
        public string _title { get; set; }
        public int _storylineId { get; set; }

        public override bool Equals(object obj)
        {
            ExhibitionContent other = (ExhibitionContent)obj;
            return _id == other._id && _title.Equals(other._title) && _storylineId.Equals(other._storylineId);
        }
    }
}