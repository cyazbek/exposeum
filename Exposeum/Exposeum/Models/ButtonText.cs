namespace Exposeum.Models
{
    public class ButtonText
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public ButtonText()
        {

        }
        public ButtonText(string id, string text)
        {
            Id = id;
            Text = text; 
        }
    }
}