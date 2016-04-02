using Exposeum.TDGs;
using Exposeum.Models; 

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

        public VideoContent VideoTableToModelEn(Tables.ExhibitionContentEn content)
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

        public VideoContent VideoTableToModelFr(Tables.ExhibitionContentFr content)
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

        public Tables.ExhibitionContentEn VideoModelToTableEn(VideoContent content)
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

        public Tables.ExhibitionContentFr VideoModelToTableFr(VideoContent content)
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
                _tdgFr.Add(VideoModelToTableFr(content));
            }
            else
                _tdgEn.Add(VideoModelToTableEn(content));
        }

        public VideoContent Get(int id)
        {
            if (Models.User.GetInstance().Language == Models.Language.Fr)
            {
                return VideoTableToModelFr(_tdgFr.GetExhibitionContentFr(id));
            }
            else
                return VideoTableToModelEn(_tdgEn.GetExhibitionContentEn(id));
        }

        public void Update(VideoContent content)
        {
            if (content.Language == Models.Language.Fr)
                _tdgFr.Update(VideoModelToTableFr(content));
            else
                _tdgEn.Update(VideoModelToTableEn(content));
        }

    }
}