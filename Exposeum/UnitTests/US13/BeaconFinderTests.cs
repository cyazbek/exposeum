using System;
using NUnit.Framework;
using EstimoteSdk;
using Android.App;

namespace UnitTests
{
	[TestFixture]
	public class BeaconFinderTests
	{
		[SetUp]
		public void Setup ()
		{
			
			BeaconManager beaconManager = new BeaconManager(Application.Context);
		}


		[TearDown]
		public void Tear ()
		{
		}

		[Test]
		public void Pass ()
		{
			Console.WriteLine ("test1");
			Assert.True (true);
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
	}
}

