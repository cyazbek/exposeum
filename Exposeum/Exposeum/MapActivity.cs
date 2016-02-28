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
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			View view = LayoutInflater.Inflate (Resource.Layout.MapView, null);

			FrameLayout map_view_frame_layout = (FrameLayout)view.FindViewById (Resource.Id.map_view_frame_lay);

			MapView map_view = new MapView (this);

			map_view_frame_layout.AddView (map_view);

			bool guidedTour = true; //will depend on whether or not currentStoryLine == null I presume (storyline 0 shenanigans)

			if (guidedTour) {
				// Create a new fragment and a transaction.
				FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
				MapProgressionFragment newMapProgressionFrag = new MapProgressionFragment(); //pass storyline into here eventually?

				// The fragment will have the ID of Resource.Id.fragment_container.
				fragmentTx.Add(Resource.Id.map_frag_frame_lay, newMapProgressionFrag);

				// Commit the transaction.
				fragmentTx.Commit();
			}


			SeekBar floorSeekBar = view.FindViewById<SeekBar>(Resource.Id.floor_seek_bar);
			floorSeekBar.ProgressChanged += map_view.OnMapSliderProgressChange;

			SetContentView (view);
		}
	}
}
