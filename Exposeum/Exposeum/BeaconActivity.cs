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
	public class BeaconActivity : Activity, BeaconManager.IServiceReadyCallback
	{

		const int REQUEST_ENABLE_BLUETOOTH = 123321;
		private BeaconManager beaconManager;
		private EstimoteSdk.Region region;
		private bool beaconInRegion;
		bool beaconsEnabled = false;
		private TextView beaconContextualText;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//View v = new BeaconView(Resource.Layout.Beacon);
			SetContentView (Resource.Layout.Beacon);

			beaconContextualText = FindViewById<TextView>(Resource.Id.textView1);


			beaconManager = new BeaconManager (this);

			//add event handler for when ranging
			beaconManager.Ranging += BeaconManagerRanging;



			beaconManager.EnteredRegion += (sender, args) => setBeaconInRegion(true);

			beaconManager.ExitedRegion += (sender, args) => setBeaconInRegion(false);

			Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
			StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BLUETOOTH);
		}


		private void BeaconManagerRanging(object sender, BeaconManager.RangingEventArgs e)
		{


			if (e.Beacons == null)
			{
				return;
			}

			bool close = false;
			foreach (var beacon in e.Beacons)
			{
				var proximity = Utils.ComputeProximity(beacon);

				if (proximity != Utils.Proximity.Unknown)
					close = true;

				if (proximity != Utils.Proximity.Immediate)
					continue;

				var accuracy = Utils.ComputeAccuracy(beacon);
				if (accuracy > .06)
					continue;

				Log.Debug("BeaconActivity", "Found {0} beacons.", e.Beacons.Count);

				beaconContextualText.Text = "Found " + e.Beacons.Count + "beacons.";

			}
		}

		public void OnServiceReady()
		{
			if (region != null)
			{
				beaconManager.StopRanging(region);
				region = null;
			}

			region = new EstimoteSdk.Region("rid", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
			beaconManager.StartRanging(region);
			beaconsEnabled = true;
		}

		protected override void OnResume()
		{
			base.OnResume();
			CheckBluetooth();

			//connect to 
			if (region != null && beaconsEnabled)
				beaconManager.Connect(this);
		}

		protected override void OnPause()
		{
			base.OnPause();
			if (beaconsEnabled)
				beaconManager.Disconnect();
		}

		protected override void OnDestroy()
		{
			// Make sure we disconnect from the Estimote.
			if (beaconsEnabled)
				beaconManager.Disconnect();
			base.OnDestroy();
		}

		public void setBeaconInRegion(bool inRegion){
			beaconInRegion = inRegion;
		}

		private void CheckBluetooth()
		{
			beaconContextualText.Visibility = ViewStates.Gone;
			//Validation checks
			if (!beaconManager.HasBluetooth || !PackageManager.HasSystemFeature(PackageManager.FeatureBluetoothLe))
			{
				beaconsEnabled = false;
			}
			else if (!beaconManager.IsBluetoothEnabled)
			{
				//bluetooth is not enabled
				beaconContextualText.Text = "You must enable Bluetooth";
				beaconsEnabled = false;
			}
			else if (!beaconManager.CheckPermissionsAndService())
			{
				beaconContextualText.Text = "There is an issues with bluetooth permission, make sure exposeum is authorized to use the Bluetooth.";
				beaconsEnabled = false;
			}
			else
			{
				beaconsEnabled = true;
				beaconContextualText.Visibility = ViewStates.Visible;
			}
		}

	}
}

