using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Exposeum.Models;
using Exposeum.Controllers;
using System;

namespace Exposeum
{

	public class MapView : View
    {
		private readonly ScaleGestureDetector _scaleDetector;
		private static readonly int InvalidPointerId = -1;
		private int _activePointerId = InvalidPointerId;
		private float _lastTouchX;
		private float _lastTouchY;
		private float _translateX;
		private float _translateY;
		private float _scaleFactor = 0.5f;
		private Context _context;
		private Map _map;
		private Paint _visitedEdge = new Paint ();
		private Paint _unvisitedEdge = new Paint ();

	    public MapView (Context context) : base(context, null, 0)
		{            
            _context = context;
			_scaleDetector = new ScaleGestureDetector (context, new MyScaleListener (this));
			_map = MapController.getInstance ().RegisterMapView (this);

			_visitedEdge.SetStyle (Paint.Style.Fill);
			_visitedEdge.Color = Color.Purple;
			_visitedEdge.StrokeWidth = 20;

			_unvisitedEdge.SetStyle (Paint.Style.Stroke);
			_unvisitedEdge.Color = Color.Red;
			_unvisitedEdge.StrokeWidth = 25;
		}

        public void OnMapSliderProgressChange (object sender, SeekBar.ProgressChangedEventArgs e)
		{
			MapController.getInstance().FoorChanged(e.Progress);
		}

		public void update(){
			//draw
			this.Invalidate(); //force redraw (necessary?)
		}

		public override bool OnTouchEvent (MotionEvent ev)
		{
			_scaleDetector.OnTouchEvent (ev);

			MotionEventActions action = ev.Action & MotionEventActions.Mask;
			int pointerIndex;

			switch (action) {
			case MotionEventActions.Down:
				PointOfInterest selected = getSelectedPOI (ev.GetX (), ev.GetY ());
				if (selected != null) {
					MapController.getInstance ().PointOfInterestTapped (selected);
				}
				_lastTouchX = ev.GetX ();
				_lastTouchY = ev.GetY ();
				_activePointerId = ev.GetPointerId (0);
				break;

			case MotionEventActions.Move:
				pointerIndex = ev.FindPointerIndex (_activePointerId);
				float x = ev.GetX (pointerIndex);
				float y = ev.GetY (pointerIndex);
				if (!_scaleDetector.IsInProgress) {
					// Only move the ScaleGestureDetector isn't already processing a gesture.
					float deltaX = x - _lastTouchX;
					float deltaY = y - _lastTouchY;
					_translateX += deltaX;
					_translateY += deltaY;
					Invalidate ();
				}

				_lastTouchX = x;
				_lastTouchY = y;
				break;

			case MotionEventActions.Cancel:
                    // This events occur when something cancels the gesture (for example the
                    // activity going in the background) or when the pointer has been lifted up.
                    // We no longer need to keep track of the active pointer.
				_activePointerId = InvalidPointerId;
				break;

			case MotionEventActions.PointerUp:
                    // We only want to update the last touch position if the the appropriate pointer
                    // has been lifted off the screen.
				pointerIndex = (int)(ev.Action & MotionEventActions.PointerIndexMask) >> (int)MotionEventActions.PointerIndexShift;
				int pointerId = ev.GetPointerId (pointerIndex);
				if (pointerId == _activePointerId) {
					// This was our active pointer going up. Choose a new
					// action pointer and adjust accordingly
					int newPointerIndex = pointerIndex == 0 ? 1 : 0;
					_lastTouchX = ev.GetX (newPointerIndex);
					_lastTouchY = ev.GetY (newPointerIndex);
					_activePointerId = ev.GetPointerId (newPointerIndex);
				}
				break;
			}
			return true;
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			canvas.Save ();
			canvas.Translate (_translateX + _scaleFactor * -_map._currentFloor.Image.IntrinsicWidth / 2, _translateY + _scaleFactor * -_map._currentFloor.Image.IntrinsicHeight / 2);
			canvas.Scale (_scaleFactor, _scaleFactor);

			_map._currentFloor.Image.Draw (canvas);

			Paint appropriateEdgePaintBrush = _visitedEdge;

			//draw edges on top of map
			for (int i = 0; i < _map._currentStoryline.poiList.Count; i++) {

				PointOfInterest current = _map._currentStoryline.poiList [i];

				if (_map._currentFloor.PointsOfInterest ().Contains (current)) {
					
					if (i < _map._currentStoryline.poiList.Count - 1) {

						PointOfInterest next = _map._currentStoryline.poiList [i + 1];

						if (_map._currentFloor.PointsOfInterest ().Contains (next)) {

							if (next.visited == false)
								appropriateEdgePaintBrush = _unvisitedEdge;
							canvas.DrawLine (current._u * _map._currentFloor.Image.IntrinsicWidth, current._v * _map._currentFloor.Image.IntrinsicHeight, next._u * _map._currentFloor.Image.IntrinsicWidth, next._v * _map._currentFloor.Image.IntrinsicHeight, appropriateEdgePaintBrush);

						}
					}

					current.Draw (canvas, _map._currentFloor.Image.IntrinsicWidth, _map._currentFloor.Image.IntrinsicHeight); //draw the current guy

				}
			}

			canvas.Restore ();
		}
			
		private class MyScaleListener : ScaleGestureDetector.SimpleOnScaleGestureListener
		{
			private readonly MapView _view;

			public MyScaleListener (MapView view)
			{
				_view = view;
			}

			public override bool OnScale (ScaleGestureDetector detector)
			{
               
				_view._scaleFactor *= detector.ScaleFactor;

				// put a limit on how small or big the image can get.
				if (_view._scaleFactor > 5.0f) {
					_view._scaleFactor = 5.0f;
				}
				if (_view._scaleFactor < 0.1f) {
					_view._scaleFactor = 0.1f;
				}

				_view.Invalidate ();

				return true;
			}
		}

		private PointOfInterest getSelectedPOI(float screenX, float screenY){

			foreach (PointOfInterest poi in _map._currentFloor.PointsOfInterest()) {

				float poiX = _translateX + (_scaleFactor * _map._currentFloor.Image.IntrinsicWidth * poi._u) - ((_scaleFactor * _map._currentFloor.Image.IntrinsicWidth) / 2);
				float poiY = _translateY + (_scaleFactor * _map._currentFloor.Image.IntrinsicHeight * poi._v) - ((_scaleFactor * _map._currentFloor.Image.IntrinsicHeight) / 2);

				if (Math.Sqrt (Math.Pow (screenX - poiX, 2) + Math.Pow (screenY - poiY, 2)) <= poi.Radius * _scaleFactor) {
					return poi;
				}
			}

			return null;
		}
    }
}
