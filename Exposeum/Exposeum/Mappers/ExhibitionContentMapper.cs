using System.Collections.Generic;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.Models;
using ExhibitionContent = Exposeum.Models.ExhibitionContent;
using User = Exposeum.Models.User;

namespace Exposeum.Mappers
{
   
    public class ExhibitionContentMapper
    {

        private static ExhibitionContentMapper _instance;
        private readonly VideoMapper _videoMapper;
        private readonly AudioContentMapper _audioMapper;
        private readonly TextContentMapper _textMapper;
        private readonly ImageContentMapper _imageMapper;

        private readonly ExhibitionContentEnTdg _enTdg;
        private readonly ExhibitionContentFrTdg _frTdg;

        private readonly User _user; 

        private ExhibitionContentMapper()
        {
            _videoMapper = VideoMapper.GetInstance();
            _audioMapper = AudioContentMapper.GetInstance();
            _enTdg = ExhibitionContentEnTdg.GetInstance();
            _frTdg = ExhibitionContentFrTdg.GetInstance();
            _textMapper = TextContentMapper.GetInstance();
            _imageMapper = ImageContentMapper.GetInstance();
            _user = User.GetInstance();
        }

        public static ExhibitionContentMapper GetInstance()
        {
            if(_instance == null)
                _instance = new ExhibitionContentMapper();
            return _instance;
        }

        public List<ExhibitionContent> ContentTableToModelEn(List<ExhibitionContentEn> list)
        {
            List<ExhibitionContent> modelsList= new List<ExhibitionContent>();
            
            foreach (var x in list)
            {
                if (x.Discriminator == "TextContent")
                    modelsList.Add(_textMapper.TextTableToModelEn(x));
                else if (x.Discriminator == "AudioContent")
                    modelsList.Add(_audioMapper.AudioTableToModelEn(x));
                else if (x.Discriminator == "ImageContent")
                    modelsList.Add(_imageMapper.ImageTableToModelEn(x));
                else 
                    modelsList.Add(_videoMapper.VideoTableToModelEn(x));
            } 
            return modelsList;
        }

        public List<ExhibitionContent> ContentTableToModelFr(List<ExhibitionContentFr> list)
        {
            List<ExhibitionContent> modelsList = new List<ExhibitionContent>();

            foreach (var x in list)
            {
                if (x.Discriminator == "TextContent")
                    modelsList.Add(_textMapper.TextTableToModelFr(x));
                else if (x.Discriminator == "AudioContent")
                    modelsList.Add(_audioMapper.AudioTableToModelFr(x));
                else if (x.Discriminator == "ImageContent")
                    modelsList.Add(_imageMapper.ImageTableToModelFr(x));
                else
                    modelsList.Add(_videoMapper.VideoTableToModelFr(x));
            }
            return modelsList;
        }

        public List<ExhibitionContentEn> ContentModelToTableEn(List<ExhibitionContent> list)
        {
            List<ExhibitionContentEn> tableList = new List<ExhibitionContentEn>();
            foreach (var x in list)
            {
                if (x.GetType().ToString() == "Exposeum.Models.AudioContent")
                    tableList.Add(_audioMapper.AudioModelToTableEn((AudioContent)x));
                else if (x.GetType().ToString() == "Exposeum.Models.VideoContent")
                    tableList.Add(_videoMapper.VideoModelToTableEn((VideoContent)x));
                else if (x.GetType().ToString() == "Exposeum.Models.TextContent")
                    tableList.Add(_textMapper.TextModelToTableEn((TextContent)x));
                else 
                    tableList.Add(_imageMapper.ImageModelToTableEn((ImageContent)x));

            }
            return tableList;
        }
        public List<ExhibitionContentFr> ContentModelToTableFr(List<ExhibitionContent> list)
        {
            List<ExhibitionContentFr> tableList = new List<ExhibitionContentFr>();
            foreach (var x in list)
            {
                if (x.GetType().ToString() == "Exposeum.Models.AudioContent")
                    tableList.Add(_audioMapper.AudioModelToTableFr((AudioContent)x));
                else if (x.GetType().ToString() == "Exposeum.Models.VideoContent")
                    tableList.Add(_videoMapper.VideoModelToTableFr((VideoContent)x));
                else if (x.GetType().ToString() == "Exposeum.Models.TextContent")
                    tableList.Add(_textMapper.TextModelToTableFr((TextContent)x));
                else
                    tableList.Add(_imageMapper.ImageModelToTableFr((ImageContent)x));
            }
            return tableList;
        }

        public void AddExhibitionContent(List<ExhibitionContent> list)
        {
            var language = list[0].Language;

            if (language == Models.Language.En)
            {
                List<ExhibitionContentEn> tableList = new List<ExhibitionContentEn>();
                tableList = ContentModelToTableEn(list);
                foreach (var x in tableList)
                {
                    _enTdg.Add(x);
                }
            }
            else
            {
                List<ExhibitionContentFr> tableList = new List<ExhibitionContentFr>();
                tableList = ContentModelToTableFr(list);
                foreach (var x in tableList)
                {
                    _frTdg.Add(x);
                }
            }
        }

        public List<ExhibitionContent> GetExhibitionByPoiId(int id)
        {
            var language = _user.Language;
            List<ExhibitionContent> modelList;

            if (language == Models.Language.En)
            {
                modelList = ContentTableToModelEn(_enTdg.GetExhibitionContentByPoiId(id));
            }
            else
                modelList = ContentTableToModelFr(_frTdg.GetExhibitionContentByPoiId(id));

            return modelList;
        }

        public void UpdateExhibitionList(List<ExhibitionContent> list)
        {
            foreach (var x in list)
            {
                if(x.GetType().ToString()=="Exposeum.Models.TextContent")
                    _textMapper.Update((TextContent)x);
                else if (x.GetType().ToString() == "Exposeum.Models.AudioContent")
                    _audioMapper.Update((AudioContent)x);
                else if (x.GetType().ToString() == "Exposeum.Models.VideoContent")
                    _videoMapper.Update((VideoContent)x);
                else 
                    _imageMapper.Update((ImageContent)x);
            }
        }

    }
}