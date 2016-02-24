namespace Exposeum.Models
{
    public abstract class ExhibitionContent
    {
        public int ID { get; set; }
        public string title { get; set; }
        public Language language { get; set; }
        public int storylineID { get; set; }

        // Method to format the content in HTML format.
        public abstract string HtmlFormat();
    }

}