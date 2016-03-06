using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NUnit.Framework;
using Exposeum.Models;
using Exposeum.Views;
using Exposeum.Controllers;
using Exposeum;
using Android.Test.Mock;
using Java.Util;
using System.Reflection;

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

                _mockStoryLine.addPoi(mockPointOfInterest1);
                _mockStoryLine.addPoi(mockPointOfInterest2);
                _mockStoryLine.addPoi(mockPointOfInterest3);
                _mockStoryLine.addPoi(mockPointOfInterest4);

                _mockStoryLine.SetLastPointOfInterestVisited(mockPointOfInterest3);
                _mockStoryLine.currentStatus = Status.inProgress;

            }
       

            [Test]
            public void TestStorylineIsInProgress()
            {
                _status = Status.inProgress;
                
                Assert.AreEqual(_status, _mockStoryLine.currentStatus);
            }

            [Test]
            public void TestStorylineIsNewAfterReset()
            {
                StorylineController.GetInstance().ResetStorylineProgress(_mockStoryLine);

                Assert.AreEqual(Status.isNew, _mockStoryLine.currentStatus);
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