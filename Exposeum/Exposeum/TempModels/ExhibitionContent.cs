namespace Exposeum.TempModels
{
    public abstract class ExhibitionContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int StorylineId { get; set; }

        public Models.Language Language {get; set;}

        public override bool Equals(object obj)
        {
            ExhibitionContent other = (ExhibitionContent)obj;
            return Id == other.Id && Title.Equals(other.Title) && StorylineId.Equals(other.StorylineId);
        }
    }
}
//transferred to Models