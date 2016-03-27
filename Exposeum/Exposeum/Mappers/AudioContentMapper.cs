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

        public AudioContent ConvertFromTable(Tables.ExhibitionContentEn content)
        {
            return new AudioContent
            {
                Id = content.Id,
                Title = content.Title,
                Language = Models.Language.En,
                StorylineId = content.StoryLineId,
                Duration = content.Duration,
                Encoding = content.Encoding,
                FilePath = content.Filepath
            };
        }

        public AudioContent ConvertFromTable(Tables.ExhibitionContentFr content)
        {
            return new AudioContent
            {
                Id = content.Id,
                Title = content.Title,
                Language = Models.Language.Fr,
                FilePath = content.Filepath,
                Duration = content.Duration,
                Encoding = content.Encoding,
                StorylineId = content.StoryLineId
            };
        }

        public Tables.ExhibitionContentEn ConvertFromModelEN(AudioContent content)
        {

            return new Tables.ExhibitionContentEn
            {
                Id = content.Id,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "AudioContent",
                Filepath = content.FilePath,
                Duration = content.Duration,
                Encoding = content.Encoding
            };
        }

        public Tables.ExhibitionContentFr ConvertFromModelFR(AudioContent content)
        {

            return new Tables.ExhibitionContentFr
            {
                Id = content.Id,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "AudioContent",
                Filepath = content.FilePath,
                Duration = content.Duration,
                Encoding = content.Encoding
            };
        }

        public void Add(AudioContent content)
        {
            if (content.Language == Models.Language.Fr)
            {
                _tdgFr.Add(ConvertFromModelFR(content));
            }
            else
                _TdgEn.Add(ConvertFromModelEN(content));
        }

        public AudioContent Get(int id)
        {
            if (Models.User.GetInstance().Language == Models.Language.Fr)
            {
                return ConvertFromTable(_tdgFr.GetExhibitionContentFr(id));
            }
            else
                return ConvertFromTable(_TdgEn.GetExhibitionContentEn(id));
        }

        public void Update(AudioContent content)
        {
            if (content.Language == Models.Language.Fr)
                _tdgFr.Update(ConvertFromModelFR(content));
            else
                _TdgEn.Update(ConvertFromModelEN(content));
        }

    }
}