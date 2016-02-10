using System;
using NUnit.Framework;
using EstimoteSdk;
using Android.App;
using Exposeum;
using System.Collections.Generic;

namespace UnitTests
{
	[TestFixture]
	public class BeaconFinderTests : IBeaconFinderObserver
	{

		BeaconFinder beaconFinder = new BeaconFinder (Application.Context);

		[SetUp]
		public void Setup ()
		{
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

			KeyValuePair<double, Beacon> previousPair;
			int j = 0;
			foreach (KeyValuePair<double, Beacon> pair in beaconFinder.getImmediateBeacons ()) {

				if (j == 0) {
					previousPair = pair;
					continue;
				}

				Assert.True (pair.Key <  previousPair.Key);
				Assert.True (Utils.ComputeAccuracy (pair.Value) <  Utils.ComputeAccuracy (previousPair.Value));
				j++;

				previousPair = pair;
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

