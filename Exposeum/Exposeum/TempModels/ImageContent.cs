
namespace Exposeum.TempModels
{
    public class ImageContent:ExhibitionContent
    {
        public string FilePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public override bool Equals(object obj)
        {
            ImageContent other = (ImageContent)obj;
            return FilePath.Equals(other.FilePath) && Width == other.Width && Height == other.Height;
        }
    }
}
//transferred to Models