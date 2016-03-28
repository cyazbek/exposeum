using NUnit.Framework;
using Exposeum.Models;
using Exposeum.Controllers;

namespace UnitTests.US_8
{
    class StopResetStorylineTest
    {
        [TestFixture]
        public class StopResetStoryLineTest
        {
            private StoryLine _mockStoryLine;
            private Status _status;

            [SetUp]
            public void Setup()
            {
                _mockStoryLine = new StoryLine();

                PointOfInterest mockPointOfInterest1 = new PointOfInterest(0f, 0f);
                PointOfInterest mockPointOfInterest2 = new PointOfInterest(0f, 0f);
                PointOfInterest mockPointOfInterest3 = new PointOfInterest(0f, 0f);
                PointOfInterest mockPointOfInterest4 = new PointOfInterest(0f, 0f);

                mockPointOfInterest1.Visited = true;
                mockPointOfInterest2.Visited = true;
                mockPointOfInterest3.Visited = true;
                mockPointOfInterest4.Visited = false;

                _mockStoryLine.AddPoi(mockPointOfInterest1);
                _mockStoryLine.AddPoi(mockPointOfInterest2);
                _mockStoryLine.AddPoi(mockPointOfInterest3);
                _mockStoryLine.AddPoi(mockPointOfInterest4);

                _mockStoryLine.SetLastPointOfInterestVisited(mockPointOfInterest3);
                _mockStoryLine.CurrentStatus = Status.InProgress;

            }
       

            [Test]
            public void TestStorylineIsInProgress()
            {
                _status = Status.InProgress;
                
                Assert.AreEqual(_status, _mockStoryLine.CurrentStatus);
            }

            [Test]
            public void TestStorylineIsNewAfterReset()
            {
                StorylineController.GetInstance().ResetStorylineProgress(_mockStoryLine);

                Assert.AreEqual(Status.IsNew, _mockStoryLine.CurrentStatus);
            }

            [Test]
            public void TestLastVisitedPoiIsNullAfterReset()
            {
                StorylineController.GetInstance().ResetStorylineProgress(_mockStoryLine);
                Assert.IsNull(_mockStoryLine.GetLastVisitedPointOfInterest());

            }
        }           
    }
}