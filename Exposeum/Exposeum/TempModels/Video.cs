
namespace Exposeum.TempModels
{
    public class Video:ExhibitionContent
    {
        public string _filePath { get; set; }
        public int _duration { get; set; }
        public int _resolution { get; set; }
        public string _encoding { get; set; }

        public override bool Equals(object obj)
        {
            Video other = (Video)obj;
            return _filePath.Equals(other._filePath) && _duration == other._duration && _resolution == other._resolution && _encoding.Equals(other._encoding);
        }
    }
}