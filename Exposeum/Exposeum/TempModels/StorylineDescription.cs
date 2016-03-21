namespace Exposeum.TempModels
{
    public class StorylineDescription
    {
        public int _storyLineDescriptionId { get; set; }
        public string _title { get; set; }
        public string _description { get; set; }
        public Models.Language _language { get; set; }
        public bool Equals(StorylineDescription other)
        {
            if ((object)other == null)
            {
                return false;
            }
            return _storyLineDescriptionId == other._storyLineDescriptionId &&
                _description.Equals(other._description) &&
                _title.Equals(other._title) &&
                _language.Equals(other._language);
        }
    }
}