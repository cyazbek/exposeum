
namespace Exposeum.TempModels
{
    public class Audio:ExhibitionContent
    {
        public string _filePath { get; set; }
        public int _duration { get; set; }
        public string _encoding { get; set; }

        public override bool Equals(object obj)
        {
            Audio other = (Audio)obj;
            return _filePath.Equals(other._filePath) && _duration == other._duration && _encoding.Equals(other._encoding);
        }

    }
}