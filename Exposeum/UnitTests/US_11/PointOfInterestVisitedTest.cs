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

namespace UnitTests.US_11
{
    class PointOfInterestVisitedTest
    {
        [TestFixture]
        public class PointInterestVisitedTest
        {
            PointOfInterest pointOfInterestTest;

            [SetUp]
            public void Setup()
            {
                pointOfInterestTest = new PointOfInterest();
            }

            [TearDown]
            public void Tear()
            {
            }

            [Test]
            public void testPoiVisitedIsFalseUponInstantiation()
            {
                Assert.AreEqual(false, pointOfInterestTest.visited);
            }

            [Test]
            public void testPoiVisitedIsTrueOnceCorrespondingBeaconIsFound()
            {
                Context c = Android.App.Application.Context;
                MapView mapView = new MapView(c);
                MapController mapController = mapView.getController();
                BeaconFinder beaconFinder = BeaconFinder.getInstance();

                Beacon beacon1 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
                pointOfInterestTest = mapController.Model.CurrentStoryline.poiList.Where(poi => poi.beacon.Equals(beacon1)).FirstOrDefault();

                mapController.beaconFinderObserverUpdate(beaconFinder);

                Assert.True(pointOfInterestTest.visited);
            }

            [Test]
            public void Pass()
            {
                Assert.False(false);
            }

        }
    }
}