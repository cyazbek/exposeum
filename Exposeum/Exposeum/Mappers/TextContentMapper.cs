using Exposeum.TDGs;
using Exposeum.TempModels;

namespace Exposeum.Mappers
{
    public class TextContentMapper
    {
        private static TextContentMapper _instance;
        private readonly ExhibitionContentEnTdg _tdgEn;
        private readonly ExhibitionContentFrTdg _tdgFr;

        private TextContentMapper()
        {
            _tdgEn = ExhibitionContentEnTdg.GetInstance();
            _tdgFr = ExhibitionContentFrTdg.GetInstance();
        }

        public static TextContentMapper GetInstance()
        {
            if (_instance == null)
                _instance = new TextContentMapper();
            return _instance;
        }

        public TextContent ConvertFromTable(Tables.ExhibitionContentEn content)
        {
            return new TextContent
            {
                Id = content.Id,
                Title = content.Title,
                Language = Models.Language.En,
                StorylineId = content.StoryLineId,
                HtmlContent = content.Description
                
            };
        }

        public TextContent ConvertFromTable(Tables.ExhibitionContentFr content)
        {
            return new TextContent
            {
                Id = content.Id,
                Title = content.Title,
                Language = Models.Language.Fr,
                StorylineId = content.StoryLineId,
                HtmlContent = content.Description
            };
        }

        public Tables.ExhibitionContentEn ConvertFromModelEn(TextContent content)
        {

            return new Tables.ExhibitionContentEn
            {
                Id = content.Id,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "TextContent",
                Description = content.HtmlContent
            };
        }

        public Tables.ExhibitionContentFr ConvertFromModelFr(TextContent content)
        {

            return new Tables.ExhibitionContentFr
            {
                Id = content.Id,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "TextContent",
                Description = content.HtmlContent
            };
        }

        public void Add(TextContent content)
        {
            if (content.Language == Models.Language.Fr)
            {
                _tdgFr.Add(ConvertFromModelFr(content));
            }
            else
                _tdgEn.Add(ConvertFromModelEn(content));
        }

        public TextContent Get(int id)
        {
            if (Models.User.GetInstance().Language == Models.Language.Fr)
            {
                return ConvertFromTable(_tdgFr.GetExhibitionContentFr(id));
            }
            else
                return ConvertFromTable(_tdgEn.GetExhibitionContentEn(id));
        }

        public void Update(TextContent content)
        {
            if (content.Language == Models.Language.Fr)
                _tdgFr.Update(ConvertFromModelFr(content));
            else
                _tdgEn.Update(ConvertFromModelEn(content));
        }

    }
}