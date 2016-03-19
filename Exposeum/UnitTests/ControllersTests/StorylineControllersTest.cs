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
using Exposeum.Controllers;
using NUnit.Framework;

namespace UnitTests.ControllersTests
{
    [TestFixture]
    class StorylineControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void Tear()
        {
        }

        [Test]
        public void getInstanceTest()
        {
            Assert.IsNotNull(StorylineController.GetInstance());
        }

    }
}