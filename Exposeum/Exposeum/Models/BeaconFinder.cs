using System;
using EstimoteSdk;
using Android.Util;
using Android.OS;
using Android.Content.PM;
using JavaObject = Java.Lang.Object;
using Android.Content;
using System.Collections.Generic;
using Android.App;
using Android.Support.V4.App;
using Exposeum.Models;

namespace Exposeum
{
	public class BeaconFinder: JavaObject, BeaconManager.IServiceReadyCallback, IBeaconFinderObservable
	{
		private static readonly int BeaconFoundNotificationId = 1001;
		private NotificationManager notificationManager;
		private BeaconManager beaconManager;
		private readonly EstimoteSdk.Region region;
		private bool beaconInRegion;
		private bool isRanging;
		private const string TAG = "BeaconFinder";
		private int beaconCount;
		private LinkedList<IBeaconFinderObserver> observers = new LinkedList<IBeaconFinderObserver>();
		private SortedList<double, EstimoteSdk.Beacon> immediateBeacons;
		private NotificationCompat.Builder buildingNotification;
		private StoryLine storyLine;

		public BeaconFinder (Context context, StoryLine storyLine)
		{
			region = new EstimoteSdk.Region("rid", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
			this.storyLine = storyLine;
			constructBeaconManager (context);
			constructNotificationManager (context);
			preBuildAndroidNotification (context);
		}

		public BeaconFinder (Context context, Region customRegion){
			region = customRegion;
			this.storyLine = storyLine;
			constructBeaconManager (context);
			constructNotificationManager (context);
			preBuildAndroidNotification (context);
		}

		private void constructBeaconManager(Context context){
			beaconManager = new BeaconManager (context);

			//add event handlers
			beaconManager.Ranging += beaconManagerRanging;
			beaconManager.EnteredRegion += (sender, args) => setBeaconInRegion(true);
			beaconManager.ExitedRegion += (sender, args) => setBeaconInRegion(false);
		}

		private void constructNotificationManager(Context context){
			notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
		}

		private void preBuildAndroidNotification(Context context){

			Intent notifyIntent = new Intent(context, context.GetType());
			notifyIntent.SetFlags(ActivityFlags.SingleTop);

			PendingIntent pendingIntent = PendingIntent.GetActivities(context, 0, new[] { notifyIntent }, PendingIntentFlags.UpdateCurrent);

			buildingNotification = new NotificationCompat.Builder (context)
				.SetAutoCancel (true)                    // Dismiss from the notif. area when clicked
				.SetContentIntent (pendingIntent)  // Start 2nd activity when the intent is clicked.
				.SetSmallIcon(Resource.Drawable.beacon_gray)// Display this icon
				.SetContentTitle(storyLine.getName);
		}

		private Notification buildAndroidNotification(string message){
			return buildingNotification.SetContentText (message).Build ();
		}
			
		private void beaconManagerRanging(object sender, BeaconManager.RangingEventArgs e)
		{
			if (e.Beacons == null)
			{
				return;
			}

			beaconCount = e.Beacons.Count;
			immediateBeacons = new SortedList<double, EstimoteSdk.Beacon>();

			Log.Debug("BeaconFinder", "Found {0} beacons.", e.Beacons.Count);

			foreach (var beacon in e.Beacons)
			{

				var proximity = Utils.ComputeProximity(beacon);
				var accuracy = Utils.ComputeAccuracy(beacon);

				if (proximity == Utils.Proximity.Immediate) {
					try {
						immediateBeacons.Add (Utils.ComputeAccuracy (beacon), beacon);
					} catch (System.ArgumentException ex) {
						Log.Debug("BeaconFind", "Two beacons with the same accuracy where found, displaying only one: ", ex.Message);
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

		public bool getBeaconInRegion(){
			return beaconInRegion;
		}

		public void addObserver(IBeaconFinderObserver observer){
			observers.AddLast (observer);
		}

		public void notifyObservers(){
			foreach (IBeaconFinderObserver observer in observers) {
				observer.beaconFinderObserverUpdate (this);
			}
		}

		public void sendAndroidNotification(){
		}

		public int getBeaconCount(){
			return beaconCount;
		}

		public SortedList<double, EstimoteSdk.Beacon> getImmediateBeacons(){
			return immediateBeacons;
		}

		public EstimoteSdk.Beacon getClosestBeacon(){
			if (immediateBeacons.Count == 0)
				return null;
			
			return immediateBeacons.Values[ immediateBeacons.Count - 1 ];
		}

		public void startMonitoring(BeaconManager.IMonitoringListener listener){
			beaconManager.SetMonitoringListener (listener);
			beaconManager.StartMonitoring (region);
		}

		public void stopMonitoring(){
			beaconManager.StopMonitoring (region);
		}

		public BeaconManager getBeaconManager(){
			return beaconManager;
		}

		public StoryLine getStoryLine(){
			return storyLine;
		}

		public void setStoryLine(StoryLine storyLine){
			this.storyLine = storyLine;
		}

	}
}

