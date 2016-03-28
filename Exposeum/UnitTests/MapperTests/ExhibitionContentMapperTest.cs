using System.Collections.Generic;
using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.TempModels;
using Java.Util;
using NUnit.Framework;
using AudioContent = Exposeum.TempModels.AudioContent;
using ExhibitionContent = Exposeum.TempModels.ExhibitionContent;
using ImageContent = Exposeum.TempModels.ImageContent;
using PointOfInterest = Exposeum.TempModels.PointOfInterest;
using TextContent = Exposeum.TempModels.TextContent;
using User = Exposeum.Models.User;

namespace UnitTests
{
    [TestFixture]
    public class ExhibitionContentMapperTest
    {
        private static ExhibitionContentMapper _instance;

        private AudioContent _audioContentEn;
        private VideoContent _videoContentEn;
        private TextContent _textContentEn;
        private ImageContent _imageContentEn;

        private AudioContent _audioContentFr;
        private VideoContent _videoContentFr;
        private TextContent _textContentFr;
        private ImageContent _imageContentFr;

        private readonly List<ExhibitionContent> _contentsEnglish = new List<ExhibitionContent>();
        private readonly List<ExhibitionContent> _contentsFrench = new List<ExhibitionContent>();

        private List<ExhibitionContent> _expected;
        private PointOfInterest _poi;

        private readonly ExhibitionContentEnTdg _tdgEn = ExhibitionContentEnTdg.GetInstance();
        private readonly ExhibitionContentFrTdg _tdgFr = ExhibitionContentFrTdg.GetInstance();
        private readonly Exposeum.Models.User _user = User.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = ExhibitionContentMapper.GetInstance();

            _tdgEn.Db.DeleteAll<ExhibitionContentEn>();
            _tdgFr.Db.DeleteAll<ExhibitionContentFr>();

            _audioContentEn = new AudioContent
            {
                Id = 1,
                PoiId = 1,
                Title = "AudioTitle",
                Language = Language.En,
                StorylineId = 1,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };

            _videoContentEn = new VideoContent
            {
                Id = 2,
                PoiId = 1,
                Title = "VideoTitle",
                Language = Language.En,
                StorylineId = 1,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
            };

            _textContentEn = new TextContent
            {
                Id = 3,
                PoiId = 1,
                Title = "TextTitle",
                Language = Language.En,
                StorylineId = 1,
                HtmlContent = "TheContentEn"
            };

            _imageContentEn = new ImageContent
            {
                Id = 4,
                PoiId = 1,
                Title = "ImageTitle",
                Language = Language.En,
                StorylineId = 1,
                FilePath = "ImagePath",
                Width = 250,
                Height = 250
            };

            _audioContentFr = new AudioContent
            {
                Id = 1,
                PoiId = 1,
                Title = "AudioTitle",
                Language = Language.Fr,
                StorylineId = 1,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };

            _videoContentFr = new VideoContent
            {
                Id = 2,
                PoiId = 1,
                Title = "VideoTitle",
                Language = Language.Fr,
                StorylineId = 1,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
            };

            _textContentFr = new TextContent
            {
                Id = 3,
                PoiId = 1,
                Title = "TextTitle",
                Language = Language.Fr,
                StorylineId = 1,
                HtmlContent = "TheContentEn"
            };

            _imageContentFr = new ImageContent
            {
                Id = 4,
                PoiId = 1,
                Title = "ImageTitle",
                Language = Language.Fr,
                StorylineId = 1,
                FilePath = "ImagePath",
                Width = 250,
                Height = 250
            };

            _contentsEnglish.Add(_audioContentEn);
            _contentsEnglish.Add(_videoContentEn);
            _contentsEnglish.Add(_textContentEn);
            _contentsEnglish.Add(_imageContentEn);

            _contentsFrench.Add(_audioContentFr);
            _contentsFrench.Add(_videoContentFr);
            _contentsFrench.Add(_textContentFr);
            _contentsFrench.Add(_imageContentFr);

