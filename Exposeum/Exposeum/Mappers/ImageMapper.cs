using Exposeum.Tables;
using Exposeum.TDGs;

namespace Exposeum.Mappers
{
    public class ImageMapper
    {
        private static ImageMapper _instance;
        private readonly ImagesTDG _imageTdg;

        private ImageMapper()
        {
            _imageTdg = ImagesTDG.GetInstance();
        }

        public static ImageMapper GetInstance()
        {
            if (_instance == null)
                _instance = new ImageMapper();

            return _instance;
        }

        public void AddImage(string imagePath, int id)
        {
            Images image = ImagesModelToTable(imagePath, id);
            _imageTdg.Add(image);
        }

        public void UpdateImagePath(string imagePath, int id)
        {
            Images image = ImagesModelToTable(imagePath, id);
            _imageTdg.Update(image);
        }

        public string GetImagePath(int imageId)
        {
            string imagePath = _imageTdg.GetImages(imageId).path;
            return imagePath;
        }

        private static Images ImagesModelToTable(string imagePath, int id)
        {
            Images image = new Images
            {
                ID = id,
                path = imagePath
            };
            return image;
        }

    }
}