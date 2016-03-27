using Exposeum.TempModels;
using Exposeum.Mappers;
using NUnit.Framework;
using Exposeum.TDGs;
using TablesEn = Exposeum.Tables.ExhibitionContentEn;
using TablesFr = Exposeum.Tables.ExhibitionContentFr;

namespace UnitTests.MapperTests
{
    [TestFixture]
    public class VideoMapperTest
    {
        public VideoMapper _mapper;
        public ExhibitionContentEnTdg _tdgEn;
        public ExhibitionContentFrTdg _tdgFr;
        public VideoContent _videoEn;
        public VideoContent _videoFr;
        public VideoContent _expectedVideo;
        public TablesEn _contentEn;
        public TablesFr _contentFr;
        public Exposeum.Models.User _user;

        [SetUp]
        public void SetUp()
        {
            _mapper = VideoMapper.GetInstance();
            _tdgEn = ExhibitionContentEnTdg.GetInstance();
            _tdgFr = ExhibitionContentFrTdg.GetInstance();
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
            _contentEn = _mapper.ConvertFromModelEN(_videoEn);
            _expectedVideo = _mapper.ConvertFromTable(_contentEn);

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
            _contentFr = _mapper.ConvertFromModelFR(_videoFr);
            _expectedVideo = _mapper.ConvertFromTable(_contentFr);

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