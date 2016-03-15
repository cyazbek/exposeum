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
            Beacon beaconFiras = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982,54450);
            _myPoi = new PointOfInterest(){ NameEn = "Point Of interest Firas", NameFr = "Point d'interet Firas", DescriptionEn = "This is the point of interest Firas", DescriptionFr = "Celui là est le premier point de Firas"};
            _myPoi.Beacon = beaconFiras;
            Beacon beaconOli = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 55339, 19185);
            _myPoi1 = new PointOfInterest { NameEn = "Point Of interest Oli", NameFr = "Point d'interet Oli", DescriptionEn = "This is the point of interest Oli", DescriptionFr = "Celui là est le premier point de Oli" };
            _myPoi1.Beacon = beaconOli;
            _story.AddPoi(_myPoi);
            _story.AddPoi(_myPoi1);



            //define UI bindings
            _beaconContextualText = FindViewById<TextView>(Resource.Id.textView1);

			_beaconFinder = BeaconFinder.GetInstance ();
			_beaconFinder.SetNotificationDestination (this);
			_beaconFinder.SetStoryLine (_story);
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

		protected override void OnDestroy()
		{
			base.OnDestroy();
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

