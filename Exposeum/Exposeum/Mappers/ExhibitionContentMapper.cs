using Exposeum.TDGs;
using Exposeum.TempModels;

namespace Exposeum.Mappers
{
   
    public class ExhibitionContentMapper
    {
        private static ExhibitionContentMapper _instance;
        private readonly ExhibitionContentEnTdg _exhibitionContentEnTdg;
        private readonly ExhibitionContentFrTdg _exhibitionContentFrTdg;


        private ExhibitionContentMapper()
        {
            _exhibitionContentEnTdg = ExhibitionContentEnTdg.GetInstance();
            _exhibitionContentFrTdg = ExhibitionContentFrTdg.GetInstance();
        }

        public static ExhibitionContentMapper GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentMapper();
            return _instance;
        }

        public Tables.ExhibitionContentFr ConvertToTablesFr(VideoContent content)
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

        public Tables.ExhibitionContentFr ConvertToTablesFr(AudioContent content)
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

        public Tables.ExhibitionContentFr ConvertToTablesFr(TextContent content)
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
        
        public Tables.ExhibitionContentFr ConvertToTablesFr(ImageContent content)
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

        public Tables.ExhibitionContentEn ConvertToTablesEn(VideoContent content)
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

        public Tables.ExhibitionContentEn ConvertToTablesEn(AudioContent content)
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

        public Tables.ExhibitionContentEn ConvertToTablesEn(TextContent content)
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

        public Tables.ExhibitionContentEn ConvertToTablesEn(ImageContent content)
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

        public ExhibitionContent ConvertFromTable(Tables.ExhibitionContentEn content)
        {
            string descriminator = content.Discriminator; 
            switch (descriminator)
            {
                case "VideoContent":
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

                case "AudioContent":
                    return new AudioContent
                    {
                        Id = content.Id,
                        Title = content.Title,
                        StorylineId = content.StoryLineId,
                        FilePath = content.Filepath,
                        Duration = content.Duration,
                        Encoding = content.Encoding,
                        Language = Models.Language.En
                    };

                case "TextContent":
                    return new TextContent
                    {
                        Id = content.Id,
                        Title = content.Title,
                        StorylineId = content.StoryLineId,
                        Language = Models.Language.En,
                        HtmlContent = content.Description
                    };

                default:
                    return new ImageContent
                    {
                        Id = content.Id,
                        Title = content.Title,
                        StorylineId = content.StoryLineId,
                        FilePath = content.Filepath,
                        Height = content.Height,
                        Width = content.Width,
                        Language = Models.Language.En
                    }; 
            }
        }

        public ExhibitionContent ConvertFromTable(Tables.ExhibitionContentFr content)
        {
            string descriminator = content.Discriminator;
            switch (descriminator)
            {
                case "VideoContent":
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

                case "AudioContent":
                    return new AudioContent
                    {
                        Id = content.Id,
                        Title = content.Title,
                        StorylineId = content.StoryLineId,
                        FilePath = content.Filepath,
                        Duration = content.Duration,
                        Encoding = content.Encoding,
                        Language = Models.Language.Fr
                    };

                case "TextContent":
                    return new TextContent
                    {
                        Id = content.Id,
                        Title = content.Title,
                        StorylineId = content.StoryLineId,
                        Language = Models.Language.Fr,
                        HtmlContent = content.Description
                    };

                default:
                    return new ImageContent
                    {
                        Id = content.Id,
                        Title = content.Title,
                        StorylineId = content.StoryLineId,
                        FilePath = content.Filepath,
                        Height = content.Height,
                        Width = content.Width,
                        Language = Models.Language.Fr
                    };
            }
        }

        public void add(ExhibitionContent content)
        {
            var type = content.GetType();
            Models.Language lang = content.Language;
            if (lang == Models.Language.Fr)
            {
                if (type.Equals(typeof(VideoContent)))
                {
                    _exhibitionContentFrTdg.Add(ConvertToTablesFr((VideoContent)content));
                }
                else if (type.Equals(typeof(AudioContent)))
                {
                    _exhibitionContentFrTdg.Add(ConvertToTablesFr((AudioContent)content));
                }
                else if (type.Equals(typeof(TextContent)))
                {
                    _exhibitionContentFrTdg.Add(ConvertToTablesFr((TextContent)content));
                }
                else
                {
                    _exhibitionContentFrTdg.Add(ConvertToTablesFr((ImageContent)content));
                }
            }
            else
            {
                if (type.Equals(typeof(VideoContent)))
                {
                    _exhibitionContentEnTdg.Add(ConvertToTablesEn((VideoContent)content));
                }
                else if (type.Equals(typeof(AudioContent)))
                {
                    _exhibitionContentEnTdg.Add(ConvertToTablesEn((AudioContent)content));
                }
                else if (type.Equals(typeof(TextContent)))
                {
                    _exhibitionContentEnTdg.Add(ConvertToTablesEn((TextContent)content));
                }
                else
                {
                    _exhibitionContentEnTdg.Add(ConvertToTablesEn((ImageContent)content));
                }
            }
        }

        public ExhibitionContent Get(int id)
        {
            if (Models.User.GetInstance().Language == Models.Language.Fr)
            {
                return ConvertFromTable(_exhibitionContentFrTdg.GetExhibitionContentFr(id));
            }
            else
                return ConvertFromTable(_exhibitionContentEnTdg.GetExhibitionContentEn(id));
        }

        public void Update(ExhibitionContent content)
        {
            var type = content.GetType();
            Models.Language lang = content.Language;
            if (lang == Models.Language.Fr)
            {
                if (type.Equals(typeof(VideoContent)))
                {
                    _exhibitionContentFrTdg.Update(ConvertToTablesFr((VideoContent)content));
                }
                else if (type.Equals(typeof(AudioContent)))
                {
                    _exhibitionContentFrTdg.Update(ConvertToTablesFr((AudioContent)content));
                }
                else if (type.Equals(typeof(TextContent)))
                {
                    _exhibitionContentFrTdg.Update(ConvertToTablesFr((TextContent)content));
                }
                else
                {
                    _exhibitionContentFrTdg.Update(ConvertToTablesFr((ImageContent)content));
                }
            }
            else
            {
                if (type.Equals(typeof(VideoContent)))
                {
                    _exhibitionContentEnTdg.Update(ConvertToTablesEn((VideoContent)content));
                }
                else if (type.Equals(typeof(AudioContent)))
                {
                    _exhibitionContentEnTdg.Update(ConvertToTablesEn((AudioContent)content));
                }
                else if (type.Equals(typeof(TextContent)))
                {
                    _exhibitionContentEnTdg.Update(ConvertToTablesEn((TextContent)content));
                }
                else
                {
                    _exhibitionContentEnTdg.Update(ConvertToTablesEn((ImageContent)content));
                }
            }

        }
    }
}