namespace Exposeum.TempModels
{
    public class StorylineDescription
    {
        public int _storyLineDescriptionId { get; set; }
        public string _title { get; set; }
        public string _description { get; set; }
        public Models.Language _language { get; set; }
        public override bool Equals(object obj)
        {
            if(obj!=null)
            {
                StorylineDescription other = (StorylineDescription)obj;
                return _storyLineDescriptionId == other._storyLineDescriptionId && _title.Equals(other._title) && _description.Equals(other._description)
                    && _language.Equals(other._language);
            }
            return false; 
        }
    }
}