using System;
using System.Reflection;
using NUnit.Framework;
using EstimoteSdk;
using Android.App;
using Exposeum;
using System.Collections.Generic;
using Exposeum.Models;
using Java.Util;
using System.Threading.Tasks;
using System.Threading;
using Android.Util;

namespace UnitTests
{
	[TestFixture]
	public class BeaconFinderTestsUS9 : IBeaconFinderObserver
	{

		BeaconFinder beaconFinder = BeaconFinder.getInstance();
		private PointOfInterest myPoi;
		private PointOfInterest myPoi1;
		private PointOfInterest myPoi2;
		private StoryLine story;
		private EstimoteSdk.Beacon beacon1;
		private EstimoteSdk.Beacon beacon2;
		private EstimoteSdk.Beacon beacon3;
		private EstimoteSdk.BeaconManager.RangingEventArgs rangingEvent;
		private IList<EstimoteSdk.Beacon> beaconList;
		private bool observersNotified;

		[SetUp]
		public void Setup ()
		{

			//Setup the storyline supplied to the BeaconFinder
			story = new StoryLine ();
			Exposeum.Models.Beacon beaconFiras = new Exposeum.Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982,54450);
			myPoi = new PointOfInterest(){ name_en = "Point Of interest Firas", name_fr = "Point d'interet Firas", description_en = "This is the point of interest Firas", description_fr = "Celui là est le premier point de Firas"};
			myPoi.beacon = beaconFiras;
			Exposeum.Models.Beacon beaconOli = new Exposeum.Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 55339, 19185);
			myPoi1 = new PointOfInterest { name_en = "Point Of interest Oli", name_fr = "Point d'interet Oli", description_en = "This is the point of interest Oli", description_fr = "Celui là est le premier point de Oli" };
			myPoi1.beacon = beaconOli;
			Exposeum.Models.Beacon beaconFar = new Exposeum.Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 65339, 69185);
			myPoi2 = new PointOfInterest { name_en = "Point Of interest FAR", name_fr = "Point d'interet FAR", description_en = "This is the point is far", description_fr = "Celui là est loin" };
			myPoi2.beacon = beaconFar;
			story.addPoi(myPoi);
			story.addPoi(myPoi1);

			//Setup the beaconFinder
			beaconFinder.setStoryLine (story);
			beaconFinder.setInFocus (true);
			beaconFinder.addObserver (this);

			//Create dummy beacons
			//This beacon's is the closest (immediate zone)
			beacon1 = new EstimoteSdk.Beacon (UUID.FromString ("b9407f30-f5f8-466e-aff9-25556b57fe6d"),
				"EST", EstimoteSdk.MacAddress.FromString("DA:FC:D4:B2:36:9E"), 13982, 54450, -100, -80);

			//This beacon's is the in immediate zone 
			beacon2 = new EstimoteSdk.Beacon (UUID.FromString ("b9407f30-f5f8-466e-aff9-25556b57fe6d"),
				"EST", EstimoteSdk.MacAddress.FromString("DA:FC:D4:B2:36:9F"), 55339, 19185, -90, -80);

			//This beacon's is the Far zone 
			beacon3 = new EstimoteSdk.Beacon (UUID.FromString ("b9407f30-f5f8-466e-aff9-25556b57fe6d"),
				"EST", EstimoteSdk.MacAddress.FromString("DA:FC:D4:B2:36:9F"), 65339, 69185, -50, -80);

			//create a list of dummy beacons to pass in the ranging event, add them not in order
			beaconList = new List<EstimoteSdk.Beacon> (3);
			beaconList.Add (beacon2);
			beaconList.Add (beacon1);
			beaconList.Add (beacon3);

			//Create a dummmy ranging event
			rangingEvent = new EstimoteSdk.BeaconManager.RangingEventArgs (beaconFinder.getRegion (), beaconList);

			//Observers have not yet been notified
			observersNotified = false;
		}


		[TearDown]
		public void Tear ()
		{
		}

		[Test]
		public void testUserNotifiedIfOutOfFocus(){

			//ensure that when in focus, the observer get notified
			Assert.False (observersNotified);
			triggerBeaconRanginEvent ();
			Assert.True (observersNotified);

			//make sure that the observer does not get notified, if the observers does not get notified, the user get notified
			observersNotified = false;
			beaconFinder.setInFocus (false);
			triggerBeaconRanginEvent ();
			Assert.False (observersNotified);


		}

		private void triggerBeaconRanginEvent(){
			//Using reflection to invoke beaconManagerRangingMethod and pass the dummy ranging event
			Type beaconFinderType = typeof(BeaconFinder);
			MethodInfo beaconManagerRangingMethod = beaconFinderType.GetMethod("beaconManagerRanging", BindingFlags.NonPublic | BindingFlags.Instance); 
			object[] mParam = new object[] {null , rangingEvent};
			beaconManagerRangingMethod.Invoke (beaconFinder, mParam);
		}

		public void beaconFinderObserverUpdate(IBeaconFinderObservable observable){
			observersNotified = true;
		}
        
	}
}

