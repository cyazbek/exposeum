using System.Collections.Generic;
using Exposeum.TDGs;
using Exposeum.TempModels;
using Java.Security.Interfaces;
using Javax.Crypto.Interfaces;
using ExhibitionContent = Exposeum.TempModels.ExhibitionContent;

namespace Exposeum.Mappers
{
   
    public class ExhibitionContentMapper
    {
        private static ExhibitionContentMapper _instance;
        private readonly TextContentMapper _textContentMapper;
        private readonly VideoMapper _videoMapper;
        private readonly AudioContentMapper _audioContentMapper;
        private readonly ImageContentMapper _imageContentMapper;
        private readonly ExhibitionContentListTDG _exhibitionContentListTdg;
        private readonly ExhibitionContentEnTdg _englihsTdg;
        private readonly ExhibitionContentFrTdg _frenchTdg;
        private readonly Models.User _user; 


        private ExhibitionContentMapper()
        {
            _textContentMapper = TextContentMapper.GetInstance();
            _videoMapper = VideoMapper.GetInstance();
            _audioContentMapper = AudioContentMapper.GetInstance();
            _imageContentMapper = ImageContentMapper.GetInstance();
            _exhibitionContentListTdg = ExhibitionContentListTDG.GetInstance();
            _englihsTdg = ExhibitionContentEnTdg.GetInstance();
            _frenchTdg = ExhibitionContentFrTdg.GetInstance();
            _user = Models.User.GetInstance();
        }

        public static ExhibitionContentMapper GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentMapper();
            return _instance;
        }

        public List<ExhibitionContent> GetContentList(List<int> list)
        {
           List<ExhibitionContent> exhibitionContents = new List<ExhibitionContent>();
            if (_user.Language == Models.Language.Fr)
            {
                foreach (var x in list)
                {
                    var table = _frenchTdg.GetExhibitionContentFr(x);
                    if (table.Discriminator == "TextContent")
                    {
                        exhibitionContents.Add(_textContentMapper.Get(x));
                    }
                    else if (table.Discriminator == "AudioContent")
                    {
                        exhibitionContents.Add(_audioContentMapper.Get(x));
                    }
                    else if (table.Discriminator == "VideoContent")
                    {
                        exhibitionContents.Add(_videoMapper.Get(x));
                    }
                    else
                        exhibitionContents.Add(_imageContentMapper.Get(x));
                     
                }
                
            }
            return exhibitionContents;
        }

        public void AddExhibitionContents(int id, List<ExhibitionContent> list)
        {
            List<int> contentIds = new List<int>();

            foreach (var x in list)
            {
                if (x.GetType().ToString()=="Exposeum.TempModels.TextContent")
                {
                    _textContentMapper.Add((TextContent)x);
                    
                }
                else if (x.GetType().ToString() == "Exposeum.TempModels.AudioContent")
                {
                    _audioContentMapper.Add((AudioContent)x);
                }
                else if (x.GetType().ToString() == "Exposeum.TempModels.VideoCotent")
                {
                    _videoMapper.Add((VideoContent)x);
                }
                else
                    _imageContentMapper.Add((ImageContent)x);

                contentIds.Add(x.Id);
            }
            _exhibitionContentListTdg.AddList(contentIds,id);
        }   

        


    }
}