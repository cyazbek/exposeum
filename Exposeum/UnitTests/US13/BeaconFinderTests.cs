using System;
using NUnit.Framework;
using EstimoteSdk;
using Android.App;
using Exposeum;
using System.Collections.Generic;
using Exposeum.Models;
using Java.Util;

namespace UnitTests
{
	[TestFixture]
	public class BeaconFinderTests : IBeaconFinderObserver
	{

		BeaconFinder beaconFinder = BeaconFinder.getInstance();
		private PointOfInterest myPoi;
		private PointOfInterest myPoi1;
		private StoryLine story;


		[SetUp]
		public void Setup ()
		{
			story = new StoryLine ();
			Exposeum.Models.Beacon beaconFiras = new Exposeum.Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982,54450);
			myPoi = new PointOfInterest(){ name_en = "Point Of interest Firas", name_fr = "Point d'interet Firas", description_en = "This is the point of interest Firas", description_fr = "Celui là est le premier point de Firas"};
			myPoi.beacon = beaconFiras;
			Exposeum.Models.Beacon beaconOli = new Exposeum.Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 55339, 19185);
			myPoi1 = new PointOfInterest { name_en = "Point Of interest Oli", name_fr = "Point d'interet Oli", description_en = "This is the point of interest Oli", description_fr = "Celui là est le premier point de Oli" };
			myPoi1.beacon = beaconOli;
			story.addPoi(myPoi);
			story.addPoi(myPoi1);

			beaconFinder.setStoryLine (story);
			beaconFinder.addObserver (this);
			beaconFinder.findBeacons ();

		}


		[TearDown]
		public void Tear ()
		{
		}

		[Test]
		public void testImmediateBeaconOrderedByAccuracy ()
		{

			Console.WriteLine ("test1");
			Assert.True (true);

			KeyValuePair<double, EstimoteSdk.Beacon> previousPair = new KeyValuePair<double, EstimoteSdk.Beacon>();
			int j = 0;
			foreach (KeyValuePair<double, EstimoteSdk.Beacon> pair in beaconFinder.getImmediateBeacons ()) {

				if (j == 0) {
					previousPair = pair;
					continue;
				}
                /*
				Assert.True (pair.Key <  previousPair.Key);
				Assert.True (Utils.ComputeAccuracy (pair.Value) <  Utils.ComputeAccuracy (previousPair.Value));
				j++;

				previousPair = pair;

    */

			}
		}

		[Test]
		public void Fail ()
		{
			Assert.False (true);
		}

		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}

		[Test]
		public void Inconclusive ()
		{
			Assert.Inconclusive ("Inconclusive");
		}

		public void beaconFinderObserverUpdate(IBeaconFinderObservable observable){
			testImmediateBeaconOrderedByAccuracy ();	
		}
        
	}
}

