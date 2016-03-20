using Exposeum.TempModels;
using Exposeum.Mappers;
using NUnit.Framework;

namespace UnitTests.MapperTests
{
    [TestFixture]
    public class StorylineDescriptionTest
    {
        StorylineDescriptionMapper _mapper;
        StorylineDescriptionMapperEn _englishMapper;
        StorylineDescriptionMapperFr _frenchMapper;
        StorylineDescription _modelEn;
        StorylineDescription _modelFr;
        StorylineDescription _testModel;
        Exposeum.Models.User _user = Exposeum.Models.User.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _modelEn = new StorylineDescription
            {
                _storyLineDescriptionId = 1,
                _description = "description",
                _title = "title",
                _language = Exposeum.Models.Language.En
            };

            _modelFr = new StorylineDescription
            {
                _storyLineDescriptionId = 1,
                _description = "description",
                _title = "title",
                _language = Exposeum.Models.Language.Fr
            };

            _mapper = StorylineDescriptionMapper.GetInstance();
            _englishMapper = StorylineDescriptionMapperEn.GetInstance();
            _frenchMapper = StorylineDescriptionMapperFr.GetInstance();
        }

        [Test()]
        public void AddDescriptionEnglishTest()
        {
            _mapper.AddDescription(_modelEn);
            _testModel = _mapper.GetDescription(_modelEn._storyLineDescriptionId);
            Assert.IsTrue(_modelEn.Equals(_testModel));
        }

        [Test()]
        public void AddDescriptionFrenchTest()
        {
            _mapper.AddDescription(_modelFr);
            _testModel = _mapper.GetDescription(_modelFr._storyLineDescriptionId);
            Assert.IsTrue(_modelEn.Equals(_testModel));
        }
    }
}