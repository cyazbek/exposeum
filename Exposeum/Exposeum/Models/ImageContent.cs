namespace Exposeum.Models
{
    public class ImageContent : ExhibitionContent
    {
        public string filePath { get; set; }
        public int width { get; set; }
        public int heigh { get; set; }

        public override string htmlFormat()
        {
            return "<img>sldks</img>";
        }
    }
}