
namespace Exposeum.TempModels
{
    public class Text:ExhibitionContent
    {
        public string _htmlContent { get; set; }

        public override bool Equals(object obj)
        {
            if(obj!=null)
            {
                Text other = (Text)obj;
                return _htmlContent.Equals(other._htmlContent);
            }
            return false; 
        }
    }
}