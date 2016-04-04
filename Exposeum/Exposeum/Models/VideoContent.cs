using System;

namespace Exposeum.Models
{
    public class VideoContent : ExhibitionContent
    {
        public string FilePath { get; set; }
        public int Duration { get; set; }
        public int Resolution { get; set; }
        public string Encoding { get; set; }

        public bool Equals(VideoContent other)
        {
            return FilePath.Equals(other.FilePath) && Duration == other.Duration && Resolution == other.Resolution && Encoding.Equals(other.Encoding);
        }

        public override string HtmlFormat()
        {
            return "";
        }
    }
}