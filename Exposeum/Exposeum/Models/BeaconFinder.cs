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
using Java.Util.Concurrent;

namespace Exposeum
{
	public class BeaconFinder: JavaObject, BeaconManager.IServiceReadyCallback, IBeaconFinderObservable
	{
		private static readonly int BeaconFoundNotificationId = 1001;
		private NotificationManager notificationManager;
		private BeaconManager beaconManager;
		private readonly EstimoteSdk.Region region;
		private bool isRanging;
		private bool isServiceReady;
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

			// Default values are 5s of scanning and 25s of waiting time to save CPU cycles.
			// In order for this demo to be more responsive and immediate we lower down those values.
			beaconManager.SetBackgroundScanPeriod(TimeUnit.Seconds.ToMillis(1), 0);
		}

		private void constructNotificationManager(Context context){
			notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
		}

		/// <summary>
		/// This method initiate the building process of an android notification but does not complete it. You must
		/// call buildBeaconFoundNotification() to complete the building process.
		/// </summary>
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

		/// <summary>
		/// This method completes the building process of an android system notification for when a beacon is found.
		/// The notification building must have already been started by preBuildBeaconFoundNotification()
		/// </summary>
		private Notification buildBeaconFoundNotification(string message){
			if (buildingNotification == null)
				throw new Exception ("No notification currently in the building process. " +
					"Call preBuildBeaconFoundNotification before calling this method");
			return buildingNotification.SetContentText (message).Build ();
		}

		/// <summary>
		/// This method handles ranging events. When a beacon is found using ranging this method is called and
		/// the event along with the sender is passed. This method will filter the beacons and notify the
		/// registered observers
		/// </summary>
		private void beaconManagerRanging(object sender, BeaconManager.RangingEventArgs e)
		{
			if (e.Beacons == null)
			{
				return;
			}

			filterImmediateBeacons (e.Beacons);

			notifyObservers();
		}

		/// <summary>
		/// This method will filter am IList of beacons based on their proximity. It will only keep immediate beacons
		/// and will put them in a sorted list, ordered by the accurary of their distance estimation.
		/// </summary>
		private void filterImmediateBeacons(IList<EstimoteSdk.Beacon> beacons){
			beaconCount = beacons.Count;
			immediateBeacons = new SortedList<double, EstimoteSdk.Beacon>();

			Log.Debug("BeaconFinder", "Found {0} beacons.", beacons.Count);

			foreach (var beacon in beacons)
			{

				var proximity = Utils.ComputeProximity(beacon);
				var accuracy = Utils.ComputeAccuracy(beacon);

				if (proximity == Utils.Proximity.Immediate) {
					try {
						immediateBeacons.Add (accuracy, beacon);
					} catch (System.ArgumentException ex) {
						Log.Debug("BeaconFind", "Two beacons with the same accuracy where found, displaying only one: ", ex.Message);
					}
				}

			}
		}

		/// <summary>
		/// This method is an implementation of the OnServiceReady() method of the BeaconManager.IServiceReadyCallback interface.
		/// It is required for the IBeacons to work.
		/// </summary>
		public void OnServiceReady()
		{
			if (region == null)
			{
				throw new Exception("beacon reagion is null");
			}
			try
			{
				isServiceReady = true;
				beaconManager.StartRanging(region);
				isRanging = true;
				Log.Debug(TAG, "Looking for beacons in the region.");
			}
			catch (RemoteException e)
			{
				isRanging = false;
				isServiceReady = false;
				Log.Error(TAG, "Cannot start ranging, {0}", e);
			}

		}

		/// <summary>
		/// This method lauch the Estimote Ibeacon finding service and connects our current instance to it.
		/// </summary>
		public void findBeacons(){
			if(checkBluetooth() && !isServiceReady){
				//Connect ot the beacon service
				beaconManager.Connect(this);

			}
		}

		/// <summary>
		/// This method stops BeaconFinder from Ranging
		/// </summary>
		public void stopRanging(){
			if (isRanging && isServiceReady) {
				//Disconnect ot the beacon service
				beaconManager.StopRanging (region);
			}
				
		}

