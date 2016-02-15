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
			preBuildBeaconFoundNotification (context);
		}

		public BeaconFinder (Context context, StoryLine storyLine, Region customRegion){
			region = customRegion;
			this.storyLine = storyLine;
			constructBeaconManager (context);
			constructNotificationManager (context);
			preBuildBeaconFoundNotification (context);
		}

		private void constructBeaconManager(Context context){
			beaconManager = new BeaconManager (context);

			//add event handlers for ranging
			beaconManager.Ranging += beaconManagerRanging;
			//add event handlers for monitoring
			beaconManager.EnteredRegion += (sender, args) => beaconEnteredRegion(sender, args);
			beaconManager.ExitedRegion += (sender, args) => beaconExitedRegion(sender, args);
		}

		private void constructNotificationManager(Context context){
			notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
		}

		private void preBuildBeaconFoundNotification(Context context){

			Intent notifyIntent = new Intent(context, context.GetType());
			notifyIntent.SetFlags(ActivityFlags.SingleTop);

			PendingIntent pendingIntent = PendingIntent.GetActivities(context, 0, new[] { notifyIntent }, PendingIntentFlags.UpdateCurrent);

			buildingNotification = new NotificationCompat.Builder (context)
				.SetAutoCancel (true)                    // Dismiss from the notif. area when clicked
				.SetContentIntent (pendingIntent)  // Start 2nd activity when the intent is clicked.
				.SetSmallIcon(Resource.Drawable.beacon_gray)// Display this icon
				.SetContentTitle(storyLine.getName());
		}

		private Notification buildBeaconFoundNotification(string message){
			return buildingNotification.SetContentText (message).Build ();
		}
			
		private void beaconManagerRanging(object sender, BeaconManager.RangingEventArgs e)
		{
			if (e.Beacons == null)
			{
				return;
			}

			filterImmediateBeacons ((List<EstimoteSdk.Beacon>) e.Beacons);

			notifyObservers();
		}

		private void filterImmediateBeacons(List<EstimoteSdk.Beacon> beacons){
			beaconCount = beacons.Count;
			immediateBeacons = new SortedList<double, EstimoteSdk.Beacon>();

			Log.Debug("BeaconFinder", "Found {0} beacons.", beacons.Count);

			foreach (var beacon in beacons)
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

		public void addObserver(IBeaconFinderObserver observer){
			observers.AddLast (observer);
		}

		public void notifyObservers(){
			foreach (IBeaconFinderObserver observer in observers) {
				observer.beaconFinderObserverUpdate (this);
			}
		}

		public void sendBeaconFoundNotification(Notification notification){
			notification.Defaults |= NotificationDefaults.Lights;
			notification.Defaults |= NotificationDefaults.Sound;
			notificationManager.Notify(BeaconFoundNotificationId, notification);

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

		public void startMonitoring(){
			beaconManager.StartMonitoring (region);
		}

		public void stopMonitoring(){
			beaconManager.StopMonitoring (region);
			notificationManager.Cancel(BeaconFoundNotificationId);
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

		private void beaconEnteredRegion(object sender, BeaconManager.EnteredRegionEventArgs e){
			if (e.Beacons == null)
			{
				return;
			}

			filterImmediateBeacons ((List<EstimoteSdk.Beacon>) e.Beacons);

			POI poi = storyLine.findPOI (getClosestBeacon ());
			sendBeaconFoundNotification ( buildBeaconFoundNotification (poi.getDescription() ) );
			
		}

		private void beaconExitedRegion(object sender, BeaconManager.ExitedRegionEventArgs e){
			//
		}

	}
}

