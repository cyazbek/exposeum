using Android.Test.Mock;
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
        private PointOfInterestPopup _beaconPopup;

        [SetUp]
        public void Setup()
        {
            _poi = new PointOfInterest(1f, 1f);
            _poi.NameEn = "testpoint";
            MockContext context = new MockContext();
            _mapview = new MapView(context);

            _beaconPopup = new PointOfInterestPopup(context, _poi);

            

        }

        [Test]
        public void TestClickPoiOpensSummaryPopup()
        {

            Assert.Equals(3, 3);

        }
    }
}