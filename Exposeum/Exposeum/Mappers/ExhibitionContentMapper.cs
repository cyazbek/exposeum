using Exposeum.TDGs;
using Exposeum.Models;

namespace Exposeum.Mappers
{
   
    public class ExhibitionContentMapper
    {
        public static ExhibitionContentMapper _instance;
        private readonly ExhibitionContentEnTDG exhibitionContentEnTDG;
        private readonly ExhibitionContentFrTDG exhibitionContentFrTDG;
        private ExhibitionContentTDG exhibitionContentTDG; 

        private ExhibitionContentMapper()
        {
            exhibitionContentEnTDG = ExhibitionContentEnTDG.GetInstance();
            exhibitionContentFrTDG = ExhibitionContentFrTDG.GetInstance();
            if (User.GetInstance()._language == Language.Fr)
                exhibitionContentTDG = exhibitionContentFrTDG;
            else exhibitionContentTDG = exhibitionContentEnTDG;
        }

        public static ExhibitionContentMapper GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentMapper();
            return _instance;
        }



    }


}