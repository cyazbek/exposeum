using Android.App;
using Android.OS;
using Android.Widget;
using Exposeum.Models;
using Java.Util;
 


namespace Exposeum
{
	[Activity(Label = "@string/beacon_activity")]	
	public class BeaconActivity : Activity, IBeaconFinderObserver
	{
		private TextView _beaconContextualText;
		private BeaconFinder _beaconFinder;
        private PointOfInterest _myPoi;
        private PointOfInterest _myPoi1;
		private StoryLine _story;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Beacon);

			_story = new StoryLine ();




            //define UI bindings
            _beaconContextualText = FindViewById<TextView>(Resource.Id.textView1);

			_beaconFinder = BeaconFinder.GetInstance ();
			_beaconFinder.SetNotificationDestination (this);
			_beaconFinder.SetPath (_story);
			_beaconFinder.AddObserver (this);
			_beaconFinder.FindBeacons ();
		}

		protected override void OnResume()
		{
			base.OnResume();
			_beaconFinder.SetInFocus (true);
 
        }

		protected override void OnPause()
		{
			base.OnPause();
			_beaconFinder.SetInFocus (false);
		}

		public void BeaconFinderObserverUpdate(IBeaconFinderObservable observable){

			BeaconFinder beaconFinder = (BeaconFinder)observable;

			string text1 = "There is " + beaconFinder.GetBeaconCount () + " beacon(s) close to you\n\n";

			text1 = text1 + "There is " + beaconFinder.GetImmediateBeacons().Count + " beacon(s) immediate to you\n\n";

            EstimoteSdk.Beacon beacon = beaconFinder.GetClosestBeacon();

			text1 = text1 + "The Closest beacon is: \n";
            if(beacon!=null)
            {
                if (_story.HasBeacon(beacon))
                {
                    PointOfInterest poi = _story.FindPoi(beacon);
                    text1 = text1 + "Beacon UUID: " + beacon.ProximityUUID + "\nName: " + poi.GetName() + "\nDescription: " + poi.GetDescription() + "\nMajor: " +beacon.Major +"\nMinor: " + beacon.Minor + "\n\n";
                }
                if(!_story.HasBeacon(beacon))
                    text1 = text1 + "Beacon UUID: " + beacon.ProximityUUID + "\nMajor: " + beacon.Major + "\nMinor: " + beacon.Minor +" " + _story.GetSize()+"\n\n" ;
            }
			_beaconContextualText.Text = text1 ;


		}

	}
}

