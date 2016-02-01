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

		private BeaconManager beaconManager;
		private readonly EstimoteSdk.Region REGION = new EstimoteSdk.Region("rid", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
		private bool beaconInRegion;
		private TextView beaconContextualText;
		private bool isRanging;
		private readonly string CLASSNAME = typeof(BeaconActivity).Name;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//View v = new BeaconView(Resource.Layout.Beacon);
			SetContentView (Resource.Layout.Beacon);

			//define UI bindings
			beaconContextualText = FindViewById<TextView>(Resource.Id.textView1);


			beaconManager = new BeaconManager (this);

			//add event handlers
			beaconManager.Ranging += BeaconManagerRanging;
			beaconManager.EnteredRegion += (sender, args) => setBeaconInRegion(true);
			beaconManager.ExitedRegion += (sender, args) => setBeaconInRegion(false);


		}


		private void BeaconManagerRanging(object sender, BeaconManager.RangingEventArgs e)
		{


			if (e.Beacons == null)
			{
				return;
			}

			Log.Debug("BeaconActivity", "Found {0} beacons.", e.Beacons.Count);
			beaconContextualText.Text = "Found " + e.Beacons.Count + " beacon(s).";

			/*bool close = false;
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
			}*/
		}

		public void OnServiceReady()
		{
			if (REGION == null)
			{
				throw new Exception("beacon reagion is null");
			}
			try
			{
				if(checkBluetooth()){
					beaconManager.StartRanging(REGION);
					Log.Debug(CLASSNAME, "Looking for beacons in the region.");
					isRanging = true;
				}
			}
			catch (RemoteException e)
			{
				isRanging = false;
				Log.Error(CLASSNAME, "Cannot start ranging, {0}", e);
			}
	
		}

		protected override void OnResume()
		{
			base.OnResume();
			//CheckBluetooth();

			//connect to the beacon service
			beaconManager.Connect(this);
		}

		protected override void OnPause()
		{
			// Make sure we disconnect from the beacon service.
			if (isRanging)
				beaconManager.Disconnect();
			base.OnPause();
		}

		protected override void OnDestroy()
		{
			// Make sure we disconnect from the beacon service.
			if (isRanging)
				beaconManager.Disconnect();
			base.OnDestroy();
		}

		public void setBeaconInRegion(bool inRegion){
			beaconInRegion = inRegion;
		}

		private bool checkBluetooth()
		{
			//Validation checks
			if (!beaconManager.HasBluetooth || !PackageManager.HasSystemFeature(PackageManager.FeatureBluetoothLe))
			{
				return false;
			}
			else if (!beaconManager.IsBluetoothEnabled)
			{
				//bluetooth is not enabled
				beaconContextualText.Text = "You must enable Bluetooth";
				return false;
			}
			else if (!beaconManager.CheckPermissionsAndService())
			{
				beaconContextualText.Text = "There is an issues with bluetooth permission, make sure exposeum is authorized to use the Bluetooth.";
				return false;
			}
			else
			{
				return true;
			}
		}

	}
}

