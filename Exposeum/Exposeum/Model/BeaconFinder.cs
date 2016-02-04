using System;
using EstimoteSdk;
using Android.Util;
using Android.OS;
using Android.Content.PM;
using JavaObject = Java.Lang.Object;
using Android.Content;
using System.Collections.Generic; 

namespace Exposeum
{
	public class BeaconFinder: JavaObject, BeaconManager.IServiceReadyCallback, IBeaconFinderObservable
	{
		private BeaconManager beaconManager;
		private readonly EstimoteSdk.Region region;
		private bool beaconInRegion;
		private bool isRanging;
		private const string TAG = "BeaconFinder";
		private int beaconCount;
		private LinkedList<IBeaconFinderObserver> observers = new LinkedList<IBeaconFinderObserver>();
		private SortedList<double, Beacon> immediateBeacons;

		public BeaconFinder (Context context)
		{
			region = new EstimoteSdk.Region("rid", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
			beaconManager = new BeaconManager (context);

			//add event handlers
			beaconManager.Ranging += beaconManagerRanging;
			beaconManager.EnteredRegion += (sender, args) => setBeaconInRegion(true);
			beaconManager.ExitedRegion += (sender, args) => setBeaconInRegion(false);
		}

		public BeaconFinder (Context context, Region customRegion){
			region = customRegion;
			beaconManager = new BeaconManager (context);

			//add event handlers
			beaconManager.Ranging += beaconManagerRanging;
			beaconManager.EnteredRegion += (sender, args) => setBeaconInRegion(true);
			beaconManager.ExitedRegion += (sender, args) => setBeaconInRegion(false);
		}


		private void beaconManagerRanging(object sender, BeaconManager.RangingEventArgs e)
		{
			if (e.Beacons == null)
			{
				return;
			}

			beaconCount = e.Beacons.Count;
			immediateBeacons = new SortedList<double, Beacon>();

			Log.Debug("BeaconActivity", "Found {0} beacons.", e.Beacons.Count);

			foreach (var beacon in e.Beacons)
			{

				var proximity = Utils.ComputeProximity(beacon);
				var accuracy = Utils.ComputeAccuracy(beacon);

				if (proximity == Utils.Proximity.Immediate) {
					try {
						immediateBeacons.Add (Utils.ComputeAccuracy (beacon), beacon);
					} catch (System.ArgumentException ex) {

					}
				}

			}

			notifyObservers();
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

		public void findBeacons(){
			if(checkBluetooth())
				//Connect ot the beacon service
				beaconManager.Connect(this);
		}

		public void stop(){
			if (isRanging)
				//Disconnect ot the beacon service
				beaconManager.Disconnect ();
		}

		private bool checkBluetooth()
		{
			//Validation checks
			if (!beaconManager.HasBluetooth || !beaconManager.IsBluetoothEnabled 
				|| !beaconManager.CheckPermissionsAndService())
				return false;
			

			return true;

		}

		private void setBeaconInRegion(bool inRegion){
			beaconInRegion = inRegion;
		}

		public void addObserver(IBeaconFinderObserver observer){
			observers.AddLast (observer);
		}

		public void notifyObservers(){
			foreach (IBeaconFinderObserver observer in observers) {
				observer.beaconFinderObserverUpdate (this);
			}
		}

		public int getBeaconCount(){
			return beaconCount;
		}

		public SortedList<double, Beacon> getImmediateBeacons(){
			return immediateBeacons;
		}

		public Beacon getClosestBeacon(){

			if (immediateBeacons.Count == 0)
				return null;
			
			return immediateBeacons.Values[ immediateBeacons.Count - 1 ];
		}

	}
}

