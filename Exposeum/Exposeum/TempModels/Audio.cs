
namespace Exposeum.TempModels
{
    public class Audio:ExhibitionContent
    {
        public string FilePath { get; set; }
        public int Duration { get; set; }
        public string Encoding { get; set; }

        public override bool Equals(object obj)
        {
            Audio other = (Audio)obj;
            return FilePath.Equals(other.FilePath) && Duration == other.Duration && Encoding.Equals(other.Encoding);
        }

    }
}