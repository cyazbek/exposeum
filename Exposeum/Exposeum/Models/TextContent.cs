using System;

namespace Exposeum.Models
{
    public class TextContent : ExhibitionContent
    {
        public string HtmlContent { get; set; }

        public override string HtmlFormat()
        {
            return "";
        }
    }
}