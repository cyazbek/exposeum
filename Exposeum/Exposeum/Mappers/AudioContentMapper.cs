using Exposeum.TDGs;
using Exposeum.Models;

namespace Exposeum.Mappers
{
    public class AudioContentMapper
    {
        private static AudioContentMapper _instance;
        private readonly ExhibitionContentEnTdg _tdgEn;
        private readonly ExhibitionContentFrTdg _tdgFr;

        private AudioContentMapper()
        {
            _tdgEn = ExhibitionContentEnTdg.GetInstance();
            _tdgFr = ExhibitionContentFrTdg.GetInstance();
        }

        public static AudioContentMapper GetInstance()
        {
            if (_instance == null)
                _instance = new AudioContentMapper();
            return _instance;
        }

        public Models.AudioContent AudioTableToModelEn(Tables.ExhibitionContentEn content)
        {
            return new Models.AudioContent
            {
                Id = content.Id,
                PoiId = content.PoiId,
                Title = content.Title,
                Language = Models.Language.En,
                StorylineId = content.StoryLineId,
                Duration = content.Duration,
                Encoding = content.Encoding,
                FilePath = content.Filepath
            };
        }

        public AudioContent AudioTableToModelFr(Tables.ExhibitionContentFr content)
        {
            return new AudioContent
            {
                Id = content.Id,
                PoiId = content.PoiId,
                Title = content.Title,
                Language = Models.Language.Fr,
                FilePath = content.Filepath,
                Duration = content.Duration,
                Encoding = content.Encoding,
                StorylineId = content.StoryLineId
            };
        }

        public Tables.ExhibitionContentEn AudioModelToTableEn(AudioContent content)
        {

            return new Tables.ExhibitionContentEn
            {
                Id = content.Id,
                PoiId = content.PoiId,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "AudioContent",
                Filepath = content.FilePath,
                Duration = content.Duration,
                Encoding = content.Encoding
            };
        }

        public Tables.ExhibitionContentFr AudioModelToTableFr(AudioContent content)
        {

            return new Tables.ExhibitionContentFr
            {
                Id = content.Id,
                PoiId = content.PoiId,
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
                _tdgFr.Add(AudioModelToTableFr(content));
            }
            else
                _tdgEn.Add(AudioModelToTableEn(content));
        }

        public AudioContent Get(int id)
        {
            if (Models.User.GetInstance().Language == Models.Language.Fr)
            {
                return AudioTableToModelFr(_tdgFr.GetExhibitionContentFr(id));
            }
            else
                return AudioTableToModelEn(_tdgEn.GetExhibitionContentEn(id));
        }

        public void Update(AudioContent content)
        {
            if (content.Language == Models.Language.Fr)
                _tdgFr.Update(AudioModelToTableFr(content));
            else
                _tdgEn.Update(AudioModelToTableEn(content));
        }

    }
}