namespace Exposeum.Models
{
    public class TextContent : ExhibitionContent
    {
        public string text { get; set; }

        public override string htmlFormat()
        {
            throw new System.NotImplementedException();
        }
    }
}