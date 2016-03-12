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
using Android.Util;
using Exposeum.Views;
using Exposeum.Controllers;

namespace Exposeum
{
	[Activity(Label = "@string/map_activity", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]	
	public class MapActivity : Activity
	{

		private BeaconFinder _beaconFinder;
		private MapController _mapController;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			_mapController = MapController.GetInstance (this);

			//Configure the BeaconFinder for this activity
			_beaconFinder = BeaconFinder.GetInstance ();
			_beaconFinder.SetInFocus(true);
			_beaconFinder.SetNotificationDestination (this);

			//Bind the _totalMapView to the Activity
			SetContentView (_mapController._totalMapView);

            //=========================================================================================

            //remove default bar
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetDisplayShowTitleEnabled(false);

            //add custom bar
            ActionBar.SetCustomView(Resource.Layout.ActionBar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            var backActionBarButton = FindViewById<ImageView>(Resource.Id.BackImage);
            backActionBarButton.Click += (s, e) =>
            {
                base.OnBackPressed();
            };





            //=========================================================================================
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

		protected override void OnDestroy ()
		{
			base.OnDestroy ();
			_mapController.Destroy();
		}


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {


                case Resource.Id.LanguageItem:
                    //Language.ToogleLanguage();
                    //var intent = new Intent(this, typeof(VisitActivityFr));
                    //StartActivity(intent);
                    return true;
                case Resource.Id.PauseItem:
                    //do something
                    return true;
                case Resource.Id.QRScannerItem:
                    //do something
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