            _poi = new PointOfInterest
            {
                Id = 1,
                ExhibitionContent = _contentsEnglish
            };
        }

        [Test]
        public void GetInstanceExhibitionContentMapperTest()
        {
            Assert.NotNull(ExhibitionContentMapper.GetInstance());
        }

        [Test]
        public void AddExhibitionContentEnTest()
        {
            _user.Language = Language.En;
            _instance.AddExhibitionContent(_contentsEnglish);
            _expected = _instance.GetExhibitionByPoiId(_poi.Id);
            Assert.True(ExhibitionContent.ListEquals(_contentsEnglish, _expected));
        }

        public void AddExhibitionContentFrTest()
        {
            _user.Language = Language.Fr;
            _instance.AddExhibitionContent(_contentsFrench);
            _expected = _instance.GetExhibitionByPoiId(_poi.Id);
            Assert.True(ExhibitionContent.ListEquals(_contentsFrench, _expected));
        }

        [Test]
        public void UpdateExhibitionContentEnTest()
        {
            _user.Language = Language.En;

            _audioContentEn = new AudioContent
            {
                Id = 5,
                PoiId = 1,
                Title = "AudioTitle",
                Language = Language.En,
                StorylineId = 1,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };

            _videoContentEn = new VideoContent
            {
                Id = 6,
                PoiId = 1,
                Title = "VideoTitle",
                Language = Language.En,
                StorylineId = 1,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
            };

            List<ExhibitionContent> contents2 = new List<ExhibitionContent>();
            contents2.Add(_audioContentEn);
            contents2.Add(_videoContentEn);

            _instance.AddExhibitionContent(contents2);

            contents2[1].Title = "TitleUpdated";
            _instance.UpdateExhibitionList(contents2);

            _expected = _instance.GetExhibitionByPoiId(_poi.Id);
            Assert.AreEqual("TitleUpdated", _expected[1].Title);
        }

        [Test]
        public void UpdateExhibitionContentFrTest()
        {
            _user.Language = Language.Fr;

            _audioContentFr = new AudioContent
            {
                Id = 5,
                PoiId = 1,
                Title = "AudioTitle",
                Language = Language.Fr,
                StorylineId = 1,
                FilePath = "AudioPath",
                Duration = 1,
                Encoding = "AudioEncoding"
            };

            _videoContentFr = new VideoContent
            {
                Id = 6,
                PoiId = 1,
                Title = "VideoTitle",
                Language = Language.Fr,
                StorylineId = 1,
                FilePath = "VideoPath",
                Duration = 1,
                Encoding = "VideoEncoding"
            };

            List<ExhibitionContent> contents3 = new List<ExhibitionContent>();
            contents3.Add(_audioContentFr);
            contents3.Add(_videoContentFr);

            _instance.AddExhibitionContent(contents3);

            contents3[1].Title = "TitleUpdated";
            _instance.UpdateExhibitionList(contents3);

            _expected = _instance.GetExhibitionByPoiId(_poi.Id);
            Assert.AreEqual("TitleUpdated", _expected[1].Title);
        }

        [Test]
        public void ConvertToAndFromTableFrTest()
        {
            List<ExhibitionContentFr> contentFrTable = _instance.ConvertToTableFr(_contentsFrench);
            List<ExhibitionContent> contentFrModel = _instance.ConvertFromTableFr(contentFrTable);

            Assert.AreEqual(_contentsFrench, contentFrModel);
        }

        [Test()]
        public void ConvertToAndFromTableEnTest()
        {
            List<ExhibitionContentEn> contentEnTable = _instance.ConvertToTableEn(_contentsEnglish);
            List<ExhibitionContent> contentEnModel = _instance.ConvertFromTableEn(contentEnTable);

            Assert.AreEqual(_contentsEnglish, contentEnModel);

        }


    }
}