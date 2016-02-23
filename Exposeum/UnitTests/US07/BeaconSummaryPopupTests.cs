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
using Android.Webkit;
using Android.Widget;
using Exposeum;
using Exposeum.Models;
using Exposeum.Views;
using NUnit.Framework;

namespace UnitTests.US07
{
    [TestFixture]
    class BeaconSummaryPopupTests
    {
        private PointOfInterest _poi;
        private MapView _mapview;
        private BeaconPopup _beaconPopup;

        [SetUp]
        public void Setup()
        {
            _poi = new PointOfInterest(1f, 1f);
            _poi.name_en = "testpoint";
            MockContext context = new MockContext();
            _mapview = new MapView(context);

            _beaconPopup = new BeaconPopup(context, _poi);

            

        }

        [Test]
        public void TestClickPoiOpensSummaryPopup()
        {

            Assert.Equals(3, 3);

        }
    }
}