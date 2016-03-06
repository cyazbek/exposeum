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
		private static BeaconFinder singletonInstance;

		private static readonly int BeaconFoundNotificationId = 1001;
		private NotificationManager notificationManager;
		private BeaconManager beaconManager;
		private EstimoteSdk.Region region;
		private bool isRanging;
		private bool isServiceReady;
		private const string TAG = "BeaconFinder";
		private int beaconCount;
		private LinkedList<IBeaconFinderObserver> observers = new LinkedList<IBeaconFinderObserver>();
		private SortedList<double, EstimoteSdk.Beacon> immediateBeacons;
		private StoryLine storyLine;
		private Context context;
		private Context notificationDestination;
		private bool inFocus;

		private BeaconFinder (Context context)
		{
			region = new EstimoteSdk.Region("rid", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
			constructBeaconManager (context);
			constructNotificationManager (context);
			this.context = context;
		}

		/// <summary>
		/// This method instantiate the instance of the BeaconFinder singleton
		/// </summary>
		public static void initInstance(Context context){
			if (singletonInstance == null)
				singletonInstance = new BeaconFinder (context);
		}

		/// <summary>
		/// This method returns the instance of the BeaconFinder singleton
		/// </summary>
		public static BeaconFinder getInstance(){
			return singletonInstance;
		}

		/// <summary>
		/// This method builds the beaconManager
		/// </summary>
		private void constructBeaconManager(Context context){
			beaconManager = new BeaconManager (context);

			//add event handlers for ranging
			beaconManager.Ranging += beaconManagerRanging;
		}

		/// <summary>
		/// This method builds the notification manager
		/// </summary>
		private void constructNotificationManager(Context context){
			notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
		}

		/// <summary>
		/// This method completes the building process of an android system notification for when a beacon is found.
		/// </summary>
		private Notification buildBeaconFoundNotification(string poiName, string poiDesc){

			Context destination = notificationDestination == null ? context : notificationDestination;

			Intent notifyIntent = new Intent(destination, destination.GetType());
			notifyIntent.SetFlags(ActivityFlags.SingleTop);

			PendingIntent pendingIntent = PendingIntent.GetActivities(context, 0, new[] { notifyIntent }, PendingIntentFlags.UpdateCurrent);

			NotificationCompat.Builder notifBuilder =  new NotificationCompat.Builder (context)
				.SetAutoCancel (true)                    // Dismiss from the notif. area when clicked
				.SetContentIntent (pendingIntent)  // Start 2nd activity when the intent is clicked.
				.SetSmallIcon(Resource.Drawable.logo_notif)// Display this icon
				.SetContentTitle(poiName)
				.SetContentText (poiDesc)
				.SetPriority(2)
				.SetVibrate(new long[] { 1000, 1000, 1000 });

			return notifBuilder.Build ();
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
				//If the app is not in focus, clear all potential notification associated with a beacon
				if(!inFocus)
					notificationManager.Cancel (BeaconFoundNotificationId);
				Log.Debug("BeaconFinder", "e.Beacons is null");
				return;
			}

			Log.Debug("BeaconFinder", e.Beacons.ToString());

			EstimoteSdk.Beacon previousClosestBeacon = getClosestBeacon ();

			filterImmediateBeacons (e.Beacons);

			EstimoteSdk.Beacon currentClosestBeacon = getClosestBeacon ();

			if (previousClosestBeacon != null && currentClosestBeacon != null && previousClosestBeacon.Major == currentClosestBeacon.Major &&
				previousClosestBeacon.Minor == currentClosestBeacon.Minor)
				return;

			if (inFocus)
				notifyObservers();

			if(!inFocus)
				notifyUser ();
				
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
				//if the beacon is not in the storyline, skip it and move to the next one
				if (!storyLine.hasBeacon (beacon))
					continue;

				var proximity = Utils.ComputeProximity(beacon);
				var accuracy = Utils.ComputeAccuracy(beacon);

				if (proximity == Utils.Proximity.Immediate) {
					try {
						immediateBeacons.Add (accuracy, beacon);
					} catch (System.ArgumentException ex) {
						Log.Debug("BeaconFind", "Two beacons with the same accuracy where found, displaying only one: " + ex.Message);
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

			if (storyLine == null) {
				throw new StoryLineNotFoundException ("You must set the storyline first. Call setStorylLine");
			}

			if(checkBluetooth() && !isServiceReady){
				//Connect ot the beacon service
				beaconManager.Connect(this);

			}else if(isServiceReady)
				beaconManager.StartRanging(region);
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
			Log.Debug("BeaconFinder", "stopping everything");
			stopRanging ();
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
		private void sendBeaconFoundNotification(Notification notification){
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

			if (immediateBeacons == null || immediateBeacons.Count == 0)
				return null;
			
			return immediateBeacons.Values[ 0 ];
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
		/// This method sets the current instance of the storyline.
		/// </summary>
		public void setStoryLine(StoryLine storyLine){
			this.storyLine = storyLine;
		}

		/// <summary>
		/// </summary>
		private void notifyUser(){

			Log.Debug("BeaconFinder", "Notifying user");

			//clear all previous notifications
			notificationManager.Cancel (BeaconFoundNotificationId);

			if (getClosestBeacon () != null) {
				PointOfInterest poi = storyLine.findPOI (getClosestBeacon ());
				if(!poi.Visited) //don't send a notification if the beacon has already been visited
					sendBeaconFoundNotification (buildBeaconFoundNotification (poi.getName (), poi.getDescription ()));
			} else {
				notificationManager.Cancel (BeaconFoundNotificationId);
			}
		}

		/// <summary>
		/// This method allows you to set the focus status of the app/activity
		/// </summary>
		public void setInFocus(bool focus){
			inFocus = focus;
		}

		/// <summary>
		/// This method allows you to get the focus status of the app/activity
		/// </summary>
		public bool getInFocus(){
			return inFocus;
		}

		/// <summary>
		/// This method allows you to set the region that the BeaconFinder will range
		/// </summary>
		public void setRegion(Region region){
			if (isRanging)
				throw new CantSetRegionException ("Cannot set set region since BeaconFinder is ranging. Stop ranging first, then set the region.");

			this.region = region;
		}

		/// <summary>
		/// This method allows you to get the region that the BeaconFinder ranges
		/// </summary>
		public Region getRegion(){
			return region;
		}

		/// <summary>
		/// This method allows you to get the the context to which a a user 
		/// will be directed after tapping the notification
		/// </summary>
		public Context getNotificationDestination(){
			return notificationDestination;
		}

		/// <summary>
		/// This method allows you to set the the context to which a a user 
		/// will be directed after tapping the notification
		/// </summary>
		public void setNotificationDestination(Context cont){
			notificationDestination = cont;
		}

	}
}