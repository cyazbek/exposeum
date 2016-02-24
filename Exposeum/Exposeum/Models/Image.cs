using System.IO;

namespace Exposeum.Models
{
    public class Image : ExhibitionContent
    {
        public string filePath { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public override string HtmlFormat()
        {
            throw new System.NotImplementedException();
        }
    }
}