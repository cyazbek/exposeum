using System;
using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;
using EstimoteSdk;

namespace Exposeum.UITests
{
	[TestFixture]
	public class BeaconTests : IBeaconFinderObserver
	{
		public BeaconTests ()
		{
		}

		AndroidApp app;
		BeaconFinder beaconFinder;
		BeaconManager beaconManager;

		[SetUp]
		public void BeforeEachTest ()
		{
			app = ConfigureApp.Android.StartApp ();
		}

		[Test]
		public void testBeaconsAreProperlyOrderedByAccuracy ()
		{
	
		}

		public void testNoTwoBeaconsWithSameAccuracyExists(){
		}

		public void testAllImmediateBeaconInCollection(){
			
		}
	}
}

