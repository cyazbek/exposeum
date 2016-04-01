using Exposeum.Mappers;
using Exposeum.TempModels;
using NUnit.Framework;
using TablesEn = Exposeum.Tables.ExhibitionContentEn;
using TablesFr = Exposeum.Tables.ExhibitionContentFr;

namespace UnitTests
{
    [TestFixture]
    public class ImageContentMapperTest
    {
        private ImageContentMapper _mapper;
        private ImageContent _imageEn;
        private ImageContent _imageFr;
        private ImageContent _expectedImage;
        private TablesEn _contentEn;
        private TablesFr _contentFr;
        private Exposeum.Models.User _user;

        [SetUp]
        public void SetUp()
        {
            _mapper = ImageContentMapper.GetInstance();
            _user = Exposeum.Models.User.GetInstance();

            _imageEn = new ImageContent
            {
                Id = 1,
                Title = "ImageTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                FilePath = "ImagePath",
                Width = 250,
                Height = 250
            };

            _imageFr = new ImageContent
            {
                Id = 1,
                Title = "ImageTitle",
                Language = Exposeum.Models.Language.Fr,
                StorylineId = 1,
                FilePath = "ImagePath",
                Width = 250,
                Height = 250
            };
        }

        [Test]
        public void AddAndGetTestEn()
        {
            _user.Language = Exposeum.Models.Language.En;
            _mapper.Add(_imageEn);

            _expectedImage = _mapper.Get(_imageEn.Id);
            Assert.IsTrue(_imageEn.Equals(_expectedImage));
        }

        [Test]
        public void UpdateAndGetTestEn()
        {
            _user.Language = Exposeum.Models.Language.En;
            _mapper.Add(_imageEn);

            _imageEn.Width = 500;
            _mapper.Update(_imageEn);

            _expectedImage = _mapper.Get(_imageEn.Id);
            Assert.AreEqual(500, _expectedImage.Width);
        }

        [Test]
        public void ConvertFromAndToModelTestFr()
        {
            _contentFr = _mapper.ImageModelToTableFr(_imageFr);
            _expectedImage = _mapper.ImageTableToModelFr(_contentFr);

            Assert.IsTrue(_imageFr.Equals(_expectedImage));
        }

        [Test]
        public void AddAndGetTestFr()
        {
            _user.Language = Exposeum.Models.Language.Fr;
            _mapper.Add(_imageFr);

            _expectedImage = _mapper.Get(_imageFr.Id);
            Assert.IsTrue(_imageFr.Equals(_expectedImage));
        }

        [Test]
        public void UpdateAndGetTestFr()
        {
            _user.Language = Exposeum.Models.Language.Fr;
            _mapper.Add(_imageFr);

            _imageFr.Width = 500;
            _mapper.Update(_imageFr);

            _expectedImage = _mapper.Get(_imageFr.Id);
            Assert.AreEqual(500, _expectedImage.Width);
        }

        [Test]
        public void ConvertFromAndToModelTestEn()
        {
            _contentEn = _mapper.ImageModelToTableEn(_imageEn);
            _expectedImage = _mapper.ImageTableToModelEn(_contentEn);

            Assert.IsTrue(_imageEn.Equals(_expectedImage));
        }
    }
}
