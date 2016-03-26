using Exposeum.TempModels;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.Mappers;
using NUnit.Framework;

namespace UnitTests.MapperTests
{

    [TestFixture]
    class STDescriptionMapperEnTest
    {
        readonly StoryLineDescriptionEnTdg _tdg = StoryLineDescriptionEnTdg.GetInstance();
        StorylineDescription _desc1;
        StorylineDescription _desc2;
        StorylineDescriptionMapperEn _mapper;
        StoryLineDescriptionEn _table1;

        [SetUp]
        public void SetUp()
        {
            _tdg.Db.DeleteAll<StoryLineDescriptionEn>();

            _desc1 = new StorylineDescription
            {
                Title = "theTitle",
                Description = "theDescription",
                Language = Exposeum.Models.Language.En
            };

            _desc2 = new StorylineDescription
            {
                Title = "theTitle",
                Description = "theDescription",
                Language = Exposeum.Models.Language.En
            };

            _table1 = new StoryLineDescriptionEn
            {
                Title = "theTitle",
                Description = "theDescription"
            };

            _mapper = StorylineDescriptionMapperEn.GetInstance();
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
            StoryLineDescriptionEn expected = _mapper.DescriptionModelToTable(_desc1);            
            Assert.IsTrue(_tdg.Equals(_table1,expected));
        }

    }
}