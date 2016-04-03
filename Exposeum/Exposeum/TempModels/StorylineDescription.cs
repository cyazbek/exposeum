namespace Exposeum.TempModels
{
    public class StorylineDescription
    {
        public int StoryLineDescriptionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IntendedAudience { get; set; }
        public Models.Language Language { get; set; }
        public bool Equals(StorylineDescription other)
        {
            if ((object)other == null)
            {
                return false;
            }
            return StoryLineDescriptionId == other.StoryLineDescriptionId &&
                Description.Equals(other.Description) &&
                Title.Equals(other.Title) &&
                Language.Equals(other.Language);
        }
    }
}
//moved to models 