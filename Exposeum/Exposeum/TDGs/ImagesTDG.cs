using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public class ImagesTdg : Tdg
    {
        private static ImagesTdg _instance;
        
        private ImagesTdg() { }

        public static ImagesTdg GetInstance()
        {
            if (_instance == null)
                _instance = new ImagesTdg();

            return _instance;
        }

        public void Add(Images item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(Images item)
        {
            Db.Update(item);
        }

        public Images GetImages(int id)
        {
            return Db.Get<Images>(id);
        }
        public bool Equals(Images image1, Images image2)
        {
            if (image1.Id == image2.Id && image1.Path == image2.Path)
                return true;
            else return false; 
        }
    }
}