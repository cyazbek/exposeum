using Exposeum.TDGs;
using Exposeum.Mappers;
using NUnit.Framework;
using Exposeum.Models; 

namespace UnitTests.MapperTests
{
    [TestFixture]
    public class StatusMapperTest
    {
        public Status _isNewStatus;
        public Status _isVisitedStatus;
        public Status _inProgressStatus;
        public StatusMapper _mapper;
        public Status _testStatus; 
        public int statusTable; 


        [SetUp]
        public void SetUp()
        {
            _isNewStatus = Status.IsNew;
            _isVisitedStatus = Status.IsVisited;
            _inProgressStatus = Status.InProgress;
            _mapper = StatusMapper.GetInstance(); 
        }

        [Test]
        public void GetInstanceStatusMapperTest()
        {
            Assert.NotNull(StatusMapper.GetInstance());
        }

        [Test()]
        public void StatusTableToModelNew()
        {
            statusTable = 2;
            _testStatus = _mapper.StatusTableToModel(statusTable);
            Assert.AreEqual(_testStatus, _isNewStatus);
        }

        [Test()]
        public void StatusTableToModelVisited()
        {
            statusTable = 0;
            _testStatus=_mapper.StatusTableToModel(statusTable);
            Assert.AreEqual(_testStatus, _isVisitedStatus);

        }

        [Test()]
        public void StatusTableToModelProgress()
        {
            statusTable = 1;
            _testStatus = _mapper.StatusTableToModel(statusTable);
            Assert.AreEqual(_testStatus, _inProgressStatus);

        }

        [Test()]
        public void StatusModelToTableNew()
        {
            statusTable = _mapper.StatusModelToTable(_isNewStatus);
            Assert.AreEqual(statusTable, 2);
        }

        [Test()]
        public void StatusModelToTableVisited()
        {
            statusTable = _mapper.StatusModelToTable(_isVisitedStatus);
            Assert.AreEqual(statusTable, 0);

        }

        [Test()]
        public void StatusModelToTableProgress()
        {
            statusTable = _mapper.StatusModelToTable(_inProgressStatus);
            Assert.AreEqual(statusTable, 1);
        }
    }
}