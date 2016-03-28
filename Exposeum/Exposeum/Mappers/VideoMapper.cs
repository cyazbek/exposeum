using Exposeum.TDGs;
using Exposeum.TempModels; 

namespace Exposeum.Mappers
{
    public class VideoMapper
    {
        private static VideoMapper _instance;
        private readonly ExhibitionContentEnTdg _tdgEn;
        private readonly ExhibitionContentFrTdg _tdgFr; 

        private VideoMapper()
        {
            _tdgEn = ExhibitionContentEnTdg.GetInstance();
            _tdgFr = ExhibitionContentFrTdg.GetInstance();
        }

        public static VideoMapper GetInstance()
        {
            if (_instance == null)
                _instance = new VideoMapper();
            return _instance; 
        }

        public VideoContent ConvertFromTable(Tables.ExhibitionContentEn content)
        {
            return new VideoContent
            {
                Id = content.Id,
                PoiId = content.PoiId,
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
                PoiId = content.PoiId,
                Title = content.Title,
                Duration = content.Duration,
                Resolution = content.Resolution,
                Encoding = content.Encoding,
                StorylineId = content.StoryLineId,
                Language = Models.Language.Fr,
                FilePath = content.Filepath
            };
        }

        public Tables.ExhibitionContentEn ConvertFromModelEn(VideoContent content)
        {
        
            return new Tables.ExhibitionContentEn
            {
                Id = content.Id,
                PoiId = content.PoiId,
                Title = content.Title,
                StoryLineId = content.StorylineId,
                Discriminator = "VideoContent",
                Filepath = content.FilePath,
                Duration = content.Duration,
                Resolution = content.Resolution,
                Encoding = content.Encoding
            };
        }

        public Tables.ExhibitionContentFr ConvertFromModelFr(VideoContent content)
        {

            return new Tables.ExhibitionContentFr
            {
                Id = content.Id,
                PoiId = content.PoiId,
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
                _tdgFr.Add(ConvertFromModelFr(content));
            }
            else
                _tdgEn.Add(ConvertFromModelEn(content));
        }

        public VideoContent Get(int id)
        {
            if (Models.User.GetInstance().Language == Models.Language.Fr)
            {
                return ConvertFromTable(_tdgFr.GetExhibitionContentFr(id));
            }
            else
                return ConvertFromTable(_tdgEn.GetExhibitionContentEn(id));
        }

        public void Update(VideoContent content)
        {
            if (content.Language == Models.Language.Fr)
                _tdgFr.Update(ConvertFromModelFr(content));
            else
                _tdgEn.Update(ConvertFromModelEn(content));
        }

    }
}