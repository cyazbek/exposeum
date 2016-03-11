﻿using System;
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
		public StoryLine Storyline;
		private MapProgressionFragmentView _mapProgressionFragmentView;

		public MapProgressionFragment() { } //necessary due to default constructor called on rotate event

		public MapProgressionFragment(StoryLine storyline){
			Storyline = storyline;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			_mapProgressionFragmentView = new MapProgressionFragmentView(Activity);
			_mapProgressionFragmentView.SetHostFragment (this);
			Update ();
			return _mapProgressionFragmentView;
		}

		public void Update(){
			_mapProgressionFragmentView.Invalidate ();
		}

		class MapProgressionFragmentView : LinearLayout
		{
			private MapProgressionFragment _hostFragment;
			private Paint _bgLine, _circle;

			public MapProgressionFragmentView(Context context) : base(context)
			{
				this.LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
				this.Orientation = Orientation.Vertical;
				this.SetWillNotDraw(false); //causes the OnDraw override below to be called
				this.SetMinimumHeight(220);
				this.SetBackgroundColor(Color.Red);

				ResetPaint();
			}

			protected override void OnDraw(Canvas canvas)
			{
				base.OnDraw(canvas);

				canvas.Save ();
				canvas.Scale (0.20f, 0.20f);

				Paint text = new Paint ();
				text.TextSize = 300;
				text.Color = Color.Red;

				StoryLine currentStoryline = _hostFragment.Storyline;

				int currentCentreX = 400;
				bool unvisitedTripped = false;

				for (int i = 0; i < currentStoryline.MapElements.Count; i++) {

					MapElement current = currentStoryline.MapElements [i];

					canvas.DrawLine (currentCentreX, 650, currentCentreX + 800, 650, _bgLine);

					if (!current.Visited)
						unvisitedTripped = true;
					
					if (unvisitedTripped) {

						text.Color = Color.White;

						_circle.SetStyle (Paint.Style.Fill);
						_circle.Color = Color.Red;

						canvas.DrawCircle (currentCentreX, 650, 250, _circle);

						_circle.Color = Color.White;
						_circle.SetStyle (Paint.Style.Stroke);
					}

					canvas.DrawCircle (currentCentreX, 650, 250, _circle);
					canvas.DrawText ("" + (i + 1), currentCentreX - 100, 650 + 100, text);

					currentCentreX += 800;
				}

				canvas.Restore ();

				ResetPaint ();
			}

			public override bool OnTouchEvent (MotionEvent ev){
				ev.Dispose (); //dispose of the touch event, do not pass it to the map view underneath
				return true;
			}

			public void SetHostFragment(MapProgressionFragment f){
				this._hostFragment = f;
			}

			private void ResetPaint(){
				_bgLine = new Paint ();
				_bgLine.SetStyle (Paint.Style.Stroke);
				_bgLine.Color = Color.White;
				_bgLine.StrokeWidth = 85;

				_circle = new Paint ();
				_circle.SetStyle (Paint.Style.FillAndStroke);
				_circle.Color = Color.White;
				_circle.StrokeWidth = 55;
			}
		}
	}
}