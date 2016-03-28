namespace Exposeum.Models
{
    public abstract class ExhibitionContent
    {
        private int Id { get; set; }
        private string Title { get; set; }
        private Language Language { get; set; }
        private int StorylineId { get; set; }

        //Method that format's the content in html syntax.
        public abstract string HtmlFormat();
        public override bool Equals(object obj)
        {
            ExhibitionContent other = (ExhibitionContent)obj;
            return Id == other.Id && Title.Equals(other.Title) && StorylineId.Equals(other.StorylineId);
        }

    }
}