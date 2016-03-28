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
using User = Exposeum.Models.User;

namespace UnitTests
{
    [TestFixture]
    public class ExhibitionContentMapperTest
    {
        private static ExhibitionContentMapper _instance;
        private AudioContent _audioContent;
        private VideoContent _videoContent;
        private TextContent _textContent;
        private ImageContent _imageContent;

        private readonly List<ExhibitionContent> _contents = new List<ExhibitionContent>();
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

            _audioContent = new AudioContent
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

            _videoContent = new VideoContent
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

            _textContent = new TextContent
            {
                Id = 3,
                PoiId = 1,
                Title = "TextTitle",
                Language = Language.En,
                StorylineId = 1,
                HtmlContent = "TheContentEn"
            };

            _imageContent = new ImageContent
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

            _contents.Add(_audioContent);
            _contents.Add(_videoContent);
            _contents.Add(_textContent);
            _contents.Add(_imageContent);

            _poi = new PointOfInterest
            {
                Id = 1,
                ExhibitionContent = _contents
            };
        }

        [Test]
        public void GetInstanceExhibitionContentMapperTest()
        {
            Assert.NotNull(ExhibitionContentMapper.GetInstance());
        }

        [Test]
        public void AddExhibitionContentTest()
        {
            _user.Language = Language.En;
            _instance.AddExhibitionContent(_contents);
            _expected = _instance.GetExhibitionByPoiId(_poi.Id);
            Assert.True(ExhibitionContent.ListEquals(_contents, _expected));
        }

        [Test]
        public void UpdateExhibitionContentTest()
        {
            _user.Language = Language.En;

            _imageContent = new ImageContent
            {
                Id = 5,
                PoiId = 1,
                Title = "ImageTitle5",
                Language = Language.En,
                StorylineId = 1,
                FilePath = "ImagePath5",
                Width = 250,
                Height = 250
            };

            _contents.Add(_imageContent);

            _instance.UpdateExhibitionList(_contents);
            _expected = _instance.GetExhibitionByPoiId(_poi.Id);
            Assert.AreEqual(5, _expected.Count);
        }

        [Test]
        public void ExhibitionContentModelToTableTest()
        {
           
        }

        [Test()]
        public void ExhibitionContentTableToModelTest()
        {
            
        }

    }
}