using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using NUnit.Framework;
using Exposeum.Models;
using Exposeum.Controllers;
using Exposeum;
using Java.Util;
using System.Reflection;

namespace UnitTests.US_11
{
    class PointOfInterestVisitedTest
    {
        [TestFixture]
        public class PointInterestVisitedTest
        {
            private PointOfInterest pointOfInterestTest;
            private MapViewMock mapView;
            private Context context;
            private EstimoteSdk.Beacon beacon1;
            private EstimoteSdk.Beacon beacon2;
            private EstimoteSdk.Beacon beacon3;
            private EstimoteSdk.BeaconManager.RangingEventArgs rangingEvent;
            private IList<EstimoteSdk.Beacon> _beaconList;
            private BeaconFinder _beaconFinder = BeaconFinder.GetInstance();
            private MapControllerMock mapController;

            [SetUp]
            public void Setup()
            {
                pointOfInterestTest = new PointOfInterest();

                //Create dummy beacons
                //This beacon's is the closest (immediate zone)
                beacon1 = new EstimoteSdk.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"),
                    "EST", EstimoteSdk.MacAddress.FromString("DA:FC:D4:B2:36:9E"), 13982, 54450, -100, -80);

                //This beacon's is the in immediate zone 
                beacon2 = new EstimoteSdk.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"),
                    "EST", EstimoteSdk.MacAddress.FromString("DA:FC:D4:B2:36:9F"), 55339, 19185, -90, -80);

                //This beacon's is the Far zone 
                beacon3 = new EstimoteSdk.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"),
                    "EST", EstimoteSdk.MacAddress.FromString("DA:FC:D4:B2:36:9F"), 65339, 69185, -50, -80);

                context = Android.App.Application.Context;
                mapView = new MapViewMock(context);

                mapController = mapView.getController();

                _beaconFinder.AddObserver(mapController);
                _beaconFinder.SetInFocus(true);
            }

            [TearDown]
            public void Tear()
            {
            }

            [Test]
            public void testPoiVisitedIsFalseUponInstantiation()
            {
                Assert.AreEqual(false, pointOfInterestTest.Visited);
            }

            [Test]
            public void testPoiVisitedIsTrueOnceCorrespondingBeaconIsFound()
            {
                //create a list of dummy beacons to pass in the ranging event, add them not in order
                _beaconList = new List<EstimoteSdk.Beacon>(3);
                _beaconList.Add(beacon1);
                _beaconList.Add(beacon2);
                _beaconList.Add(beacon3);

                //Create a dummmy ranging event
                rangingEvent = new EstimoteSdk.BeaconManager.RangingEventArgs(_beaconFinder.GetRegion(), _beaconList);
                triggerBeaconRanginEvent();
                
                Beacon beaconModel = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
                pointOfInterestTest = mapController.Model.getCurrentStoryline.PoiList.Where(poi => poi.Beacon.Equals(beaconModel)).FirstOrDefault();

                Assert.True(pointOfInterestTest.Visited);
            }

            [Test]
            public void testPoiVisitedIsFalseIfBeaconIsNotFound()
            {
                //create a list of dummy beacons to pass in the ranging event, add them not in order
                _beaconList = new List<EstimoteSdk.Beacon>(2);
                _beaconList.Add(beacon3);
                _beaconList.Add(beacon2);


                //Create a dummmy ranging event
                rangingEvent = new EstimoteSdk.BeaconManager.RangingEventArgs(_beaconFinder.GetRegion(), _beaconList);
                triggerBeaconRanginEvent();

                Beacon beaconModel = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
                pointOfInterestTest = mapController.Model.getCurrentStoryline.PoiList.Where(poi => poi.Beacon.Equals(beaconModel)).FirstOrDefault();

                Assert.False(pointOfInterestTest.Visited);
            }

            [Test]
            public void Pass()
            {
                Assert.False(false);
            }

            private void triggerBeaconRanginEvent()
            {
                //Using reflection to invoke beaconManagerRangingMethod and pass the dummy ranging event
                Type beaconFinderType = typeof(BeaconFinder);
                MethodInfo beaconManagerRangingMethod = beaconFinderType.GetMethod("beaconManagerRanging", BindingFlags.NonPublic | BindingFlags.Instance);
                object[] mParam = new object[] { null, rangingEvent };
                beaconManagerRangingMethod.Invoke(_beaconFinder, mParam);
            }


        }

        // Mock classes 

            public class MapViewMock: View
            {
                private Context _context;
                private Map _map;
                private MapControllerMock _controller;

                public MapViewMock(Context context) : base(context)
                {
                    _context = context;
                    _controller = new MapControllerMock(this);
                    _map = _controller.Model;
                }

                public MapControllerMock getController()
                {
                    return _controller;
                }
        }

            public class MapMock : Map
            {
                private StoryLine _currentStoryLine;

                public MapMock()
                {
                    _currentStoryLine = new StoryLine();

                    Beacon beacon1 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
                    Beacon beacon2 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 49800, 5890);
                    Beacon beacon3 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 55339, 19185);

                    PointOfInterest p1 = new PointOfInterest(0.53f, 0.46f);
                    p1.Beacon = beacon1;

                    PointOfInterest p2 = new PointOfInterest(0.53f, 0.46f);
                    p2.Beacon = beacon2;

                    PointOfInterest p3 = new PointOfInterest(0.53f, 0.46f);
                    p3.Beacon = beacon3;

                    _currentStoryLine.AddPoi(p1);
                    _currentStoryLine.AddPoi(p2);
                    _currentStoryLine.AddPoi(p3);
            }

                public StoryLine getCurrentStoryline
                {
                    get { return _currentStoryLine; }
                    set { _currentStoryLine = value; }
                }
            }

            public class MapControllerMock : MapController, IBeaconFinderObserver
            {
                private MapMock _model;
                private MapViewMock _view;
                private BeaconFinder _beaconFinder = BeaconFinder.GetInstance();

                public MapControllerMock(MapViewMock view) : base(null)
                {
                    _view = view;
                    _model = new MapMock();
                    _beaconFinder.SetStoryLine(_model.CurrentStoryline);
                }
            

                 public new void beaconFinderObserverUpdate(IBeaconFinderObservable observable)
                {
                    BeaconFinder beaconFinder = (BeaconFinder)observable;
                    EstimoteSdk.Beacon beacon = beaconFinder.GetClosestBeacon();

                    if (beacon != null && (_model.getCurrentStoryline.HasBeacon(beacon)))
                    {
                        PointOfInterest poi = _model.getCurrentStoryline.FindPoi(beacon);
                        poi.Visited = true;
                        _model.getCurrentStoryline.SetLastPointOfInterestVisited(poi);
                    }
                }

                public new MapMock Model
                {
                    get { return _model; }
                }

            }
    }
}