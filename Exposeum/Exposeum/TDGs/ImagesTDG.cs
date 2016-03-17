using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class ImagesTDG : TDG
    {
        private static ImagesTDG _instance;
        
        private ImagesTDG() { }

        public static ImagesTDG GetInstance()
        {
            if (_instance == null)
                _instance = new ImagesTDG();

            return _instance;
        }

        public void Add(Images item)
        {
            _db.Insert(item);
        }

        public void Update(Images item)
        {
            _db.Update(item);
        }

        public Images GetImages(int id)
        {
            return _db.Get<Images>(id);
        }
        public bool Equals(Images image1, Images image2)
        {
            if (image1.ID == image2.ID && image1.path == image2.path)
                return true;
            else return false; 
        }
    }
}