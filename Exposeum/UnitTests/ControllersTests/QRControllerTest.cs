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
			Context context = Android.App.Application.Context;
			Assert.IsNotNull(QRController.GetInstance(context));
        }
    }
}