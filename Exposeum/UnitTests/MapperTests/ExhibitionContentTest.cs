using Exposeum.Mappers;
using Exposeum.TDGs;
using Exposeum.TempModels;
using Java.Util;
using NUnit.Framework;

namespace UnitTests.MapperTests
{
    [TestFixture]
    public class ExhibitionContentTest
    {
        private static ExhibitionContentMapper _mapper;
        private VideoContent _video;
        private VideoContent _expectedVideo;
        private AudioContent _audio;
        private AudioContent _expectedAudio;
        private TextContent _text;
        private TextContent _Expectedtext;
        private ImageContent _image;
        private ImageContent _Expectedimage;
        private Exposeum.Tables.ExhibitionContentEn _tableContentEN;
        private Exposeum.Tables.ExhibitionContentFr _tableContentFR;
        private static Exposeum.Models.User _user; 

        [SetUp]
        public void SetUp()
        {
            _user = Exposeum.Models.User.GetInstance();
            _mapper = ExhibitionContentMapper.GetInstance();

            _video = new VideoContent
            {
                Id=1,
                Title="VideoTitle",
                Language=Exposeum.Models.Language.En,
                StorylineId=1,
                FilePath="VideoPath",
                Duration=1,
                Encoding="VideoEncoding"
            };

            _audio = new AudioContent
            {
                Id=2,
                Title="AudioTitle",
                Language = Exposeum.Models.Language.Fr,
                StorylineId=1, 
                Duration=1, 
                Encoding="AudioEncoding"
            };

            _text = new TextContent
            {
                Id = 3,
                Title = "TextTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                HtmlContent="TextContent"
            };

            _image = new ImageContent
            {
                Id = 4,
                Title = "ImageTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                FilePath="ImagePath",
                Width =1,
                Height = 2
            };

        }
        [Test()]
        public void ConvertFromModelToTable()
        {
            _tableContentEN = _mapper.ConvertToTablesEn(_video);
            _expectedVideo =(VideoContent) _mapper.ConvertFromTable(_tableContentEN);
            Assert.IsTrue(_video.Equals(_expectedVideo));
        }

        [Test()]
        public void AddAndGetTest()
        {
            _mapper.add(_video);
            _expectedVideo = (VideoContent) _mapper.Get(_video.Id);
            Assert.IsTrue(_video.Equals(_expectedVideo));
        }
        [Test()]
        public void GetTest()
        {
        }

        [Test()]
        public void UpdateTest()
        {

        }


    }
}