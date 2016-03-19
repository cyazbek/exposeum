
namespace Exposeum.TempModels
{
    public class Image:ExhibitionContent
    {
        public string _filePath { get; set; }
        public int _width { get; set; }
        public int _height { get; set; }

        public override bool Equals(object obj)
        {
            Image other = (Image)obj;
            return _filePath.Equals(other._filePath) && _width == other._width && _height == other._height;
        }
    }
}