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
            return String.Format("<div><video width=\"320\" height=\"240\" controls>" +
                                 "<source src = \"N3E.mp4\" type = \"video/mp4\" >" +
                                 "</ video ></div>");
        }
    }
}