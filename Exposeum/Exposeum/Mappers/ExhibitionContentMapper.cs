using Exposeum.TDGs;
using Exposeum.Models;

namespace Exposeum.Mappers
{
   
    public class ExhibitionContentMapper
    {
        public static ExhibitionContentMapper Instance;
        private readonly ExhibitionContentEnTdg _exhibitionContentEnTdg;
        private readonly ExhibitionContentFrTdg _exhibitionContentFrTdg;
        private ExhibitionContentTdg _exhibitionContentTdg; 

        private ExhibitionContentMapper()
        {
            _exhibitionContentEnTdg = ExhibitionContentEnTdg.GetInstance();
            _exhibitionContentFrTdg = ExhibitionContentFrTdg.GetInstance();
            if (User.GetInstance().Language == Language.Fr)
                _exhibitionContentTdg = _exhibitionContentFrTdg;
            else _exhibitionContentTdg = _exhibitionContentEnTdg;
        }

        public static ExhibitionContentMapper GetInstance()
        {
            if (Instance == null)
                Instance = new ExhibitionContentMapper();
            return Instance;
        }



    }


}