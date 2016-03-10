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

namespace Exposeum
{
	[Activity(Label = "@string/map_activity", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]	
	public class MapActivity : Activity
	{

		private BeaconFinder _beaconFinder;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			View view = LayoutInflater.Inflate (Resource.Layout.MapView, null);

			FrameLayout mapViewFrameLayout = (FrameLayout)view.FindViewById (Resource.Id.map_view_frame_lay);

			MapView mapView = new MapView (this);

			mapViewFrameLayout.AddView (mapView);

			SeekBar floorSeekBar = view.FindViewById<SeekBar>(Resource.Id.floor_seek_bar);
			floorSeekBar.ProgressChanged += mapView.OnMapSliderProgressChange;

			_beaconFinder = BeaconFinder.GetInstance ();
			_beaconFinder.SetInFocus(true);
			_beaconFinder.SetNotificationDestination (this);
			SetContentView (view);
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
	}
}
