namespace Exposeum
{
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
		private static readonly int POINT_RADIUS = 50;
		private readonly Drawable _map;
		private readonly ScaleGestureDetector _scaleDetector;
		private int _activePointerId = InvalidPointerId;
		private float _lastTouchX;
		private float _lastTouchY;
		private float _posX;
		private float _posY;
		private float _scaleFactor = 1.0f;
		private Paint _pointColour = new Paint();

		//test u,v coordinates for points to be drawn on map
		private float[] uvPointCoordinates = new float[4]{0.50f, 0.50f, 0.75f, 0.75f};

		public MapView (Context context) : base(context, null, 0)
		{
			_map = context.Resources.GetDrawable (Resource.Drawable.metro_map);
			_map.SetBounds (0, 0, _map.IntrinsicWidth, _map.IntrinsicHeight);
			_scaleDetector = new ScaleGestureDetector (context, new MyScaleListener (this));

			_pointColour.SetStyle (Paint.Style.Fill);
			_pointColour.Color = Color.OrangeRed;
		}

		public override bool OnTouchEvent (MotionEvent ev)
		{
			_scaleDetector.OnTouchEvent (ev);

			MotionEventActions action = ev.Action & MotionEventActions.Mask;
			int pointerIndex;

			switch (action) {
			case MotionEventActions.Down:
				Log.Debug ("touchdown-uvcoords", getUVFromScreenCoords (ev.GetX (), ev.GetY ()));
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
			canvas.Translate (_scaleFactor * -_map.IntrinsicWidth / 2, _scaleFactor * -_map.IntrinsicHeight / 2);
			canvas.Translate (_posX, _posY);
			canvas.Scale (_scaleFactor, _scaleFactor);
			_map.Draw (canvas);

			//draw pins on top of map
			for (int i = 0; i < uvPointCoordinates.Length; i += 2)
				canvas.DrawCircle (uvPointCoordinates[i] * _map.IntrinsicWidth, uvPointCoordinates [i + 1] * _map.IntrinsicHeight, POINT_RADIUS, _pointColour);

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

		//return screen touch coordinates in uv coordinates of map
		//takes translation and zooming into account
		private string getUVFromScreenCoords(float screenX, float screenY){

			float u = ((screenX - _posX)/_scaleFactor + _map.IntrinsicWidth/2)/_map.IntrinsicWidth;

			string ustr = u.ToString ();
			if (u > 1.0f) {
				ustr = "greater than 1.0";
			}
			if (u < 0.0f) {
				ustr = "less than 0.0";
			}

			float v = ((screenY - _posY)/_scaleFactor + _map.IntrinsicHeight/2)/_map.IntrinsicHeight;

			string vstr = v.ToString ();
			if (v > 1.0f) {
				vstr = "greater than 1.0";
			}
			if (v < 0.0f) {
				vstr = "less than 0.0";
			}

			return string.Format ("({0}, {1})", ustr, vstr);
		}
	}
}
