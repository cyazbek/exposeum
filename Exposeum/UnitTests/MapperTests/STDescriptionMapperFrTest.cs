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
        StoryLineDescriptionFrTDG _tdg = StoryLineDescriptionFrTDG.GetInstance();
        StorylineDescription _desc1;
        StorylineDescription _desc2;
        StorylineDescriptionMapperFr _mapper;
        StoryLineDescriptionFr _table1;
        StoryLineDescriptionFr _table2;


        [SetUp]
        public void SetUp()
        {
            _tdg._db.DeleteAll<StoryLineDescriptionFr>();
            _desc1 = new StorylineDescription
            {
                _title = "title",
                _description = "description",
                _language = Exposeum.Models.Language.Fr
            };
            _desc2 = new StorylineDescription
            {
                _title = "title",
                _description = "description",
                _language = Exposeum.Models.Language.Fr
            };
            _mapper = StorylineDescriptionMapperFr.GetInstance();
        }

        [Test()]
        public void AddGetDescriptionTest()
        {
            _tdg._db.Delete<StoryLineDescriptionFr>(_desc1._storyLineDescriptionId);
            _mapper.AddDescription(_desc1);
            _table1 = _tdg.GetStoryLineDescriptionFr(_desc1._storyLineDescriptionId);
            Assert.IsTrue(_desc1.Equals(_desc2));
        }

        [Test()]
        public void UpdateDescriptionTest()
        {
            _mapper.AddDescription(_desc2);
            _desc2._description.Equals("description2");
            _mapper.UpdateDescription(_desc2);
            Assert.IsTrue(_desc2._description.Equals(_mapper.GetDescription(_desc2._storyLineDescriptionId)._description));
        }

        [Test()]
        public void ConvertTableToModelTest()
        {
            _table1 = new StoryLineDescriptionFr
            {
                ID = _desc1._storyLineDescriptionId,
                description = _desc1._description,
                title = _desc1._title
            };

            _table2 = _mapper.DescriptionModelToTable(_desc1);
            Assert.IsTrue(_tdg.Equals(_table1, _table2));
        }

        [Test()]
        public void ConvertModelToTableTest()
        {
            _table1 = new StoryLineDescriptionFr
            {
                ID = 200,
                description = "none",
                title = "none"
            };
            _desc2._storyLineDescriptionId = _table1.ID;
            _desc2._title = _table1.title;
            _desc2._language = Exposeum.Models.Language.Fr;
            _desc2._description = _table1.description;

            _desc1 = _mapper.DescriptionTableToModel(_table1);
            Assert.IsTrue(_desc1.Equals(_desc2));
        }


    }
}