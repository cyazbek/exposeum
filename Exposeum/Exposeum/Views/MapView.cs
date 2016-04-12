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
using Android.Util;
using Android.Webkit;

namespace Exposeum.Views
{

	public class MapView : WebView
    {
		private readonly Context _context;
		private readonly Map _map;
		private readonly MapController _controller;
		private readonly Paint _visitedEdge = new Paint ();
		private readonly Paint _unvisitedEdge = new Paint ();

        private PointOfInterestPopup _newPointOfInterestPopup;
		private OutOfOrderPointFragment _outOfOrderDialog;
		private EndOfStoryLineFragment _endOfStoryLinePopup;

		public MapView (Context context, MapController controller) : base(context, null, 0)
		{            
            _context = context;
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

			Settings.BuiltInZoomControls = true;
			Settings.DisplayZoomControls = false;

			Settings.LoadWithOverviewMode = true; //load each floor initially fully zoomed out
			Settings.UseWideViewPort = true;

			LoadFloorPlan (_map.CurrentFloor);
		}

		public void LoadFloorPlan(Floor floor){
			String html = String.Format("<html><body><img src=\"{0}\" width=\"{1}\" height=\"{2}\"></body></html>", floor.ImagePath, floor.Width, floor.Height);
			LoadDataWithBaseURL ("file:///", html, "text/html", "UTF-8", null);
		}

		public void InitiatePointOfInterestPopup(PointOfInterest poi, PointOfInterestPopup.DismissCallback callback){

            if (_newPointOfInterestPopup == null || ! _newPointOfInterestPopup.IsShowing())
            {
                _newPointOfInterestPopup = new PointOfInterestPopup(_context, poi);
				_newPointOfInterestPopup.SetDismissCallback (callback);
                _newPointOfInterestPopup.Show();
            }
		}

		public override bool OnTouchEvent (MotionEvent ev)
		{
			base.OnTouchEvent (ev);

			MotionEventActions action = ev.Action & MotionEventActions.Mask;
			int pointerIndex;

			switch (action) {
			case MotionEventActions.Up:
				PointOfInterest selected = GetSelectedPoi (ev.GetX (), ev.GetY ());
				if (selected != null) {
					_controller.DisplayPopUp (selected);
				}
				break;
			}

			return true;
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			canvas.Save ();
			canvas.Scale (Scale, Scale);

			DrawStoryLine (canvas);

			//If we have an ActiveShortestPath, we draw it
			if (_map.GetActiveShortestPath() != null) {
				DrawShortestPath (canvas);
			}
				
			canvas.Restore ();
		}

		private void DrawShortestPath(Canvas canvas){
			List<MapElement> shortestPathMapElements = _map.GetActiveShortestPath ().MapElements.Where(e => e.Floor.Id.Equals(_map.CurrentFloor.Id)).ToList();

			DrawMapElementsEdges (canvas, shortestPathMapElements, 255);
			DrawMapElements (canvas, shortestPathMapElements);
		}

		private void DrawStoryLine(Canvas canvas){
			int storyLineAlpha = 255;
			if (_map.GetActiveShortestPath () != null)
				storyLineAlpha = 50;
				
			//draw edges and POIs on top of map
			List<MapElement> currentFloorMapElements = _map.CurrentStoryline.MapElements.Where(e => e.Floor.Id.Equals(_map.CurrentFloor.Id)).ToList();

			//draw the edges first, but only if we are in storyline mode
			if (!ExposeumApplication.IsExplorerMode) {
				DrawMapElementsEdges (canvas, currentFloorMapElements, storyLineAlpha);
			}

			//finally, draw the mapElements
			DrawMapElements(canvas, currentFloorMapElements);
		}

		private void DrawMapElements(Canvas canvas, List<MapElement> mapElements){
			foreach (MapElement mapElement in mapElements) {
				mapElement.Draw (canvas);
			}
		}

		private void DrawMapElementsEdges(Canvas canvas, List<MapElement> mapElements, int alpha){

			Paint appropriateEdgePaintBrush = _visitedEdge;
			appropriateEdgePaintBrush.Alpha = alpha;

			PointOfInterest lastVisitedPoi = null;

			List<PointOfInterest> currentFloorPoIs = mapElements.OfType<PointOfInterest> ().ToList();

			//get a reference to the last visited POI for drawing purposes
			for (int i = 0; i < currentFloorPoIs.Count; i++) {

				if (!currentFloorPoIs [i].Visited)
					break;

				lastVisitedPoi = currentFloorPoIs [i];

			}

			for (int i = 0; i < mapElements.Count; i++) {

				MapElement current = mapElements[i];

				if (current == lastVisitedPoi || lastVisitedPoi == null) {
					appropriateEdgePaintBrush = _unvisitedEdge;
					appropriateEdgePaintBrush.Alpha = alpha;
				}

				if (i < mapElements.Count - 1) {

					MapElement next = mapElements[i + 1];

                    Android.Graphics.Path path = new Android.Graphics.Path ();
					path.MoveTo(current.UCoordinate * _map.CurrentFloor.Width, current.VCoordinate * _map.CurrentFloor.Height);
					path.LineTo(next.UCoordinate * _map.CurrentFloor.Width, next.VCoordinate * _map.CurrentFloor.Height);

					canvas.DrawPath(path, appropriateEdgePaintBrush);
				}
			}
		}

		private PointOfInterest GetSelectedPoi(float screenX, float screenY){

			List<PointOfInterest> currentFloorPoIs = _map.CurrentStoryline.MapElements.OfType<PointOfInterest>().Where(poi => poi.Floor.Id.Equals(_map.CurrentFloor.Id)).ToList();

			foreach (PointOfInterest poi in currentFloorPoIs) {

				float poiX = (Scale * _map.CurrentFloor.Width * poi.UCoordinate) - ScrollX;
				float poiY = (Scale * _map.CurrentFloor.Height * poi.VCoordinate) - ScrollY;

				if (Math.Sqrt (Math.Pow (screenX - poiX, 2f) + Math.Pow (screenY - poiY, 2f)) <= poi.Radius * Scale) {
					return poi;
				}
			}

			return null;
		}

		public void InitiateOutOfOrderPointOfInterestPopup(PointOfInterest currentPoi, IEnumerable<MapElement> skippedPoi, OutOfOrderPointFragment.CallbackSkipUnvisitedPoints callbackSkip, OutOfOrderPointFragment.CallbackReturnToLastPoint callbackReturn)
	    {
	        using (FragmentTransaction tr = ((Activity) _context).FragmentManager.BeginTransaction())
	        {
				if (_outOfOrderDialog == null || !_outOfOrderDialog.IsVisible) {
					_outOfOrderDialog = new OutOfOrderPointFragment(currentPoi, skippedPoi, callbackSkip, callbackReturn);
					_outOfOrderDialog.Show(tr, "Wrong POI");
				}
            }
	    }

		public void InitiateEndOfStoryLinePopup(EndOfStoryLineFragment.Callback callback)
		{
			using (FragmentTransaction tr = ((Activity) _context).FragmentManager.BeginTransaction())
			{
				_endOfStoryLinePopup = new EndOfStoryLineFragment(callback);
				_endOfStoryLinePopup.Show(tr, "Storyline Complete!");
			}
		}
    }
}
