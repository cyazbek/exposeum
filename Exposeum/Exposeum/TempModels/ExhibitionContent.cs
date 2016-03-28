using System.Collections.Generic;

namespace Exposeum.TempModels
{
    public abstract class ExhibitionContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int StorylineId { get; set; }
        public int PoiId { get; set; }

        public Models.Language Language {get; set;}

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