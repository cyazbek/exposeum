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
                Beacon beacon1 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
                pointOfInterestTest = new PointOfInterest(0.53f, 0.46f);
                pointOfInterestTest.beacon  = beacon1;
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

                Setup();

                Context c = Android.App.Application.Context;
                MapView mapView = new MapView(c);
                MapController mapController = mapView.getController();
                BeaconFinder beaconFinder = BeaconFinder.getInstance();
                mapController.Model.CurrentStoryline.addPoi(pointOfInterestTest);

                mapController.beaconFinderObserverUpdate(beaconFinder);

                Assert.AreEqual(true, pointOfInterestTest.visited);
            }

            [Test]
            public void Pass()
            {
                Assert.False(false);
            }

        }
    }
}