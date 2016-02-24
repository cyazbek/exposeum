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
using Exposeum;
using Android.Test.Mock;


namespace UnitTests.US11
{
    [TestFixture]
    public class PointInterestVisitedTest 
    {

        private PointOfInterest pointOfInterest;

        [SetUp]
        public void Setup()
        {
            pointOfInterest = new PointOfInterest() { name_en = "Point Of interest", name_fr = "Point d'interet", description_en = "English description", description_fr = "Description Francaise" };
        }


        [TearDown]
        public void Tear()
        {
        }


        [Test]
        public void testPoiVisitedIsFalseUponInstantiation()
        {
            Assert.AreEqual(false, pointOfInterest.visited);
        }

        [Test]
        public void testPoiVisitedIsTrueOnceCorrespondingBeaconIsFound()
        {
            Context c = Android.App.Application.Context;
            MapView mapView = new MapView(c);
            BeaconFinder beaconFinder = BeaconFinder.getInstance();
            beaconFinder.addObserver(mapView);

            mapView.beaconFinderObserverUpdate(beaconFinder);

            Assert.AreEqual(true, pointOfInterest.visited);
        }

        [Test]
        public void Pass()
        {
            Assert.False(false);
        }

    }

}