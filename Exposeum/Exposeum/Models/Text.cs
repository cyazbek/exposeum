namespace Exposeum.Models
{
    public class Text : ExhibitionContent
    {
        public string text { get;  set; }
        public override string HtmlFormat()
        {
            throw new System.NotImplementedException();
        }
    }
}