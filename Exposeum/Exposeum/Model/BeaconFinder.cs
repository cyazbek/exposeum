using System;
using EstimoteSdk;
using Android.Util;
using Android.OS;
using Android.Content.PM;

namespace Exposeum
{
	public class BeaconFinder: BeaconManager.IServiceReadyCallback
	{

		private BeaconManager beaconManager;
		private readonly EstimoteSdk.Region region;
		private bool beaconInRegion;
		private bool isRanging;
		private const string TAG = "BeaconFinder";

		public BeaconFinder ()
		{
			region = new EstimoteSdk.Region("rid", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
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
			if (region == null)
			{
				throw new Exception("beacon reagion is null");
			}
			try
			{
				beaconManager.StartRanging(region);
				Log.Debug(TAG, "Looking for beacons in the region.");
				isRanging = true;

			}
			catch (RemoteException e)
			{
				isRanging = false;
				Log.Error(TAG, "Cannot start ranging, {0}", e);
			}

		}

		public void FindBeacons(){

			if(checkBluetooth())
				beaconManager.Connect(this);
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
				return false;
			}
			else if (!beaconManager.CheckPermissionsAndService())
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		private void setBeaconInRegion(bool inRegion){
			beaconInRegion = inRegion;
		}

	}
}

