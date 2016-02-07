using Android.App;
using Android.OS;
using Android.Widget;
using EstimoteSdk;
using Exposeum.Models;
using Java.Util;
 


namespace Exposeum
{
	[Activity(Label = "@string/beacon_activity")]	
	public class BeaconActivity : Activity, IBeaconFinderObserver
	{

		private TextView beaconContextualText;
		private BeaconFinder beaconFinder;
        public POI myPoi;
        //public POI myPoi2;
        //public POI myPoi3;
        //public StoryLine story = new StoryLine();
        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//View v = new BeaconView(Resource.Layout.Beacon);
			SetContentView (Resource.Layout.Beacon);
            Models.Beacon beacon = new Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982,54450); 
            myPoi = new POI { name_en = "Point Of interest one", name_fr = "Point d'interet un", dscription_en = "This is the point of interest one", dscription_fr = "Celui là est le premier point d'interet"};
            myPoi.beacon = beacon; 
            //myPoi2 = new POI { name_en = "Point Of interest two", name_fr = "Point d'interet deux", dscription_en = "This is the point of interest two", dscription_fr = "Celui là est le deuxieme point d'interet", beaconId = UUID.FromString("b9407f30-f4f2-466e-aff9-25556b57fe6d") };
            //myPoi3 = new POI { name_en = "Point Of interest three", name_fr = "Point d'interet trois", dscription_en = "This is the point of interest three", dscription_fr = "Celui là est le troisième point d'interet", beaconId = UUID.FromString("b9408z20-f4f2-466e-aff9-25556b57fe6d") };
            //story.addPOI(myPoi1);
            //story.addPOI(myPoi2);
            //story.addPOI(myPoi3);
            
            //define UI bindings
            beaconContextualText = FindViewById<TextView>(Resource.Id.textView1);

			beaconFinder = new BeaconFinder(this);
			beaconFinder.addObserver (this);
		}

		protected override void OnResume()
		{
			base.OnResume();
			beaconFinder.findBeacons ();
			beaconFinder.stopMonitoring ();
		}

		protected override void OnPause()
		{
			beaconFinder.stop ();
			base.OnPause();
		}

		protected override void OnDestroy()
		{
			beaconFinder.stop ();
			base.OnDestroy();
		}

		public void beaconFinderObserverUpdate(IBeaconFinderObservable observable){

			BeaconFinder beaconFinder = (BeaconFinder)observable;

			string text1 = "There is " + beaconFinder.getBeaconCount () + " beacon(s) close to you\n\n";

			text1 = text1 + "There is " + beaconFinder.getImmediateBeacons().Count + " beacon(s) immediate to you\n\n";

            EstimoteSdk.Beacon beacon = beaconFinder.getClosestBeacon();

			text1 = text1 + "The Closest beacon is: \n";
            if(beacon!=null)
            {
                Models.Beacon foundBeacon = new Models.Beacon(beacon.ProximityUUID, beacon.Major, beacon.Major);
                if (foundBeacon.compareBeacon(myPoi.beacon))
                {
                    text1 = text1 + "Beacon UUID: " + beacon.ProximityUUID + "\nName: " + myPoi.getName() + "\nDescription: " + myPoi.getDescription() + "\nMajor: " +beacon.Major +"\nMinor: " + beacon.Minor + "\n\n";
                }
                if(!foundBeacon.compareBeacon(myPoi.beacon))
                    text1 = text1 + "Beacon UUID: " + beacon.ProximityUUID + "\nMajor: " + beacon.Major + "\nMinor: " + beacon.Minor + "\n\n";
            }
			beaconContextualText.Text = text1 ;


		}

	}
}

