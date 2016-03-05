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
		public StoryLine _storyline;
		private MapProgressionFragmentView mapProgressionFragmentView;

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
			mapProgressionFragmentView = new MapProgressionFragmentView(Activity);
			mapProgressionFragmentView.SetHostFragment (this);
			Update ();
			return mapProgressionFragmentView;
		}

		public void Update(){

			mapProgressionFragmentView.Invalidate ();
		}

		class MapProgressionFragmentView : LinearLayout
		{
			private MapProgressionFragment hostFragment;

			public MapProgressionFragmentView(Context context) : base(context)
			{
				this.LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
				this.Orientation = Orientation.Vertical;
				this.SetWillNotDraw(false); //causes the OnDraw override below to be called
				this.SetMinimumHeight(300);
				this.SetBackgroundColor(Color.MediumVioletRed);
			}

			protected override void OnDraw(Canvas canvas)
			{
				base.OnDraw(canvas);

				canvas.Save ();

				canvas.Scale (0.20f, 0.20f);

				StoryLine currentStoryline = hostFragment._storyline;

				Paint bgLinePaint = new Paint ();
				bgLinePaint.SetStyle (Paint.Style.Stroke);
				bgLinePaint.Color = Color.White;
				bgLinePaint.StrokeWidth = 85;

				Paint circlePaint = new Paint ();
				circlePaint.SetStyle (Paint.Style.Stroke);
				circlePaint.Color = Color.Gray;
				circlePaint.StrokeWidth = 55;

				canvas.DrawLine (0.0f, 1000, 800 * currentStoryline.MapElements.Count, 1000, bgLinePaint);
		
				float currentCentreX;

				for (int i = 0; i < currentStoryline.MapElements.Count; i++) {
					currentCentreX = 800.0f * i;
					canvas.DrawCircle (currentCentreX, 1000, 250, circlePaint);
				}

				canvas.Restore ();

			}

			public void SetHostFragment(MapProgressionFragment f){
				this.hostFragment = f;
			}
		}
	}
}
