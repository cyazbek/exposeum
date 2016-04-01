using Exposeum.Mappers;
using Exposeum.TDGs;
using Exposeum.TempModels;
using NUnit.Framework;
using TablesEn = Exposeum.Tables.ExhibitionContentEn;
using TablesFr = Exposeum.Tables.ExhibitionContentFr;

namespace UnitTests
{
    [TestFixture]
    public class VideoMapperTest
    {
        private VideoMapper _mapper;
        private VideoContent _videoEn;
        private VideoContent _videoFr;
        private VideoContent _expectedVideo;
        private TablesEn _contentEn;
        private TablesFr _contentFr;
        private Exposeum.Models.User _user;

        [SetUp]
        public void SetUp()
        {
            _mapper = VideoMapper.GetInstance();
            _user = Exposeum.Models.User.GetInstance(); 

            _videoEn = new VideoContent
            {
                Id = 1,
                Title = "VideoTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
            };

            _videoFr = new VideoContent
            {
                Id = 1,
                Title = "VideoTitle",
                Language = Exposeum.Models.Language.Fr,
                StorylineId = 1,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
            };
        }

        [Test()]
        public void ConvertFromAndToModelTestEn()
        {
            _contentEn = _mapper.VideoModelToTableEn(_videoEn);
            _expectedVideo = _mapper.VideoTableToModelEn(_contentEn);

            Assert.IsTrue(_videoEn.Equals(_expectedVideo));
        }

        [Test()]
        public void AddAndGetTestEn()
        {
            _user.Language = Exposeum.Models.Language.En;
            _mapper.Add(_videoEn);
            _expectedVideo = _mapper.Get(_videoEn.Id);
            Assert.IsTrue(_videoEn.Equals(_expectedVideo));
        }

        [Test()]
        public void UpdateAndGetTestEn()
        {
            _user.Language = Exposeum.Models.Language.En;
            _mapper.Add(_videoEn);
            _videoEn.StorylineId = 5;
            _mapper.Update(_videoEn);
            _expectedVideo = _mapper.Get(_videoEn.Id);
            Assert.AreEqual(_videoEn.StorylineId, _expectedVideo.StorylineId); 
        }

        [Test()]
        public void ConvertFromAndToModelTestFr()
        {
            _contentFr = _mapper.VideoModelToTableFr(_videoFr);
            _expectedVideo = _mapper.VideoTableToModelFr(_contentFr);

            Assert.IsTrue(_videoFr.Equals(_expectedVideo));
        }

        [Test()]
        public void AddAndGetTestFr()
        {
            _user.Language = Exposeum.Models.Language.Fr;
            _mapper.Add(_videoFr);
            _expectedVideo = _mapper.Get(_videoFr.Id);
            Assert.IsTrue(_videoFr.Equals(_expectedVideo));
        }

        [Test()]
        public void UpdateAndGetTestFr()
        {
            _user.Language = Exposeum.Models.Language.Fr;
            _mapper.Add(_videoFr);
            _videoFr.StorylineId = 5;
            _mapper.Update(_videoFr);
            _expectedVideo = _mapper.Get(_videoFr.Id);
            Assert.AreEqual(_videoFr.StorylineId, _expectedVideo.StorylineId);
        }

    }
}