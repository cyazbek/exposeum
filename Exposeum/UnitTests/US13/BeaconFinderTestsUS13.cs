using System;
using System.Reflection;
using NUnit.Framework;
using EstimoteSdk;
using Exposeum;
using System.Collections.Generic;
using Exposeum.Models;
using Java.Util;
using Android.App;

namespace UnitTests
{
	[TestFixture]
	public class BeaconFinderTestsUS13 : IBeaconFinderObserver
	{

		BeaconFinder beaconFinder;
		private PointOfInterest myPoi;
		private PointOfInterest myPoi1;
		private PointOfInterest myPoi2;
		private StoryLine story;
		private EstimoteSdk.Beacon beacon1;
		private EstimoteSdk.Beacon beacon2;
		private EstimoteSdk.Beacon beacon3;
		private BeaconManager.RangingEventArgs rangingEvent;
		private BeaconManager.RangingEventArgs nullRangingEvent;
		private IList<EstimoteSdk.Beacon> beaconList;
		private IList<EstimoteSdk.Beacon> nullBeaconList;
		private bool observersNotified;

		[SetUp]
		public void Setup ()
		{
			BeaconFinder.InitInstance (Android.App.Application.Context);
			beaconFinder = BeaconFinder.GetInstance();

			//Setup the storyline supplied to the BeaconFinder
			story = new StoryLine ();
			Exposeum.Models.Beacon beaconFiras = new Exposeum.Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982,54450);
			myPoi = new PointOfInterest(){ NameEn = "Point Of interest Firas", NameFr = "Point d'interet Firas", DescriptionEn = "This is the point of interest Firas", DescriptionFr = "Celui là est le premier point de Firas"};
			myPoi.Beacon = beaconFiras;
			Exposeum.Models.Beacon beaconOli = new Exposeum.Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 55339, 19185);
			myPoi1 = new PointOfInterest { NameEn = "Point Of interest Oli", NameFr = "Point d'interet Oli", DescriptionEn = "This is the point of interest Oli", DescriptionFr = "Celui là est le premier point de Oli" };
			myPoi1.Beacon = beaconOli;
			Exposeum.Models.Beacon beaconFar = new Exposeum.Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 65339, 69185);
			myPoi2 = new PointOfInterest { NameEn = "Point Of interest FAR", NameFr = "Point d'interet FAR", DescriptionEn = "This is the point is far", DescriptionFr = "Celui là est loin" };
			myPoi2.Beacon = beaconFar;
			story.AddPoi(myPoi);
			story.AddPoi(myPoi1);

			//Setup the beaconFinder
			beaconFinder.SetPath (story);
			beaconFinder.SetInFocus (true);
			beaconFinder.AddObserver(this);

			//Create dummy beacons
			//This beacon's is the closest (immediate zone)
			beacon1 = new EstimoteSdk.Beacon (UUID.FromString ("b9407f30-f5f8-466e-aff9-25556b57fe6d"),
				"EST", MacAddress.FromString("DA:FC:D4:B2:36:9E"), 13982, 54450, -100, -80);

			//This beacon's is the in immediate zone 
			beacon2 = new EstimoteSdk.Beacon (UUID.FromString ("b9407f30-f5f8-466e-aff9-25556b57fe6d"),
				"EST", MacAddress.FromString("DA:FC:D4:B2:36:9F"), 55339, 19185, -90, -80);

			//This beacon's is the Far zone 
			beacon3 = new EstimoteSdk.Beacon (UUID.FromString ("b9407f30-f5f8-466e-aff9-25556b57fe6d"),
				"EST", MacAddress.FromString("DA:FC:D4:B2:36:9F"), 65339, 69185, -50, -80);

			//create a list of dummy beacons to pass in the ranging event, add them not in order
			beaconList = new List<EstimoteSdk.Beacon> (3);
			beaconList.Add (beacon2);
			beaconList.Add (beacon1);
			beaconList.Add (beacon3);

			nullBeaconList = new List<EstimoteSdk.Beacon> (1);
			nullBeaconList.Add (beacon3);

			//Create a dummmy ranging event
			rangingEvent = new BeaconManager.RangingEventArgs (beaconFinder.GetRegion (), beaconList);
			nullRangingEvent = new BeaconManager.RangingEventArgs (beaconFinder.GetRegion (), nullBeaconList);

			//Observers have not yet been notified
			observersNotified = false;
		}


		[TearDown]
		public void Tear ()
		{
		}


		[Test]
		public void testImmediateBeaconOrderedByAccuracy ()
		{
			triggerBeaconRanginEvent (rangingEvent);

			//Test that the beacons are properly ordered by accuracy
			KeyValuePair<double, EstimoteSdk.Beacon> previousPair = new KeyValuePair<double, EstimoteSdk.Beacon>();
			SortedList<double, EstimoteSdk.Beacon> immediateBeacons = beaconFinder.GetImmediateBeacons ();

			//make sure we have beacons on which we can run the test
			if(immediateBeacons.Count == 0)
				Assert.Inconclusive ("No beacons received.");

			int j = 0;
			foreach (KeyValuePair<double, EstimoteSdk.Beacon> pair in immediateBeacons) {

				if (j == 0) {
					previousPair = pair;
					j++;
					continue;
				}

				//Assert that the previous key and accuracy is smaller (better accuracy) than the current
				Assert.True (pair.Key >  previousPair.Key);
				Assert.True (Utils.ComputeAccuracy (pair.Value) >  Utils.ComputeAccuracy (previousPair.Value));
				j++;

				previousPair = pair;
			}
		}

		[Test]
		public void testFarBeaconIsFilteredOut ()
		{
			triggerBeaconRanginEvent (rangingEvent);

			//Test that the beacons are properly ordered by accuracy
			KeyValuePair<double, EstimoteSdk.Beacon> previousPair = new KeyValuePair<double, EstimoteSdk.Beacon>();
			SortedList<double, EstimoteSdk.Beacon> immediateBeacons = beaconFinder.GetImmediateBeacons ();

			//make sure we have beacons on which we can run the test
			if(immediateBeacons.Count == 0)
				Assert.Inconclusive ("No beacons received.");

			//check that beacon (the far beacon) was filtered out
			foreach (KeyValuePair<double, EstimoteSdk.Beacon> pair in immediateBeacons) {

				//Assert that the major and minor of beacon 3 is not found in the list
				Assert.True (pair.Value.Major !=  beacon3.Major);
				Assert.True (pair.Value.Minor !=  beacon3.Minor);

			}
		}

		[Test]
		public void testObserversAreNotified(){
			beaconFinder.SetInFocus (true);
			//observer should not be notified if no event occured
			Assert.False (observersNotified);
			//observer should not be notified if the previous beacon and curreng beacon are the same
			triggerBeaconRanginEvent (rangingEvent);
			triggerBeaconRanginEvent (rangingEvent);
			Assert.False (observersNotified);

			//observer should  be notified if the previous beacon and curreng beacon are different
			triggerBeaconRanginEvent (nullRangingEvent);
			triggerBeaconRanginEvent (rangingEvent);
			Assert.True (observersNotified);
		}

		//[Test]
		public void testUserNotifiedIfOutOfFocus(){

			//ensure that when in focus, the observer get notified
			Assert.False (observersNotified);
			//observer should  be notified if the previous beacon and curreng beacon are different
			triggerBeaconRanginEvent (nullRangingEvent);
			triggerBeaconRanginEvent (rangingEvent);
			Assert.True (observersNotified);

			//make sure that the observer does not get notified, if the observers does not get notified, the user get notified
			observersNotified = false;
			beaconFinder.SetInFocus (false);
			//observer should  be notified if the previous beacon and curreng beacon are different
			triggerBeaconRanginEvent (nullRangingEvent);
			triggerBeaconRanginEvent (rangingEvent);
			Assert.False (observersNotified);
		}

		[Test]
		public void testTheClosestBeaconIsTheClosest(){

			triggerBeaconRanginEvent (rangingEvent);

			EstimoteSdk.Beacon closestBeacon = beaconFinder.GetClosestBeacon ();

			Assert.True (closestBeacon.Major == beacon1.Major);
			Assert.True (closestBeacon.Minor == beacon1.Minor);
		}

		private void triggerBeaconRanginEvent(BeaconManager.RangingEventArgs e){
			//Using reflection to invoke beaconManagerRangingMethod and pass the dummy ranging event
			Type beaconFinderType = typeof(BeaconFinder);
			MethodInfo beaconManagerRangingMethod = beaconFinderType.GetMethod("BeaconManagerRanging", BindingFlags.NonPublic | BindingFlags.Instance); 
			object[] mParam = new object[] {null , e};
			beaconManagerRangingMethod.Invoke (beaconFinder, mParam);
		}

		public void BeaconFinderObserverUpdate(IBeaconFinderObservable observable){
			observersNotified = true;
		}
        
	}
}

