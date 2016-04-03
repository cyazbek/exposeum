
namespace Exposeum.TempModels
{
    public class VideoContent:ExhibitionContent
    {
        public string FilePath { get; set; }
        public int Duration { get; set; }
        public int Resolution { get; set; }
        public string Encoding { get; set; }

        public override bool Equals(object obj)
        {
            VideoContent other = (VideoContent)obj;
            return FilePath.Equals(other.FilePath) && Duration == other.Duration && Resolution == other.Resolution && Encoding.Equals(other.Encoding);
        }
    }
}
//moved to models