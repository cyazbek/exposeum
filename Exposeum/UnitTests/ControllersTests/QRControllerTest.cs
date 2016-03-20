using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Test.Mock;
using Android.Views;
using Android.Widget;
using Exposeum;
using Exposeum.Controllers;
using NUnit.Framework;

namespace UnitTests.ControllersTests
{
    [TestFixture]
    class QRControllerTest
    {
        private Application app;

        [SetUp]
        public void Setup()
        {
            //app = new MockApplication();
        }

        [TearDown]
        public void Tear()
        {
        }

        [Test]
        public void getInstanceTest()
        {
            //Assert.IsNotNull(QRController.GetInstance(app.ApplicationContext));
        }
    }
}