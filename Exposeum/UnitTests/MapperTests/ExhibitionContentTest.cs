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
        }



    }
}