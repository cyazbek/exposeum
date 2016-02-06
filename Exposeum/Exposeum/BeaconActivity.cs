using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EstimoteSdk;
using Java.Util.Concurrent;
using Android.Util;
using Android.Content.PM;
using Android.Bluetooth;
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
        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//View v = new BeaconView(Resource.Layout.Beacon);
			SetContentView (Resource.Layout.Beacon);
            myPoi = new POI { name_en = "Point Of interest one", name_fr = "Point d'interet un", dscription_en = "This is the point of interest one", dscription_fr = "Celui là est le premier point d'interet", beaconId = UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d") };
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

			Beacon beacon = beaconFinder.getClosestBeacon();

			text1 = text1 + "The Closest beacon is: \n";
            if(beacon!=null)
            {
                if (beacon.ProximityUUID.Equals(myPoi.beaconId))
                {
                    text1 = text1 + "Beacon UUID: " + beacon.ProximityUUID + "\nName: " + myPoi.getName() + "\nDescription: " + myPoi.getDescription() + "\nMinor: " + beacon.Minor + "\n\n";
                }
                else
                    text1 = text1 + "Beacon UUID: " + beacon.ProximityUUID + "\nMajor: " + beacon.Major + "\nMinor: " + beacon.Minor + "\n\n";
            }
			beaconContextualText.Text = text1 ;


		}

	}
}

