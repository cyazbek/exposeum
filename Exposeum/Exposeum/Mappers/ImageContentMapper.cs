using Exposeum.TDGs;
using Exposeum.TempModels;

namespace Exposeum.Mappers
{
    public class ImageContentMapper
    {
        private static ImageContentMapper _instance;
        private readonly ExhibitionContentEnTdg _tdgEn;
        private readonly ExhibitionContentFrTdg _tdgFr;

        private ImageContentMapper()
        {
            _tdgEn = ExhibitionContentEnTdg.GetInstance();
            _tdgFr = ExhibitionContentFrTdg.GetInstance();
        }

        public static ImageContentMapper GetInstance()
        {
            if (_instance == null)
                _instance = new ImageContentMapper();
            return _instance;
        }

        public ImageContent ConvertFromTable(Tables.ExhibitionContentEn content)
        {
            return new ImageContent
            {
                Id = content.Id,
                Title = content.Title,
                Language = Models.Language.En,
                StorylineId = content.StoryLineId,
                Width = content.Width,
                Height = content.Height,
                FilePath = content.Filepath
            };
        }

        public ImageContent ConvertFromTable(Tables.ExhibitionContentFr content)
        {
            return new ImageContent
            {
                Id = content.Id,
                Title = content.Title,
                Language = Models.Language.Fr,
                StorylineId = content.StoryLineId,
                Width = content.Width,
                Height = content.Height,
                FilePath = content.Filepath
            };
        }

        public Tables.ExhibitionContentEn ConvertFromModelEn(ImageContent content)
        {

            return new Tables.ExhibitionContentEn
            {
                Id = content.Id,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "ImageContent",
                Filepath = content.FilePath,
                Height = content.Height,
                Width = content.Width
            };
        }

        public Tables.ExhibitionContentFr ConvertFromModelFr(ImageContent content)
        {

            return new Tables.ExhibitionContentFr
            {
                Id = content.Id,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "ImageContent",
                Filepath = content.FilePath,
                Height = content.Height,
                Width = content.Width
            };
        }

        public void Add(ImageContent content)
        {
            if (content.Language == Models.Language.Fr)
            {
                _tdgFr.Add(ConvertFromModelFr(content));
            }
            else
                _tdgEn.Add(ConvertFromModelEn(content));
        }

        public ImageContent Get(int id)
        {
            if (Models.User.GetInstance().Language == Models.Language.Fr)
            {
                return ConvertFromTable(_tdgFr.GetExhibitionContentFr(id));
            }
            else
                return ConvertFromTable(_tdgEn.GetExhibitionContentEn(id));
        }

        public void Update(ImageContent content)
        {
            if (content.Language == Models.Language.Fr)
                _tdgFr.Update(ConvertFromModelFr(content));
            else
                _tdgEn.Update(ConvertFromModelEn(content));
        }

    }
}