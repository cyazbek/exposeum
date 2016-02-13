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

namespace Exposeum
{
	[Activity(Label = "@string/map_activity", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]	
	public class MapActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			View view = LayoutInflater.Inflate (Resource.Layout.MapView, null);
			SetContentView(view);

			LinearLayout lay = (LinearLayout)view.FindViewById (Resource.Id.linlay1);

			MapView map_view = new MapView (this);

			map_view.LayoutParameters = new ViewGroup.LayoutParams (
				ViewGroup.LayoutParams.WrapContent,
				ViewGroup.LayoutParams.WrapContent
			);

			lay.AddView (map_view, 1);

			SeekBar floorSeekBar = view.FindViewById<SeekBar>(Resource.Id.floor_seek_bar);
			floorSeekBar.ProgressChanged += map_view.OnMapSliderProgressChange;
		}
	}
}
