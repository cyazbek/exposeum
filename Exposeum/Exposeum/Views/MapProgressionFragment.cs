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
			MapProgressionFragmentView mapProgressionFragmentView = new MapProgressionFragmentView(Activity);
			_progressBar = mapProgressionFragmentView.progressBar;
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

		class MapProgressionFragmentView : LinearLayout
		{
			public ProgressBar progressBar { get; set; }

			public MapProgressionFragmentView(Context context) : base(context)
			{
				this.LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
				this.Orientation = Orientation.Vertical;

				progressBar = new ProgressBar(context, null, Android.Resource.Attribute.ProgressBarStyleHorizontal);
	
				progressBar.LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
				progressBar.Max = 100;

				AddView(progressBar);
			}
		}
	}
}
