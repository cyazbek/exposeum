namespace Exposeum
{
	using System.Collections.Generic;
	using System;
	using Android.Content;
	using Android.Graphics;
	using Android.Graphics.Drawables;
	using Android.Util;
	using Android.Views;

	/// <summary>
	///   This class will show how to respond to touch events using a custom subclass
	///   of View.
	/// </summary>
	public class MapView : View
	{
		private static readonly int InvalidPointerId = -1;
		private readonly Drawable _map;
		private readonly ScaleGestureDetector _scaleDetector;
		private int _activePointerId = InvalidPointerId;
		private float _lastTouchX;
		private float _lastTouchY;
		private float _posX;
		private float _posY;
		private float _scaleFactor = 1.0f;

		//test points to be drawn on map
		private List<PointOfInterest> samplePoints = new List<PointOfInterest>();

		public MapView (Context context) : base(context, null, 0)
		{
			_map = context.Resources.GetDrawable (Resource.Drawable.floor_5);
			_map.SetBounds (0, 0, _map.IntrinsicWidth, _map.IntrinsicHeight);
			_scaleDetector = new ScaleGestureDetector (context, new MyScaleListener (this));

			//push some sample points to draw on our map
			samplePoints.Add(new PointOfInterest(0.53f, 0.46f, "site 1"));
			samplePoints.Add(new PointOfInterest(0.62f, 0.64f, "site 2"));
			samplePoints.Add(new PointOfInterest(0.60f, 0.82f, "site 3"));
			samplePoints.Add(new PointOfInterest(0.85f, 0.88f, "site 4"));
			samplePoints.Add(new PointOfInterest(0.925f, 0.55f, "site 5"));
			samplePoints.Add(new PointOfInterest(0.77f, 0.265f, "site 6"));
			samplePoints.Add(new PointOfInterest(0.56f, 0.19f, "site 7"));
			samplePoints.Add(new PointOfInterest(0.346f, 0.886f, "site 8"));
			samplePoints.Add(new PointOfInterest(0.241f, 0.266f, "site 9"));
		}

		public override bool OnTouchEvent (MotionEvent ev)
		{
			_scaleDetector.OnTouchEvent (ev);

			MotionEventActions action = ev.Action & MotionEventActions.Mask;
			int pointerIndex;

			switch (action) {
			case MotionEventActions.Down:
				PointOfInterest selected = selectedPOI (ev.GetX (), ev.GetY ());
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
					_posX += deltaX;
					_posY += deltaY;
					Invalidate ();
				}

				_lastTouchX = x;
				_lastTouchY = y;
				break;

			case MotionEventActions.Up:
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
			canvas.Translate (_posX + _scaleFactor * -_map.IntrinsicWidth / 2, _posY + _scaleFactor * -_map.IntrinsicHeight / 2);
			canvas.Scale (_scaleFactor, _scaleFactor);
			_map.Draw (canvas);

			//draw pins on top of map
			foreach (PointOfInterest poi in samplePoints)
				poi.Draw (canvas, _map.IntrinsicWidth, _map.IntrinsicHeight);

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

		private PointOfInterest selectedPOI(float screenX, float screenY){

			PointOfInterest clicked = null;

			foreach (PointOfInterest poi in samplePoints) {
				float poiX = _posX + (_scaleFactor * _map.IntrinsicWidth * poi.U) - ((_scaleFactor * _map.IntrinsicWidth) / 2);
				float poiY = _posY + (_scaleFactor * _map.IntrinsicHeight * poi.V) - ((_scaleFactor * _map.IntrinsicHeight) / 2);

				if (Math.Sqrt (Math.Pow (screenX - poiX, 2) + Math.Pow (screenY - poiY, 2)) <= poi.Radius*_scaleFactor) {
					clicked = poi;
					poi.SetTouched();
					continue;
				}
			}
				
			return clicked;
		}
	}
}
