using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Exposeum.Models;
using Exposeum.Views;
using Exposeum.Controllers;
using System;
using System.Linq;

namespace Exposeum.Views
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
		private MapController _controller;
		private Paint _visitedEdge = new Paint ();
		private Paint _unvisitedEdge = new Paint ();

	    public MapView (Context context) : base(context, null, 0)
		{            
            _context = context;
			_scaleDetector = new ScaleGestureDetector (context, new MyScaleListener (this));
			_controller = new MapController (this);
			_map = _controller.Model;

			_visitedEdge.SetStyle (Paint.Style.Fill);
			_visitedEdge.Color = Color.Purple;
			_visitedEdge.StrokeWidth = 20;

			_unvisitedEdge.SetStyle (Paint.Style.Stroke);
			_unvisitedEdge.Color = Color.Red;
			_unvisitedEdge.StrokeWidth = 25;

			this.LayoutParameters = new ViewGroup.LayoutParams (
				ViewGroup.LayoutParams.MatchParent,
				ViewGroup.LayoutParams.MatchParent
			);

		}

        public void OnMapSliderProgressChange (object sender, SeekBar.ProgressChangedEventArgs e)
		{
			_controller.FloorChanged(e.Progress);
		}

		public void Update(){
			this.Invalidate();
		}

		public void InitiatePointOfInterestPopup(PointOfInterest poi){
			Views.BeaconPopup newBeaconPopup = new Views.BeaconPopup (_context, poi);
			newBeaconPopup.Show ();
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
					_controller.PointOfInterestTapped (selected);
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
			canvas.Translate (_translateX + _scaleFactor * -_map.CurrentFloor.Image.IntrinsicWidth / 2, _translateY + _scaleFactor * -_map.CurrentFloor.Image.IntrinsicHeight / 2);
			canvas.Scale (_scaleFactor, _scaleFactor);

			_map.CurrentFloor.Image.Draw (canvas);

			Paint appropriateEdgePaintBrush = _visitedEdge;

			//draw edges and POIs on top of map

			List<PointOfInterest> currentFloorPOIs = _map.CurrentStoryline.poiList.Where(poi => poi.floor.Equals(_map.CurrentFloor)).ToList();

			for (int i = 0; i < currentFloorPOIs.Count; i++) {

				PointOfInterest currentPOI = currentFloorPOIs[i];
					
				if (i < currentFloorPOIs.Count - 1) {

					PointOfInterest nextPOI = currentFloorPOIs[i + 1];

					if (!nextPOI.visited)
						appropriateEdgePaintBrush = _unvisitedEdge;
					
					canvas.DrawLine (currentPOI._u * _map.CurrentFloor.Image.IntrinsicWidth, currentPOI._v * _map.CurrentFloor.Image.IntrinsicHeight, nextPOI._u * _map.CurrentFloor.Image.IntrinsicWidth, nextPOI._v * _map.CurrentFloor.Image.IntrinsicHeight, appropriateEdgePaintBrush);

				}

				currentPOI.Draw (canvas); //draw the current guy

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

			List<PointOfInterest> currentFloorPOIs = _map.CurrentStoryline.poiList.Where(poi => poi.floor.Equals(_map.CurrentFloor)).ToList();

			foreach (PointOfInterest poi in currentFloorPOIs) {

				float poiX = _translateX + (_scaleFactor * _map.CurrentFloor.Image.IntrinsicWidth * poi._u) - ((_scaleFactor * _map.CurrentFloor.Image.IntrinsicWidth) / 2);
				float poiY = _translateY + (_scaleFactor * _map.CurrentFloor.Image.IntrinsicHeight * poi._v) - ((_scaleFactor * _map.CurrentFloor.Image.IntrinsicHeight) / 2);

				if (Math.Sqrt (Math.Pow (screenX - poiX, 2) + Math.Pow (screenY - poiY, 2)) <= poi.Radius * _scaleFactor) {
					return poi;
				}
			}

			return null;
		}
    }
}