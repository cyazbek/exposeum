using Exposeum.Mappers;
using Exposeum.Models;
using NUnit.Framework;
using TablesEn = Exposeum.Tables.ExhibitionContentEn;
using TablesFr = Exposeum.Tables.ExhibitionContentFr;

namespace UnitTests
{
    [TestFixture]
    public class TextContentMapperTest
    {
        private TextContentMapper _mapper;
        private TextContent _textEn;
        private TextContent _textFr;
        private TextContent _expectedText;
        private TablesEn _contentEn;
        private TablesFr _contentFr;
        private Exposeum.Models.User _user;

        [SetUp]
        public void SetUp()
        {
            _mapper = TextContentMapper.GetInstance();
            _user = Exposeum.Models.User.GetInstance();

            _textEn = new TextContent
            {
                Id = 1,
                Title = "TextTitle",
                Language = Exposeum.Models.Language.En,
                StorylineId = 1,
                HtmlContent = "TheContentEn"
            };

            _textFr = new TextContent
            {
                Id = 1,
                Title = "TextTitle",
                Language = Exposeum.Models.Language.Fr,
                StorylineId = 1,
                HtmlContent = "TheContentFr"
            };
        }

        [Test]
        public void AddAndGetTestEn()
        {
            _user.Language = Exposeum.Models.Language.En;
            _mapper.Add(_textEn);

            _expectedText = _mapper.Get(_textEn.Id);
            Assert.IsTrue(_textEn.Equals(_expectedText));
        }

        [Test]
        public void UpdateAndGetTestEn()
        {
            _user.Language = Exposeum.Models.Language.En;
            _mapper.Add(_textEn);

            _textEn.HtmlContent = "TheContentUpdatedEn";
            _mapper.Update(_textEn);

            _expectedText = _mapper.Get(_textEn.Id);
            Assert.AreEqual("TheContentUpdatedEn", _expectedText.HtmlContent);
        }

        [Test]
        public void ConvertFromAndToModelTestFr()
        {
            _contentFr = _mapper.TextModelToTableFr(_textFr);
            _expectedText = _mapper.TextTableToModelFr(_contentFr);

            Assert.IsTrue(_textFr.Equals(_expectedText));
        }

        [Test]
        public void AddAndGetTestFr()
        {
            _user.Language = Exposeum.Models.Language.Fr;
            _mapper.Add(_textFr);

            _expectedText = _mapper.Get(_textFr.Id);
            Assert.IsTrue(_textFr.Equals(_expectedText));
        }

        [Test]
        public void UpdateAndGetTestFr()
        {
            _user.Language = Exposeum.Models.Language.Fr;
            _mapper.Add(_textFr);

            _textFr.HtmlContent = "TheContentUpdatedFr";
            _mapper.Update(_textFr);

            _expectedText = _mapper.Get(_textFr.Id);
            Assert.AreEqual("TheContentUpdatedFr", _expectedText.HtmlContent);
        }

        [Test]
        public void ConvertFromAndToModelTestEn()
        {
            _contentEn = _mapper.TextModelToTableEn(_textEn);
            _expectedText = _mapper.TextTableToModelEn(_contentEn);

            Assert.IsTrue(_textEn.Equals(_expectedText));
        }
    }
}
