namespace Exposeum.Models
{
    public abstract class ExhibitionContent
    {
        private int ID { get; set; }
        private string title { get; set; }
        private Language language { get; set; }
        private int storylineID { get; set; }

        //Method that format's the content in html syntax.
        public abstract string htmlFormat();

    }
}