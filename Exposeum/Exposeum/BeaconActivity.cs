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
        public POI myPoi1;
        StoryLine story = new StoryLine();
        //public POI myPoi2;
        //public POI myPoi3;
        //public StoryLine story = new StoryLine();
        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//View v = new BeaconView(Resource.Layout.Beacon);
			SetContentView (Resource.Layout.Beacon);
            Models.Beacon beaconFiras = new Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982,54450); 
            myPoi = new POI { name_en = "Point Of interest Firas", name_fr = "Point d'interet Firas", dscription_en = "This is the point of interest Firas", dscription_fr = "Celui là est le premier point de Firas"};
            myPoi.beacon = beaconFiras;
            Models.Beacon beaconOli = new Models.Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 55339, 19185);
            myPoi1 = new POI { name_en = "Point Of interest Oli", name_fr = "Point d'interet Oli", dscription_en = "This is the point of interest Oli", dscription_fr = "Celui là est le premier point de Oli" };
            myPoi1.beacon = beaconOli;
            story.addPoi(myPoi);
            story.addPoi(myPoi1);



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
                if (story.hasBeacon(beacon))
                {
                    POI poi = story.findPOI(beacon);
                    text1 = text1 + "Beacon UUID: " + beacon.ProximityUUID + "\nName: " + poi.getName() + "\nDescription: " + poi.getDescription() + "\nMajor: " +beacon.Major +"\nMinor: " + beacon.Minor + "\n\n";
                }
                if(!story.hasBeacon(beacon))
                    text1 = text1 + "Beacon UUID: " + beacon.ProximityUUID + "\nMajor: " + beacon.Major + "\nMinor: " + beacon.Minor +" " + story.getSize()+"\n\n" ;
            }
			beaconContextualText.Text = text1 ;


		}

	}
}

