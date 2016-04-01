
namespace Exposeum.TempModels
{
    public class TextContent:ExhibitionContent
    {
        public string HtmlContent { get; set; }

        public override bool Equals(object obj)
        {
            if(obj!=null)
            {
                TextContent other = (TextContent)obj;
                return HtmlContent.Equals(other.HtmlContent);
            }
            return false; 
        }
    }
}