		/// <summary>
		/// This method stops BeaconFinder from Ranging, Monitoring and disconnnects the current instance from the 
		/// Estimote Ibeacon finding service
		/// </summary>
		public void stopBeaconFinder(){
			stopRanging ();
			stopMonitoring ();
			beaconManager.Disconnect ();
		}

		/// <summary>
		/// This method checks the current bluetooth status
		/// </summary>
		private bool checkBluetooth()
		{
			//Validation checks
			if (!beaconManager.HasBluetooth || !beaconManager.IsBluetoothEnabled 
				|| !beaconManager.CheckPermissionsAndService())
				return false;

			return true;

		}

		/// <summary>
		/// This method allows IBeaconFinderObservers to register with BeaconFinder.
		/// </summary>
		public void addObserver(IBeaconFinderObserver observer){
			observers.AddLast (observer);
		}

		/// <summary>
		/// This method notifies IBeaconFinderObservers of an event.
		/// </summary>
		public void notifyObservers(){
			foreach (IBeaconFinderObserver observer in observers) {
				observer.beaconFinderObserverUpdate (this);
			}
		}

		/// <summary>
		/// This method sends a previously built Android Notification to the android system.
		/// </summary>
		public void sendBeaconFoundNotification(Notification notification){
			notification.Defaults |= NotificationDefaults.Lights;
			notification.Defaults |= NotificationDefaults.Sound;
			notificationManager.Notify(BeaconFoundNotificationId, notification);
			Log.Debug("BeaconFinder", "Sending notification");

		}

		/// <summary>
		/// This method returns the total (unfiltered) count of the beacons detected by the phone.
		/// </summary>
		public int getBeaconCount(){
			return beaconCount;
		}

		/// <summary>
		/// This method returns a sorted list of beacons found in the immediate vicinity of the phone.
		/// </summary>
		public SortedList<double, EstimoteSdk.Beacon> getImmediateBeacons(){
			return immediateBeacons;
		}

		/// <summary>
		/// This method returns the beacon closest to the phone.
		/// </summary>
		public EstimoteSdk.Beacon getClosestBeacon(){
			if (immediateBeacons.Count == 0)
				return null;
			
			return immediateBeacons.Values[ immediateBeacons.Count - 1 ];
		}

		/// <summary>
		/// This method starts the monitoring process of beacons.
		/// </summary>
		public void startMonitoring(){
			Log.Debug("BeaconFinder", "starting to monitor");
			beaconManager.StartMonitoring (region);
		}

		/// <summary>
		/// This method stops the monitoring process of beacons.
		/// </summary>
		public void stopMonitoring(){
			Log.Debug("BeaconFinder", "stopping monitoring");
			beaconManager.StopMonitoring (region);
			notificationManager.Cancel(BeaconFoundNotificationId);
		}

		/// <summary>
		/// This method returns the current instance of the BeaconManager.
		/// </summary>
		public BeaconManager getBeaconManager(){
			return beaconManager;
		}

		/// <summary>
		/// This method returns the current instance of the sotryline.
		/// </summary>
		public StoryLine getStoryLine(){
			return storyLine;
		}

		/// <summary>
		/// This method sets the current instance of the sotryline.
		/// </summary>
		public void setStoryLine(StoryLine storyLine){
			this.storyLine = storyLine;
		}

		/// <summary>
		/// This method handles events where beacons entered the monitored region. When a beacon is found using monitoring this method is called and
		/// the event along with the sender is passed. This method will filter the beacons and send a 
		/// notification to the phone.
		/// </summary>
		private void beaconEnteredRegion(object sender, BeaconManager.EnteredRegionEventArgs e){
			if (e.Beacons == null)
			{
				return;
			}

			filterImmediateBeacons (e.Beacons);

			Log.Debug("BeaconFinder", "Monitoring...");

			if(getClosestBeacon () != null){
				POI poi = storyLine.findPOI (getClosestBeacon ());
				sendBeaconFoundNotification ( buildBeaconFoundNotification (poi.getDescription() ) );
				Log.Debug("BeaconFinder", "closest beacon not null");
			}
		}

		/// <summary>
		/// This method handles events where beacons exited the monitored region. When a beacon is lost using monitoring this method is called and
		/// the event along with the sender is passed.
		/// </summary>
		private void beaconExitedRegion(object sender, BeaconManager.ExitedRegionEventArgs e){
			//
		}

	}
}

