using System;
using EstimoteSdk;
using Android.Util;
using Android.OS;
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
		private static BeaconFinder _singletonInstance;

		private static readonly int BeaconFoundNotificationId = 1001;
		private NotificationManager _notificationManager;
		private BeaconManager _beaconManager;
		private Region _region;
		private bool _isRanging;
		private bool _isServiceReady;
		private const string Tag = "BeaconFinder";
		private int _beaconCount;
		private LinkedList<IBeaconFinderObserver> _observers = new LinkedList<IBeaconFinderObserver>();
		private SortedList<double, EstimoteSdk.Beacon> _immediateBeacons;
		private IPath _path;
		private Context _context;
		private Context _notificationDestination;
		private bool _inFocus;

		private BeaconFinder (Context context)
		{
			_region = new Region("rid", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
			ConstructBeaconManager (context);
			ConstructNotificationManager (context);
			_context = context;
		}

		/// <summary>
		/// This method instantiate the instance of the BeaconFinder singleton
		/// </summary>
		public static void InitInstance(Context context){
			if (_singletonInstance == null)
				_singletonInstance = new BeaconFinder (context);
		}

		/// <summary>
		/// This method returns the instance of the BeaconFinder singleton
		/// </summary>
		public static BeaconFinder GetInstance(){
			return _singletonInstance;
		}

		/// <summary>
		/// This method builds the beaconManager
		/// </summary>
		private void ConstructBeaconManager(Context context){
			_beaconManager = new BeaconManager (context);

			//add event handlers for ranging
			_beaconManager.Ranging += BeaconManagerRanging;
		}

		/// <summary>
		/// This method builds the notification manager
		/// </summary>
		private void ConstructNotificationManager(Context context){
			_notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
		}

		/// <summary>
		/// This method completes the building process of an android system notification for when a beacon is found.
		/// </summary>
		private Notification BuildBeaconFoundNotification(string poiName, string poiDesc){

			Context destination = _notificationDestination ?? _context;

			Intent notifyIntent = new Intent(destination, destination.GetType());
			notifyIntent.SetFlags(ActivityFlags.SingleTop);

			PendingIntent pendingIntent = PendingIntent.GetActivities(_context, 0, new[] { notifyIntent }, PendingIntentFlags.UpdateCurrent);

			NotificationCompat.Builder notifBuilder =  new NotificationCompat.Builder (_context)
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
		private void BeaconManagerRanging(object sender, BeaconManager.RangingEventArgs e)
		{
			if (e.Beacons == null)
			{
				//If the app is not in focus, clear all potential notification associated with a beacon
				if(!_inFocus)
					_notificationManager.Cancel (BeaconFoundNotificationId);
				Log.Debug("BeaconFinder", "e.Beacons is null");
				return;
			}

			Log.Debug("BeaconFinder", e.Beacons.ToString());

			EstimoteSdk.Beacon previousClosestBeacon = GetClosestBeacon ();

			FilterImmediateBeacons (e.Beacons);

			EstimoteSdk.Beacon currentClosestBeacon = GetClosestBeacon ();

			if (previousClosestBeacon != null && currentClosestBeacon != null && previousClosestBeacon.Major == currentClosestBeacon.Major &&
				previousClosestBeacon.Minor == currentClosestBeacon.Minor)
				return;

			if (_inFocus)
				NotifyObservers();

			if(!_inFocus)
				NotifyUser ();
				
		}

		/// <summary>
		/// This method will filter am IList of beacons based on their proximity. It will only keep immediate beacons
		/// and will put them in a sorted list, ordered by the accurary of their distance estimation.
		/// </summary>
		private void FilterImmediateBeacons(IList<EstimoteSdk.Beacon> beacons){
			_beaconCount = beacons.Count;
			_immediateBeacons = new SortedList<double, EstimoteSdk.Beacon>();

			Log.Debug("BeaconFinder", "Found {0} beacons.", beacons.Count);

			foreach (var beacon in beacons)
			{
				//if the beacon is not in the path, skip it and move to the next one
				if (!_path.HasBeacon (beacon))
					continue;

				var proximity = Utils.ComputeProximity(beacon);
				var accuracy = Utils.ComputeAccuracy(beacon);

				if (proximity == Utils.Proximity.Immediate) {
					try {
						_immediateBeacons.Add (accuracy, beacon);
					} catch (ArgumentException ex) {
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
			if (_region == null)
			{
				throw new Exception("beacon reagion is null");
			}
			try
			{
				_isServiceReady = true;
				_beaconManager.StartRanging(_region);
				_isRanging = true;
				Log.Debug(Tag, "Looking for beacons in the region.");
			}
			catch (RemoteException e)
			{
				_isRanging = false;
				_isServiceReady = false;
				Log.Error(Tag, "Cannot start ranging, {0}", e);
			}

		}

		/// <summary>
		/// This method lauch the Estimote Ibeacon finding service and connects our current instance to it.
		/// </summary>
		public void FindBeacons(){

			if (_path == null) {
				throw new PathNotFoundException ("You must set the Path first. Call setPath.");
			}

			if(CheckBluetooth() && !_isServiceReady){
				//Connect ot the beacon service
				_beaconManager.Connect(this);

			}else if(_isServiceReady)
				_beaconManager.StartRanging(_region);
		}

		/// <summary>
		/// This method stops BeaconFinder from Ranging
		/// </summary>
		public void StopRanging(){
			if (_isRanging && _isServiceReady) {
				//Disconnect ot the beacon service
				_beaconManager.StopRanging (_region);
			}
		}

		/// <summary>
		/// This method stops BeaconFinder from Ranging, Monitoring and disconnnects the current instance from the 
		/// Estimote Ibeacon finding service
		/// </summary>
		public void StopBeaconFinder(){
			Log.Debug("BeaconFinder", "stopping everything");
			StopRanging ();
			_beaconManager.Disconnect ();
		}

		/// <summary>
		/// This method checks the current bluetooth status
		/// </summary>
		private bool CheckBluetooth()
		{
			//Validation checks
			if (!_beaconManager.HasBluetooth || !_beaconManager.IsBluetoothEnabled 
				|| !_beaconManager.CheckPermissionsAndService())
				return false;

			return true;
		}

		/// <summary>
		/// This method allows IBeaconFinderObservers to register with BeaconFinder.
		/// </summary>
		public void AddObserver(IBeaconFinderObserver observer){
			_observers.AddLast (observer);
		}

		/// <summary>
		/// This method allows de-register observers.
		/// </summary>
		public void RemoveObserver(IBeaconFinderObserver observer){
			_observers.Remove (observer);
		}

		/// <summary>
		/// This method notifies IBeaconFinderObservers of an event.
		/// </summary>
		public void NotifyObservers(){
			foreach (IBeaconFinderObserver observer in _observers) {
				observer.BeaconFinderObserverUpdate (this);
			}
		}

		/// <summary>
		/// This method sends a previously built Android Notification to the android system.
		/// </summary>
		private void SendBeaconFoundNotification(Notification notification){
			notification.Defaults |= NotificationDefaults.Lights;
			notification.Defaults |= NotificationDefaults.Sound;
			_notificationManager.Notify(BeaconFoundNotificationId, notification);

			Log.Debug("BeaconFinder", "Sending notification");

		}

		/// <summary>
		/// This method returns the total (unfiltered) count of the beacons detected by the phone.
		/// </summary>
		public int GetBeaconCount(){
			return _beaconCount;
		}

		/// <summary>
		/// This method returns a sorted list of beacons found in the immediate vicinity of the phone.
		/// </summary>
		public SortedList<double, EstimoteSdk.Beacon> GetImmediateBeacons(){
			return _immediateBeacons;
		}

		/// <summary>
		/// This method returns the beacon closest to the phone.
		/// </summary>
		public EstimoteSdk.Beacon GetClosestBeacon(){

			if (_immediateBeacons == null || _immediateBeacons.Count == 0)
				return null;
			
			return _immediateBeacons.Values[ 0 ];
		}

		/// <summary>
		/// This method returns the current instance of the BeaconManager.
		/// </summary>
		public BeaconManager GetBeaconManager(){
			return _beaconManager;
		}

		/// <summary>
		/// This method returns the current instance of the sotryline.
		/// </summary>
		public IPath GetPath(){
			return _path;
		}

		/// <summary>
		/// This method sets the current instance of the path.
		/// </summary>
		public void SetPath(IPath path){
			_path = path;
		}

		/// <summary>
		/// </summary>
		private void NotifyUser(){

			Log.Debug("BeaconFinder", "Notifying user");

			//clear all previous notifications
			_notificationManager.Cancel (BeaconFoundNotificationId);

			if (GetClosestBeacon () != null) {
				PointOfInterest poi = _path.FindPoi (GetClosestBeacon ());
				if(!poi.Visited) //don't send a notification if the beacon has already been visited
					SendBeaconFoundNotification (BuildBeaconFoundNotification (poi.GetName (), poi.GetDescription ()));
			} else {
				_notificationManager.Cancel (BeaconFoundNotificationId);
			}
		}

		/// <summary>
		/// This method allows you to set the focus status of the app/activity
		/// </summary>
		public void SetInFocus(bool focus){
			_inFocus = focus;
		}

		/// <summary>
		/// This method allows you to get the focus status of the app/activity
		/// </summary>
		public bool GetInFocus(){
			return _inFocus;
		}

		/// <summary>
		/// This method allows you to set the region that the BeaconFinder will range
		/// </summary>
		public void SetRegion(Region region){
			if (_isRanging)
				throw new CantSetRegionException ("Cannot set set region since BeaconFinder is ranging. Stop ranging first, then set the region.");

			_region = region;
		}

		/// <summary>
		/// This method allows you to get the region that the BeaconFinder ranges
		/// </summary>
		public Region GetRegion(){
			return _region;
		}

		/// <summary>
		/// This method allows you to get the the context to which a a user 
		/// will be directed after tapping the notification
		/// </summary>
		public Context GetNotificationDestination(){
			return _notificationDestination;
		}

		/// <summary>
		/// This method allows you to set the the context to which a a user 
		/// will be directed after tapping the notification
		/// </summary>
		public void SetNotificationDestination(Context cont){
			_notificationDestination = cont;
		}

	}
}