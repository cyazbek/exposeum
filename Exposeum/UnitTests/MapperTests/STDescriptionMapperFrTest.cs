using Exposeum.TempModels;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.Mappers;
using NUnit.Framework;

namespace UnitTests.MapperTests
{

    [TestFixture]
    class STDescriptionMapperFrTest
    {
        readonly StoryLineDescriptionFrTdg _tdg = StoryLineDescriptionFrTdg.GetInstance();
        StorylineDescription _desc1;
        StorylineDescription _desc2;
        StorylineDescriptionMapperFr _mapper;
        StoryLineDescriptionFr _table1;

        [SetUp]
        public void SetUp()
        {
            _tdg.Db.DeleteAll<StoryLineDescriptionFr>();

            _desc1 = new StorylineDescription
            {
                Title = "theTitle",
                Description = "theDescription",
                Language = Exposeum.Models.Language.Fr
            };

            _desc2 = new StorylineDescription
            {
                Title = "theTitle",
                Description = "theDescription",
                Language = Exposeum.Models.Language.Fr
            };

            _table1 = new StoryLineDescriptionFr
            {
                Title = "theTitle",
                Description = "theDescription"
            };

            _mapper = StorylineDescriptionMapperFr.GetInstance();
        }

        [Test]
        public void AddGetDescriptionTest()
        {
            _mapper.AddDescription(_desc1);
            StorylineDescription expected = _mapper.GetDescription(_desc1.StoryLineDescriptionId);
            Assert.IsTrue(_desc1.Equals(expected));
        }

        [Test]
        public void UpdateDescriptionTest()
        {
            _mapper.AddDescription(_desc2);
            _desc2.Description = "description2";
            _mapper.UpdateDescription(_desc2);
            Assert.IsTrue("description2".Equals(_mapper.GetDescription(_desc2.StoryLineDescriptionId).Description));
        }

        [Test]
        public void ConvertTableToModelTest()
        {
            StorylineDescription expected = _mapper.DescriptionTableToModel(_table1);
            Assert.IsTrue(_desc1.Equals(expected));
        }

        [Test]
        public void ConvertModelToTableTest()
        {
            StoryLineDescriptionFr expected = _mapper.DescriptionModelToTable(_desc1);
            Assert.IsTrue(_tdg.Equals(_table1, expected));
        }

    }
}