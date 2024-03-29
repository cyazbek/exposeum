﻿using System.Reflection;

using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;

namespace UnitTests
{
	[Activity (Label = "UnitTests", MainLauncher = true)]
	public class MainActivity : TestSuiteActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			// tests can be inside the main assembly
			AddTest (Assembly.GetExecutingAssembly ());
			AddTest (typeof (BeaconFinderTests).Assembly);
			// or in any reference assemblies
			// AddTest (typeof (Your.Library.TestClass).Assembly);

			// Once you called base.OnCreate(), you cannot add more assemblies.
			base.OnCreate (bundle);
		}
	}
}

