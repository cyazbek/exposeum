
namespace Exposeum.TempModels
{
    public class Image:ExhibitionContent
    {
        public string FilePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public override bool Equals(object obj)
        {
            Image other = (Image)obj;
            return FilePath.Equals(other.FilePath) && Width == other.Width && Height == other.Height;
        }
    }
}