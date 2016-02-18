using Exposeum.Models;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Webkit;
using Android.App;
using Java.Util;

namespace Exposeum
{
	/// <summary>
	///   This class will show how to respond to touch events using a custom subclass
	///   of View.
	/// </summary>
	public class MapView : View

    {
		private static readonly int InvalidPointerId = -1;
		private readonly ScaleGestureDetector _scaleDetector;
		private int _activePointerId = InvalidPointerId;
		private float _lastTouchX;
		private float _lastTouchY;
		private float _posX;
		private float _posY;
		private float _scaleFactor = 0.5f;
		private PointOfInterest _lastClickedPOI;
		private Floor _currentFloor;
		private Context _context;

        
        //test points to be drawn on map
        private List<Floor> sampleFloors = new List<Floor>();

		private PopupWindow _popupWindow;

	    public MapView (Context context) : base(context, null, 0)
		{            
        
            _context = context;
			_scaleDetector = new ScaleGestureDetector (context, new MyScaleListener (this));
        
            Floor floor1 = new Floor (Resources.GetDrawable (Resource.Drawable.floor_1));
			Floor floor2 = new Floor (Resources.GetDrawable (Resource.Drawable.floor_2));
			Floor floor3 = new Floor (Resources.GetDrawable (Resource.Drawable.floor_3));
			Floor floor4 = new Floor (Resources.GetDrawable (Resource.Drawable.floor_4));
			Floor floor5 = new Floor (Resources.GetDrawable (Resource.Drawable.floor_5));

	        PointOfInterest p1 = new PointOfInterest(0.53f, 0.46f);
	        p1.name_en = "POINT 1";
	        p1.name_fr = "Le POINT 2";
            floor1.addPointOfInterest (p1);

            PointOfInterest p2 = new PointOfInterest(0.60f, 0.82f);
            p2.name_en = "POINT 2";
	        p2.name_fr = "Le POINT 2";
            floor1.addPointOfInterest (p2);

			floor2.addPointOfInterest (new PointOfInterest (0.90f, 0.46f));
			floor2.addPointOfInterest (new PointOfInterest (0.53f, 0.66f));

			floor3.addPointOfInterest (new PointOfInterest (0.53f, 0.43f));
			floor3.addPointOfInterest (new PointOfInterest (0.77f, 0.46f));

			floor4.addPointOfInterest (new PointOfInterest (0.53f, 0.46f));
			floor4.addPointOfInterest (new PointOfInterest (0.73f, 0.16f));

			floor5.addPointOfInterest(new PointOfInterest(0.53f, 0.46f));
            floor5.addPointOfInterest(new PointOfInterest(0.73f, 0.16f));


            sampleFloors.Add (floor1);
			sampleFloors.Add (floor2);
			sampleFloors.Add (floor3);
			sampleFloors.Add (floor4);
			sampleFloors.Add (floor5);

			_currentFloor = sampleFloors [0];
		}

        public void OnMapSliderProgressChange (object sender, SeekBar.ProgressChangedEventArgs e)
		{
			_currentFloor = sampleFloors [e.Progress];
			_lastClickedPOI = null;

			this.Invalidate (); //force redraw the activity instead of needing a touch
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
					if (_lastClickedPOI == null) {
						_lastClickedPOI = selected;
					} else if (_lastClickedPOI != selected) {
						_currentFloor.addEdge(new Models.Edge (_lastClickedPOI, selected));
						_lastClickedPOI = selected;
					}

					Views.BeaconPopup newBeaconPopup = new Views.BeaconPopup (_context, selected);
					newBeaconPopup.Show ();
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
			canvas.Translate (_posX + _scaleFactor * -_currentFloor.Image.IntrinsicWidth / 2, _posY + _scaleFactor * -_currentFloor.Image.IntrinsicHeight / 2);
			canvas.Scale (_scaleFactor, _scaleFactor);
			_currentFloor.Draw (canvas);
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

			PointOfInterest clicked = null;

			foreach (PointOfInterest poi in _currentFloor.PointsOfInterest) {
				float poiX = _posX + (_scaleFactor * _currentFloor.Image.IntrinsicWidth * poi._u) - ((_scaleFactor * _currentFloor.Image.IntrinsicWidth) / 2);
				float poiY = _posY + (_scaleFactor * _currentFloor.Image.IntrinsicHeight * poi._v) - ((_scaleFactor * _currentFloor.Image.IntrinsicHeight) / 2);

				if (Math.Sqrt (Math.Pow (screenX - poiX, 2) + Math.Pow (screenY - poiY, 2)) <= poi.Radius * _scaleFactor) {
					clicked = poi;
					poi.SetTouched();
					break;
				}
			}

            return clicked;
		}

        
    }
}
