
namespace Exposeum.TempModels
{
    public class AudioContent:ExhibitionContent
    {
        public string FilePath { get; set; }
        public int Duration { get; set; }
        public string Encoding { get; set; }

        public override bool Equals(object obj)
        {
            AudioContent other = (AudioContent)obj;
            return FilePath.Equals(other.FilePath) && Duration == other.Duration && Encoding.Equals(other.Encoding);
        }

    }
}
//moved to Models
