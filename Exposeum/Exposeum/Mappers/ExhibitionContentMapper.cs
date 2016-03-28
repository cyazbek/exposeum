using System.Collections.Generic;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.TempModels;
using ExhibitionContent = Exposeum.TempModels.ExhibitionContent;
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

        private readonly Models.User _user; 

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

        public List<ExhibitionContent> ConvertFromTableEn(List<Tables.ExhibitionContentEn> list)
        {
            List<ExhibitionContent> modelsList= new List<ExhibitionContent>();
            
            foreach (var x in list)
            {
                if (x.Discriminator == "TextContent")
                    modelsList.Add(_textMapper.ConvertFromTable(x));
                else if (x.Discriminator == "AudioContent")
                    modelsList.Add(_audioMapper.ConvertFromTable(x));
                else if (x.Discriminator == "ImageContent")
                    modelsList.Add(_imageMapper.ConvertFromTable(x));
                else 
                    modelsList.Add(_videoMapper.ConvertFromTable(x));
            } 
            return modelsList;
        }

        public List<ExhibitionContent> ConvertFromTableFr(List<Tables.ExhibitionContentFr> list)
        {
            List<ExhibitionContent> modelsList = new List<ExhibitionContent>();

            foreach (var x in list)
            {
                if (x.Discriminator == "TextContent")
                    modelsList.Add(_textMapper.ConvertFromTable(x));
                else if (x.Discriminator == "AudioContent")
                    modelsList.Add(_audioMapper.ConvertFromTable(x));
                else if (x.Discriminator == "ImageContent")
                    modelsList.Add(_imageMapper.ConvertFromTable(x));
                else
                    modelsList.Add(_videoMapper.ConvertFromTable(x));
            }
            return modelsList;
        }

        public List<Tables.ExhibitionContentEn> ConvertToTableEn(List<ExhibitionContent> list)
        {
            List<Tables.ExhibitionContentEn> tableList = new List<ExhibitionContentEn>();
            foreach (var x in list)
            {
                if (x.GetType().ToString() == "Exposeum.TempModels.AudioContent")
                    tableList.Add(_audioMapper.ConvertFromModelEn((AudioContent)x));
                else if (x.GetType().ToString() == "Exposeum.TempModels.VideoContent")
                    tableList.Add(_videoMapper.ConvertFromModelEn((VideoContent)x));
                else if (x.GetType().ToString() == "Exposeum.TempModels.TextContent")
                    tableList.Add(_textMapper.ConvertFromModelEn((TextContent)x));
                else 
                    tableList.Add(_imageMapper.ConvertFromModelEn((ImageContent)x));

            }
            return tableList;
        }
        public List<Tables.ExhibitionContentFr> ConvertToTableFr(List<ExhibitionContent> list)
        {
            List<Tables.ExhibitionContentFr> tableList = new List<ExhibitionContentFr>();
            foreach (var x in list)
            {
                if (x.GetType().ToString() == "Exposeum.TempModels.AudioContent")
                    tableList.Add(_audioMapper.ConvertFromModelFr((AudioContent)x));
                else if (x.GetType().ToString() == "Exposeum.TempModels.VideoContent")
                    tableList.Add(_videoMapper.ConvertFromModelFr((VideoContent)x));
                else if (x.GetType().ToString() == "Exposeum.TempModels.TextContent")
                    tableList.Add(_textMapper.ConvertFromModelFr((TextContent)x));
                else
                    tableList.Add(_imageMapper.ConvertFromModelFr((ImageContent)x));
            }
            return tableList;
        }

        public void AddExhibitionContent(List<ExhibitionContent> list)
        {
            var language = list[0].Language;

            if (language == Models.Language.En)
            {
                List<ExhibitionContentEn> tableList = new List<ExhibitionContentEn>();
                tableList = ConvertToTableEn(list);
                foreach (var x in tableList)
                {
                    _enTdg.Add(x);
                }
            }
            else
            {
                List<ExhibitionContentFr> tableList = new List<ExhibitionContentFr>();
                tableList = ConvertToTableFr(list);
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
                modelList = ConvertFromTableEn(_enTdg.GetExhibitionContentByPoiId(id));
            }
            else
                modelList = ConvertFromTableFr(_frTdg.GetExhibitionContentByPoiId(id));

            return modelList;
        }

        public void UpdateExhibitionList(List<ExhibitionContent> list)
        {
            foreach (var x in list)
            {
                if(x.GetType().ToString()=="Exposeum.TempModels.TextContent")
                    _textMapper.Update((TextContent)x);
                else if (x.GetType().ToString() == "Exposeum.TempModels.AudioContent")
                    _audioMapper.Update((AudioContent)x);
                else if (x.GetType().ToString() == "Exposeum.TempModels.VideoContent")
                    _videoMapper.Update((VideoContent)x);
                else 
                    _imageMapper.Update((ImageContent)x);
            }
        }

    }
}