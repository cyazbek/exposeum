using Exposeum.TDGs;
using Exposeum.TempModels;

namespace Exposeum.Mappers
{
    public class AudioContentMapper
    {
        private static AudioContentMapper _instance;
        private readonly ExhibitionContentEnTdg _TdgEn;
        private readonly ExhibitionContentFrTdg _tdgFr;

        private AudioContentMapper()
        {
            _TdgEn = ExhibitionContentEnTdg.GetInstance();
            _tdgFr = ExhibitionContentFrTdg.GetInstance();
        }

        public static AudioContentMapper GetInstance()
        {
            if (_instance == null)
                _instance = new AudioContentMapper();
            return _instance;
        }

        public VideoContent ConvertFromTable(Tables.ExhibitionContentEn content)
        {
            return new VideoContent
            {
                Id = content.Id,
                Title = content.Title,
                Duration = content.Duration,
                Resolution = content.Resolution,
                Encoding = content.Encoding,
                StorylineId = content.StoryLineId,
                Language = Models.Language.En,
                FilePath = content.Filepath
            };
        }

        public VideoContent ConvertFromTable(Tables.ExhibitionContentFr content)
        {
            return new VideoContent
            {
                Id = content.Id,
                Title = content.Title,
                Duration = content.Duration,
                Resolution = content.Resolution,
                Encoding = content.Encoding,
                StorylineId = content.StoryLineId,
                Language = Models.Language.Fr,
                FilePath = content.Filepath
            };
        }

        public Tables.ExhibitionContentEn ConvertFromModelEN(VideoContent content)
        {

            return new Tables.ExhibitionContentEn
            {
                Id = content.Id,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "VideoContent",
                Filepath = content.FilePath,
                Duration = content.Duration,
                Resolution = content.Resolution,
                Encoding = content.Encoding
            };
        }

        public Tables.ExhibitionContentFr ConvertFromModelFR(VideoContent content)
        {

            return new Tables.ExhibitionContentFr
            {
                Id = content.Id,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "VideoContent",
                Filepath = content.FilePath,
                Duration = content.Duration,
                Resolution = content.Resolution,
                Encoding = content.Encoding
            };
        }

        public void Add(VideoContent content)
        {
            if (content.Language == Models.Language.Fr)
            {
                _tdgFr.Add(ConvertFromModelFR(content));
            }
            else
                _TdgEn.Add(ConvertFromModelEN(content));
        }

        public VideoContent Get(int id)
        {
            if (Models.User.GetInstance().Language == Models.Language.Fr)
            {
                return ConvertFromTable(_tdgFr.GetExhibitionContentFr(id));
            }
            else
                return ConvertFromTable(_TdgEn.GetExhibitionContentEn(id));
        }

        public void Update(VideoContent content)
        {
            if (content.Language == Models.Language.Fr)
                _tdgFr.Update(ConvertFromModelFR(content));
            else
                _TdgEn.Update(ConvertFromModelEN(content));
        }

    }
}