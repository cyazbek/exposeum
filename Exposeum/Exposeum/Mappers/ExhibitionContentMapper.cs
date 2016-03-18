using Exposeum.TDGs;
using Exposeum.Models;

namespace Exposeum.Mappers
{
   
    public class ExhibitionContentMapper
    {
        public static ExhibitionContentMapper _instance;
        private ExhibitionContentEnTDG exhibitionContentEnTDG;
        private ExhibitionContentFrTDG exhibitionContentFrTDG;

        private ExhibitionContentMapper()
        {
            exhibitionContentEnTDG = ExhibitionContentEnTDG.GetInstance();
            exhibitionContentFrTDG = ExhibitionContentFrTDG.GetInstance();
        }

        public static ExhibitionContentMapper GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentMapper();
            return _instance;
        }



    }


}