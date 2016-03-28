using Exposeum.Mappers;
using Exposeum.TDGs;
using Exposeum.TempModels;
using NUnit.Framework;
using TablesEn = Exposeum.Tables.ExhibitionContentEn;
using TablesFr = Exposeum.Tables.ExhibitionContentFr;

namespace UnitTests
{
    [TestFixture]
    public class AudioContentMapperTest
    {
        private AudioContentMapper _mapper;
        private AudioContent _audioEn;
        private AudioContent _audioFr;
        private AudioContent _expectedAudio;
        private TablesEn _contentEn;
        private TablesFr _contentFr;
        private Exposeum.Models.User _user;

        [SetUp]
        public void SetUp()
        {
            _mapper = AudioContentMapper.GetInstance();
            _user = Exposeum.Models.User.GetInstance();

            _audioEn = new AudioContent
            {
                Id = 1,
                Title = "AudioTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };

            _audioFr = new AudioContent
            {
                Id = 1,
                Title = "AudioTitle",
                Language = Exposeum.Models.Language.Fr,
                StorylineId = 1,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };
        }

        [Test]
        public void AddAndGetTestEn()
        {
            _user.Language = Exposeum.Models.Language.En;
            _mapper.Add(_audioEn);

            _expectedAudio = _mapper.Get(_audioEn.Id);
            Assert.IsTrue(_audioEn.Equals(_expectedAudio));
        }

        [Test]
        public void UpdateAndGetTestEn()
        {
            _user.Language = Exposeum.Models.Language.En;
            _mapper.Add(_audioEn);

            _audioEn.Duration = 5;
            _mapper.Update(_audioEn);

            _expectedAudio = _mapper.Get(_audioEn.Id);
            Assert.AreEqual(5, _expectedAudio.Duration);
        }

        [Test]
        public void ConvertFromAndToModelTestFr()
        {
            _contentFr = _mapper.ConvertFromModelFr(_audioFr);
            _expectedAudio = _mapper.ConvertFromTable(_contentFr);

            Assert.IsTrue(_audioFr.Equals(_expectedAudio));
        }

        [Test]
        public void ConvertFromAndToModelTestEn()
        {
            _contentEn = _mapper.ConvertFromModelEn(_audioEn);
            _expectedAudio = _mapper.ConvertFromTable(_contentEn);

            Assert.IsTrue(_audioEn.Equals(_expectedAudio));
        }

        [Test]
        public void UpdateAndGetTestFr()
        {
            _user.Language = Exposeum.Models.Language.Fr;
            _mapper.Add(_audioFr);

            _audioFr.Duration = 5;
            _mapper.Update(_audioFr);

            _expectedAudio = _mapper.Get(_audioFr.Id);
            Assert.AreEqual(5, _expectedAudio.Duration);
        }

        [Test]
        public void AddAndGetTestFr()
        {
            _user.Language = Exposeum.Models.Language.Fr;
            _mapper.Add(_audioFr);

            _expectedAudio = _mapper.Get(_audioFr.Id);
            Assert.IsTrue(_audioFr.Equals(_expectedAudio));
        }

    }
}
 