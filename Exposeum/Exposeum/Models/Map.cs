using System;
using System.Collections.Generic;
using Android.Content.Res;
using Java.Util;

namespace Exposeum.Models
{
	public class Map
	{
		private List<Floor> _floors;
		private Floor _currentFloor;
		private List<StoryLine> _storylines;
		private StoryLine _currentStoryline;
		private List<MapElement> _elements;
		private List<Edge> _edges;

		public Map ()
		{
			seedData ();
		}

		private void seedData(){
			
			Floor floor1 = new Floor (Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_1));
			Floor floor2 = new Floor (Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_2));
			Floor floor3 = new Floor (Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_3));
			Floor floor4 = new Floor (Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_4));
			Floor floor5 = new Floor (Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_5));

			PointOfInterest p1 = new PointOfInterest(0.53f, 0.46f);
			p1.name_en = "POINT 1";
			p1.name_fr = "Le POINT 2";
			p1.visited = true;
			floor1.addMapElement (p1);

			PointOfInterest p2 = new PointOfInterest(0.60f, 0.82f);
			p2.name_en = "POINT 2";
			p2.name_fr = "Le POINT 2";
			p2.visited = true;
			floor1.addMapElement (p2);

			PointOfInterest p3 = new PointOfInterest(0.1f, 0.92f);
			p3.name_en = "POINT 3";
			p3.name_fr = "Le POINT 3";
			p3.visited = true;
			floor1.addMapElement (p3);

			PointOfInterest p4 = new PointOfInterest(0.40f, 0.42f);
			p4.name_en = "POINT 4";
			p4.name_fr = "Le POINT 4";
			floor1.addMapElement (p4);

			PointOfInterest p5 = new PointOfInterest(0.30f, 0.12f);
			p5.name_en = "POINT 5";
			p5.name_fr = "Le POINT 5";
			floor1.addMapElement (p5);

			PointOfInterest p6 = new PointOfInterest(0.48f, 0.12f);
			p6.name_en = "POINT 6";
			p6.name_fr = "Le POINT 6";
			floor1.addMapElement (p6);

			PointOfInterest p7 = new PointOfInterest(0.38f, 0.62f);
			p7.name_en = "POINT 7";
			p7.name_fr = "Le POINT 7";

			PointOfInterest p8 = new PointOfInterest(0.98f, 0.82f);
			p8.name_en = "POINT 8";
			p8.name_fr = "Le POINT 8";

			Beacon beacon1 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
			PointOfInterest p9 = new PointOfInterest(0.53f, 0.46f);
			p9.beacon = beacon1;

			Beacon beacon2 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 49800, 5890);
			PointOfInterest p10 = new PointOfInterest(0.53f, 0.76f);
			p10.beacon = beacon2;

			floor2.addMapElement (p7);
			floor2.addMapElement (p8);

			floor3.addMapElement (new PointOfInterest(0.53f, 0.43f));
			floor3.addMapElement (new PointOfInterest(0.77f, 0.46f));

			floor4.addMapElement (new PointOfInterest(0.53f, 0.46f));
			floor4.addMapElement (new PointOfInterest(0.73f, 0.16f));

			floor5.addMapElement(p9);
			floor5.addMapElement(p10);

			_floors = new List<Floor> ();
			_floors.Add (floor1);

			_currentFloor = floor1;

			_floors.Add (floor2);
			_floors.Add (floor3);
			_floors.Add (floor4);
			_floors.Add (floor5);

			StoryLine storyline = new StoryLine ();
			storyline.addPoi (p1);
			storyline.addPoi (p2);
			storyline.addPoi (p3);
			storyline.addPoi (p4);
			storyline.addPoi (p5);
			storyline.addPoi (p6);
			storyline.addPoi (p7);
			storyline.addPoi (p8);
			storyline.addPoi (p9);
			storyline.addPoi (p10);

			_currentStoryline = storyline;

		}

		public void SetCurrentFloor(int i)
		{
			_currentFloor = _floors [i];
		}

		public Floor CurrentFloor
		{
			get { return this._currentFloor; }
			set { this._currentFloor = value; }
		}

		public StoryLine CurrentStoryline
		{
			get { return this._currentStoryline; }
			set { this._currentStoryline = value; }
		}
	}
}
