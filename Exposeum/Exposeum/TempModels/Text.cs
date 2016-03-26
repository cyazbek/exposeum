
namespace Exposeum.TempModels
{
    public class Text:ExhibitionContent
    {
        public string HtmlContent { get; set; }

        public override bool Equals(object obj)
        {
            if(obj!=null)
            {
                Text other = (Text)obj;
                return HtmlContent.Equals(other.HtmlContent);
            }
            return false; 
        }
    }
}