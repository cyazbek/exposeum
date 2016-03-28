using System.Collections.Generic;

namespace Exposeum.Models
{
    public abstract class ExhibitionContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Language Language { get; set; }
        public int StorylineId { get; set; }

        //Method that format's the content in html syntax.
        public abstract string HtmlFormat();
        public override bool Equals(object obj)
        {
            ExhibitionContent other = (ExhibitionContent)obj;
            return Id == other.Id && Title.Equals(other.Title) && StorylineId.Equals(other.StorylineId);
        }
        public static bool ListEquals(List<ExhibitionContent> contents, List<ExhibitionContent> expected)
        {
            if (contents.Count != expected.Count)
                return false;

            bool areEquals = false;

            for (int i = 0; i < contents.Count; i++)
                areEquals = contents[i].Equals(expected[i]);

            return areEquals;
        }

    }
}