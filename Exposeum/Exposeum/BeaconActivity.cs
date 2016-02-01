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
using EstimoteSdk;
using Java.Util.Concurrent;
using Android.Util;
using Android.Content.PM;
using Android.Bluetooth;


namespace Exposeum
{
	[Activity(Label = "@string/beacon_activity")]	
	//in the future beacon discovery should probably be handled by a service or the Application android class
	public class BeaconActivity : Activity
	{

		private TextView beaconContextualText;
		private BeaconFinder beaconFinder;


		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//View v = new BeaconView(Resource.Layout.Beacon);
			SetContentView (Resource.Layout.Beacon);

			//define UI bindings
			beaconContextualText = FindViewById<TextView>(Resource.Id.textView1);

			beaconFinder = new BeaconFinder();



		}

		protected override void OnResume()
		{
			base.OnResume();

		}

		protected override void OnPause()
		{


			base.OnPause();
		}

		protected override void OnDestroy()
		{

			base.OnDestroy();
		}

	}
}

