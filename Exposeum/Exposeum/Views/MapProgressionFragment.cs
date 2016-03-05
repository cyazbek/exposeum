using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using Exposeum.Models;

namespace Exposeum
{
	public class MapProgressionFragment : Fragment
	{
		private StoryLine _storyline;
		private ProgressBar _progressBar;

		public MapProgressionFragment() { } //necessary due to default constructor called on rotate event

		public MapProgressionFragment(StoryLine storyline){
			_storyline = storyline;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View mapProgressionFragmentView = inflater.Inflate(Resource.Layout.MapProgressionFragment, container, false);
			_progressBar = mapProgressionFragmentView.FindViewById<ProgressBar> (Resource.Id.map_progression_frag_progressbar);
			Update ();
			return mapProgressionFragmentView;
		}

		public void Update(){
			
			int visitedMapElements = 0;

			while (_storyline.MapElements [visitedMapElements].Visited) {
				visitedMapElements++;
			}


			_progressBar.Progress = (int)(100 * ((float)visitedMapElements / (float) _storyline.MapElements.Count));
		}
	}
}
