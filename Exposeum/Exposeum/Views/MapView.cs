using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Exposeum.Models;
using Exposeum.Controllers;
using System;
using System.Linq;
using Android.App;
using Exposeum.Fragments;

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
		private float _canvasWidth, _canvasHeight; 

        private PointOfInterestPopup _newPointOfInterestPopup;
		private OutOfOrderPointFragment _outOfOrderDialog;

		public MapView (Context context, MapController controller) : base(context, null, 0)
		{            
            _context = context;
			_scaleDetector = new ScaleGestureDetector (context, new MyScaleListener (this));
			_controller = controller;
			_map = Map.GetInstance();

			_visitedEdge.SetStyle (Paint.Style.FillAndStroke);
			_visitedEdge.Color = Color.Green;
			_visitedEdge.StrokeWidth = 20;
			_visitedEdge.AntiAlias = true;
			_visitedEdge.StrokeCap = Paint.Cap.Round;

			_unvisitedEdge.SetStyle (Paint.Style.Stroke);
			_unvisitedEdge.Color = Color.Red;
			_unvisitedEdge.StrokeWidth = 25;
			_unvisitedEdge.SetPathEffect(new DashPathEffect (new float[]{ 5,10}, 0));
			_unvisitedEdge.AntiAlias = true;

			LayoutParameters = new ViewGroup.LayoutParams (
				ViewGroup.LayoutParams.MatchParent,
				ViewGroup.LayoutParams.MatchParent
			);
		}

		public void Update(){
			Invalidate();
		}

		public void InitiatePointOfInterestPopup(PointOfInterest poi){

            if (_newPointOfInterestPopup == null || ! _newPointOfInterestPopup.IsShowing())
            {
                _newPointOfInterestPopup = new PointOfInterestPopup(_context, poi);
                _newPointOfInterestPopup.Show();
            }
		}

		public override bool OnTouchEvent (MotionEvent ev)
		{
			_scaleDetector.OnTouchEvent (ev);

			MotionEventActions action = ev.Action & MotionEventActions.Mask;
			int pointerIndex;

			switch (action) {
			case MotionEventActions.Down:
				PointOfInterest selected = GetSelectedPoi (ev.GetX (), ev.GetY ());
				if (selected != null) {
					_controller.DisplayPopUp (selected);
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

					//clamp the translation to keep the map on-screen
					float maxX = (_scaleFactor * _map.CurrentFloor.Image.IntrinsicWidth / 2) + (_canvasWidth*0.5f);
					float maxY = (_scaleFactor * _map.CurrentFloor.Image.IntrinsicHeight / 2) + (_canvasHeight*0.5f);
					float minX = (_scaleFactor * -_map.CurrentFloor.Image.IntrinsicWidth / 2) + (_canvasWidth*0.5f);
					float minY = (_scaleFactor * -_map.CurrentFloor.Image.IntrinsicHeight / 2) + (_canvasHeight*0.5f);

					_translateX = Clamp (minX, maxX, _translateX);
					_translateY = Clamp (minY, maxY, _translateY);

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

			List<MapElement> currentFloorMapElements = _map.CurrentStoryline.MapElements.Where(e => e.Floor.Equals(_map.CurrentFloor)).ToList();

			//draw the edges first, but only if we are in storyline mode
			if (!ExposeumApplication.IsExplorerMode) {

				PointOfInterest lastVisitedPoi = null;

				List<PointOfInterest> currentFloorPoIs = currentFloorMapElements.OfType<PointOfInterest> ().ToList();

				//get a reference to the last visited POI for drawing purposes
				for (int i = 0; i < currentFloorPoIs.Count; i++) {

					if (!currentFloorPoIs [i].Visited)
						break;
					
					lastVisitedPoi = currentFloorPoIs [i];

				}

				for (int i = 0; i < currentFloorMapElements.Count; i++) {

					MapElement current = currentFloorMapElements[i];

					if (current == lastVisitedPoi || lastVisitedPoi == null)
						appropriateEdgePaintBrush = _unvisitedEdge;

					if (i < currentFloorMapElements.Count - 1) {

						MapElement next = currentFloorMapElements[i + 1];

						Path path = new Path ();
						path.MoveTo(current.U * _map.CurrentFloor.Image.IntrinsicWidth, current.V * _map.CurrentFloor.Image.IntrinsicHeight);
						path.LineTo(next.U * _map.CurrentFloor.Image.IntrinsicWidth, next.V * _map.CurrentFloor.Image.IntrinsicHeight);

						canvas.DrawPath(path, appropriateEdgePaintBrush);
					}
				}
			}

			//finally, draw the mapElements
			foreach (MapElement mapElement in currentFloorMapElements) {
				mapElement.Draw (canvas);
			}
				
			canvas.Restore ();
		}
			
		protected override void OnSizeChanged(int w, int h, int oldw, int oldh) {
			_canvasWidth = w;
			_canvasHeight = h;

			//center the image on canvas size change (rotation)
			//is also called when canvas is first instantiated
			float maxX = (_scaleFactor * _map.CurrentFloor.Image.IntrinsicWidth / 2) + (_canvasWidth*0.5f);
			float maxY = (_scaleFactor * _map.CurrentFloor.Image.IntrinsicHeight / 2) + (_canvasHeight*0.5f);
			float minX = (_scaleFactor * -_map.CurrentFloor.Image.IntrinsicWidth / 2) + (_canvasWidth*0.5f);
			float minY = (_scaleFactor * -_map.CurrentFloor.Image.IntrinsicHeight / 2) + (_canvasHeight*0.5f);

			_translateX = minX + ((maxX - minX) / 2.0f); //translate half-way on both axes
			_translateY = minY + ((maxY - minY) / 2.0f);
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
				if (_view._scaleFactor > 3.0f) {
					_view._scaleFactor = 3.0f;
				}
				if (_view._scaleFactor < 0.5f) {
					_view._scaleFactor = 0.5f;
				}

				_view.Invalidate ();

				return true;
			}
		}

		private PointOfInterest GetSelectedPoi(float screenX, float screenY){

			List<PointOfInterest> currentFloorPoIs = _map.CurrentStoryline.MapElements.OfType<PointOfInterest>().Where(poi => poi.Floor.Equals(_map.CurrentFloor)).ToList();

			foreach (PointOfInterest poi in currentFloorPoIs) {

				float poiX = _translateX + (_scaleFactor * _map.CurrentFloor.Image.IntrinsicWidth * poi.U) - ((_scaleFactor * _map.CurrentFloor.Image.IntrinsicWidth) / 2);
				float poiY = _translateY + (_scaleFactor * _map.CurrentFloor.Image.IntrinsicHeight * poi.V) - ((_scaleFactor * _map.CurrentFloor.Image.IntrinsicHeight) / 2);

				if (Math.Sqrt (Math.Pow (screenX - poiX, 2) + Math.Pow (screenY - poiY, 2)) <= poi.Radius * _scaleFactor) {
					return poi;
				}
			}

			return null;
		}

	    public void InitiateOutOfOrderPointOfInterestPopup(PointOfInterest poi)
	    {
	        using (FragmentTransaction tr = ((Activity) _context).FragmentManager.BeginTransaction())
	        {
				if (_outOfOrderDialog == null || !_outOfOrderDialog.IsVisible) {
					_outOfOrderDialog = new OutOfOrderPointFragment(poi);
					_outOfOrderDialog.Show(tr, "Wrong POI");
				}
                
            }
	    }

		private float Clamp(float min, float max, float value){
			return Math.Min(Math.Max(value, min), max);
		}
    }
}
