using System;
using System.Collections.Generic;

using Android.Graphics.Drawables;
using Android.Graphics;

namespace Exposeum.Models
{
	public class Floor
	{
		private Drawable _floorPlan;
		private List<Edge> _floorEdges = new List<Edge>();
		private List<PointOfInterest> _floorPointsOfInterest = new List<PointOfInterest>();

		public Floor (Drawable floorPlan)
		{
			_floorPlan = floorPlan;
			_floorPlan.SetBounds (0, 0, _floorPlan.IntrinsicWidth, _floorPlan.IntrinsicHeight);
		}

		public void Draw(Canvas canvas)
		{
			_floorPlan.Draw (canvas);

			//draw edges on top of map
			foreach (Models.Edge edge in _floorEdges)
				edge.Draw (canvas, _floorPlan.IntrinsicWidth, _floorPlan.IntrinsicHeight);

			//draw pins on top of edges
			foreach (PointOfInterest poi in _floorPointsOfInterest)
				poi.Draw (canvas, _floorPlan.IntrinsicWidth, _floorPlan.IntrinsicHeight);
		}

		public void addEdge(Edge e)
		{
			_floorEdges.Add (e);
		}

		public void addPointOfInterest(PointOfInterest e)
		{
			_floorPointsOfInterest.Add (e);
		}

		public Drawable Image
		{
			get { return this._floorPlan; }
		}

		public List<PointOfInterest> PointsOfInterest
		{
			get { return this._floorPointsOfInterest; }
		}

	}
}
