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

        public TextContent TextTableToModelEn(Tables.ExhibitionContentEn content)
        {
            return new TextContent
            {
                Id = content.Id,
                PoiId = content.PoiId,
                Title = content.Title,
                Language = Models.Language.En,
                StorylineId = content.StoryLineId,
                HtmlContent = content.Description
                
            };
        }

        public TextContent TextTableToModelFr(Tables.ExhibitionContentFr content)
        {
            return new TextContent
            {
                Id = content.Id,
                PoiId = content.PoiId,
                Title = content.Title,
                Language = Models.Language.Fr,
                StorylineId = content.StoryLineId,
                HtmlContent = content.Description
            };
        }

        public Tables.ExhibitionContentEn TextModelToTableEn(TextContent content)
        {

            return new Tables.ExhibitionContentEn
            {
                Id = content.Id,
                PoiId = content.PoiId,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "TextContent",
                Description = content.HtmlContent
            };
        }

        public Tables.ExhibitionContentFr TextModelToTableFr(TextContent content)
        {

            return new Tables.ExhibitionContentFr
            {
                Id = content.Id,
                PoiId = content.PoiId,
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
                _tdgFr.Add(TextModelToTableFr(content));
            }
            else
                _tdgEn.Add(TextModelToTableEn(content));
        }

        public TextContent Get(int id)
        {
            if (Models.User.GetInstance().Language == Models.Language.Fr)
            {
                return TextTableToModelFr(_tdgFr.GetExhibitionContentFr(id));
            }
            else
                return TextTableToModelEn(_tdgEn.GetExhibitionContentEn(id));
        }

        public void Update(TextContent content)
        {
            if (content.Language == Models.Language.Fr)
                _tdgFr.Update(TextModelToTableFr(content));
            else
                _tdgEn.Update(TextModelToTableEn(content));
        }

    }
}