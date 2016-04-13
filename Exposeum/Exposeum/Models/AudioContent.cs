using System;

namespace Exposeum.Models
{
    public class AudioContent : ExhibitionContent
    {
        public string FilePath { get; set; }
        public int Duration { get; set; }
        public string Encoding { get; set; }
        public override bool Equals(object obj)
        {
            AudioContent other = (AudioContent)obj;
            return FilePath.Equals(other.FilePath) && Duration == other.Duration && Encoding.Equals(other.Encoding) && PoiId == other.PoiId;
        }
        public override string HtmlFormat()
        {
            return String.Format("<div><audio controls autoplay> "+ 
                "<source src = \"{0}\" type = \"audio/mpeg\" >" +
                                 "</audio></div>", FilePath);
        }
    }
}