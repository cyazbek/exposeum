using System;
using System.Collections.Generic;
using Java.Util;
using Android.Graphics.Drawables;
using System.Linq;
using Android.Graphics;

namespace Exposeum.Models
{
	public class Map
	{
        public int Id;
        private static Map _instance;
        public List<MapEdge> Edges { get; set; }
        public List<StoryLine> Storylines { get; set; }
        public List<MapElement> MapElements { get; set; }
        public List<Floor> Floors { get; set; }
        public StoryLine CurrentStoryline { get; set; }
        public Floor CurrentFloor { get; set; }
        private Path _activeShortestPath;

        private Map ()
		{
            Edges = new List<MapEdge>();
            Floors = new List<Floor>();
            MapElements = new List<MapElement>();
            Storylines = new List<StoryLine>();
			SeedData ();
        }

	    public static Map GetInstance()
	    {
	        if (_instance == null)
                _instance = new Map();

	        return _instance;
	    }
        
		private void SeedData(){

            BitmapDrawable floorplan1, floorplan2, floorplan3, floorplan4, floorplan5;

            try
            {
				Android.Util.DisplayMetrics currentDisplayMetrics = Android.App.Application.Context.Resources.DisplayMetrics;

				floorplan1 = (BitmapDrawable)BitmapDrawable.CreateFromStream(Android.App.Application.Context.Assets.Open("venue_data/floor/1/floor1.png"), null);
				floorplan1.SetTargetDensity(currentDisplayMetrics);
				floorplan2 = (BitmapDrawable)BitmapDrawable.CreateFromStream(Android.App.Application.Context.Assets.Open("venue_data/floor/2/floor2.png"), null);
				floorplan2.SetTargetDensity(currentDisplayMetrics);
				floorplan3 = (BitmapDrawable)BitmapDrawable.CreateFromStream(Android.App.Application.Context.Assets.Open("venue_data/floor/3/floor3.png"), null);
				floorplan3.SetTargetDensity(currentDisplayMetrics);
				floorplan4 = (BitmapDrawable)BitmapDrawable.CreateFromStream(Android.App.Application.Context.Assets.Open("venue_data/floor/4/floor4.png"), null);
				floorplan4.SetTargetDensity(currentDisplayMetrics);
				floorplan5 = (BitmapDrawable)BitmapDrawable.CreateFromStream(Android.App.Application.Context.Assets.Open("venue_data/floor/5/floor5.png"), null);
				floorplan5.SetTargetDensity(currentDisplayMetrics);

            }
            catch(Exception e)
            {
				floorplan1 = new BitmapDrawable();
				floorplan2 = new BitmapDrawable();
				floorplan3 = new BitmapDrawable();
				floorplan4 = new BitmapDrawable();
				floorplan5 = new BitmapDrawable();
            }

			StoryLine storyline = new StoryLine("Nipper the dog", "Le Chien Nipper","Adults", "Adultes", "A walk through different sections of RCA Victor’s production site, constructed over a period of roughly 25 years. This tour takes you through three different time zones, the 1920s, back when Montreal was the world’s largest grain hub and Canada’s productive power house, Montreal’s entertainment rich 1930s and 1943, when production at RCA Victor diversified to serve military needs.", "Une promenade à travers les différentes sections du site de production de RCA Victor, construit sur une période d’environ 25 ans. Ce circuit vous emmène à travers trois fuseaux horaires différents, les années 1920, époque où Montréal était le plus grand centre de grains du monde et la maison de puissance productive du Canada, de divertissement riches années 1930 à Montréal et 1943, lorsque la production chez RCA Victor diversifiée pour répondre aux besoins militaires.", 120 , Resource.Drawable.NipperTheDog);

            Floor floor1 = new Floor(floorplan1);
            Floor floor2 = new Floor(floorplan2);
            Floor floor3 = new Floor(floorplan3);
            Floor floor4 = new Floor(floorplan4);
            Floor floor5 = new Floor(floorplan5);

			Floors = new List<Floor> ();

			Floors.Add (floor1);
			Floors.Add (floor2);
			Floors.Add (floor3);
			Floors.Add (floor4);
			Floors.Add (floor5);

			CurrentFloor = floor1;

			//set up POIs

			Beacon beacon1 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
			PointOfInterest p1 = new PointOfInterest(0.53f, 0.46f, floor1);
            PointOfInterestDescription description1 = new PointOfInterestDescription("The First :: Title"
                    , "A Summary about the first :: summary", "A Full Description about the first :: Description");
		    p1.Description = description1;
            p1.NameEn = "POINT 1";
			p1.NameFr = "Le POINT 1";
            p1.Visited = false;
			p1.Beacon = beacon1;

			WayPoint pot1 = new WayPoint(0.60f, 0.82f, floor1);

			/*PointOfInterest p2 = new PointOfInterest(0.60f, 0.82f, floor1);
            PointOfInterestDescription description2 = new PointOfInterestDescription("The Second :: Title"
                    , "A Summary about the second :: summary", "A Full Description about the second :: Description");
            p2.description = description2;
            p2.name_en = "POINT 2";
			p2.name_fr = "Le POINT 2";
			p2.Visited = false;
			p2.beacon = beacon3;*/

			Beacon beacon3 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 55339, 19185);
			PointOfInterest p3 = new PointOfInterest(0.1f, 0.92f, floor1);
            PointOfInterestDescription description3 = new PointOfInterestDescription("The third :: Title"
                    , "A Summary about the third :: summary", "A Full Description about the third :: Description");
            p3.Description = description3;
            p3.NameEn = "POINT 3";
			p3.NameFr = "Le POINT 3";
			p3.Visited = false;
			p3.Beacon = beacon3;

			PointOfInterest p4 = new PointOfInterest(0.40f, 0.42f, floor1);
            PointOfInterestDescription description4 = new PointOfInterestDescription("The Fourth :: Title"
                    , "A Summary about the fourth :: summary", "A Full Description about the fourth :: Description");
            p4.Description = description4;
            p4.NameEn = "POINT 4";
			p4.NameFr = "Le POINT 4";

			PointOfInterest p5 = new PointOfInterest(0.30f, 0.12f, floor1);
            PointOfInterestDescription description5 = new PointOfInterestDescription("The Fifth :: Title"
                    , "A Summary about the fifth :: summary", "A Full Description about the fifth :: Description");
            p5.Description = description5;
            p5.NameEn = "POINT 5";
			p5.NameFr = "Le POINT 5";

			PointOfInterest p6 = new PointOfInterest(0.48f, 0.12f, floor1);
            PointOfInterestDescription description6 = new PointOfInterestDescription("The Sixth :: Title"
                    , "A Summary about the sixth :: summary", "A Full Description about the sixth :: Description");
            p6.Description = description6;
            p6.NameEn = "POINT 6";
			p6.NameFr = "Le POINT 6";

			PointOfInterest p7 = new PointOfInterest(0.38f, 0.62f, floor2);
            PointOfInterestDescription description7 = new PointOfInterestDescription("The Seventh :: Title"
                    , "A Summary about the seventh :: summary", "A Full Description about the seventh :: Description");
            p7.Description = description7;
            p7.NameEn = "POINT 7";
			p7.NameFr = "Le POINT 7";

			PointOfInterest p8 = new PointOfInterest(0.18f, 0.92f, floor2);
            PointOfInterestDescription description8 = new PointOfInterestDescription("The Eighth :: Title"
                    , "A Summary about the eighth :: summary", "A Full Description about the eighth :: Description");
            p8.Description = description8;
            p8.NameEn = "POINT 8";
			p8.NameFr = "Le POINT 8";

			PointOfInterest p9 = new PointOfInterest(0.53f, 0.46f, floor5);
            PointOfInterestDescription description9 = new PointOfInterestDescription("The Ninth :: Title"
                    , "A Summary about the nineth :: summary", "A Full Description about the ninth :: Description");
            p9.Description = description9;

			Beacon beacon2 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 49800, 5890);
			PointOfInterest p10 = new PointOfInterest(0.53f, 0.76f, floor5);
            PointOfInterestDescription description10 = new PointOfInterestDescription("The Tenth :: Title"
                    , "A Summary about the tenth :: summary", "A Full Description about the tenth :: Description");
            p10.Description = description10;
            p10.Beacon = beacon2;

			PointOfInterest p11 = new PointOfInterest (0.53f, 0.46f, floor4);
			PointOfInterestDescription description11 = new PointOfInterestDescription("The Eleventh :: Title"
				, "A Summary about the eighth :: summary", "A Full Description about the eleventh :: Description");
			p11.Description = description11;
			p11.NameEn = "POINT 11";
			p11.NameFr = "Le POINT 11";

			PointOfInterest p12 = new PointOfInterest (0.73f, 0.16f, floor4);
			PointOfInterestDescription description12 = new PointOfInterestDescription("The Twelfth :: Title"
				, "A Summary about the eighth :: summary", "A Full Description about the twelfth :: Description");
			p12.Description = description12;
			p12.NameEn = "POINT 12";
			p12.NameFr = "Le POINT 12";


			storyline.AddMapElement(p1);
			storyline.AddMapElement (pot1);
			storyline.AddMapElement (p3);
			storyline.AddMapElement (p4);
			storyline.AddMapElement (p5);
			storyline.AddMapElement (p6);
			storyline.AddMapElement (p7);
			storyline.AddMapElement (p8);
			storyline.AddMapElement (p9);
			storyline.AddMapElement (p10);
			storyline.AddMapElement (p11);
			storyline.AddMapElement (p12);

			StoryLine story2 = new StoryLine("Story of Berliner","L'histoire de Berliner", "Kids","Enfants", "Description in english","Description en français", 60, Resource.Drawable.EmileBerliner);
			StoryLine story3 = new StoryLine("Pink Panther", "Panthère Rose","All Audience", "Toute Audience", "Description in english", "Description en français", 90, Resource.Drawable.Pink_Panther);
			StoryLine story4 = new StoryLine("The WW2", "La 2ième guerre Mondial", "Adults", "Adultes", "Description in english", "Description en français", 60, Resource.Drawable.army);
			StoryLine story5 = new StoryLine("The Detective", "Le Détective", "All Audience", "Toute Audience", "Description in english", "Description en français", 90, Resource.Drawable.detective);
			StoryLine story6 = new StoryLine("1940's Radio", "La radio de 1940", "All Audience", "Toute Audience", "Description in english", "Description en français", 30, Resource.Drawable.radio2);

			story2.Status = Status.InProgress;
			story3.Status = Status.IsVisited;

			Storylines.Add(storyline);
			Storylines.Add(story2);
			Storylines.Add(story3);
			Storylines.Add(story4);
			Storylines.Add(story5);
			Storylines.Add(story6);

			//Set the storyline for the explorer mode
			CurrentStoryline = storyline;

			StoryLine nicelyDrawn = new StoryLine("The Gramophone", "Le Gramophone","All Audience", "Toute Audience", "Description in english", "Description en français", 120 , Resource.Drawable.NipperTheDog);

			Beacon nicelyDrawnBeaconTest = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 00000, 00000);
			PointOfInterestDescription nicelyDrawnBeaconDescriptionTest = new PointOfInterestDescription("---", "---", "---");

			PointOfInterest poi0 = new PointOfInterest(0.519f, 0.580f, floor2);
			poi0.Description = description1;
			poi0.NameEn = "POINT 1";
			poi0.NameFr = "Le POINT 1";
			poi0.Visited = false;
			poi0.Beacon = beacon1;

			WayPoint waypoint3 = new WayPoint(0.720f, 0.577f, floor2);

			PointOfInterest poi1 = new PointOfInterest(0.745f, 0.544f, floor2);
			poi1.Description = description3;
			poi1.NameEn = "POINT 2";
			poi1.NameFr = "Le POINT 2";
			poi1.Visited = false;
			poi1.Beacon = beacon3;

			WayPoint waypoint4 = new WayPoint(0.754f, 0.538f, floor2);

			PointOfInterest poi2 = new PointOfInterest(0.762f, 0.522f, floor2);
			poi2.Description = description4;
			poi2.NameEn = "POINT 3";
			poi2.NameFr = "Le POINT 2";
			poi2.Visited = false;
			poi2.Beacon = beacon2;

			PointOfInterest poi3 = new PointOfInterest(0.777f, 0.446f, floor2);
			poi3.Description = nicelyDrawnBeaconDescriptionTest;
			poi3.NameEn = "---";
			poi3.NameFr = "---";
			poi3.Visited = false;
			poi3.Beacon = nicelyDrawnBeaconTest;

			WayPoint waypoint5 = new WayPoint(0.769f, 0.549f, floor2);
			WayPoint waypoint6 = new WayPoint(0.796f, 0.605f, floor2);
			WayPoint waypoint7 = new WayPoint(0.712f, 0.615f, floor2);

			PointOfInterest poi4 = new PointOfInterest(0.656f, 0.835f, floor2);
			poi4.Description = nicelyDrawnBeaconDescriptionTest;
			poi4.NameEn = "---";
			poi4.NameFr = "---";
			poi4.Visited = false;
			poi4.Beacon = nicelyDrawnBeaconTest;

			WayPoint waypoint8 = new WayPoint(0.645f, 0.907f, floor2);

			PointOfInterest poi5 = new PointOfInterest(0.592f, 0.907f, floor2);
			poi5.Description = nicelyDrawnBeaconDescriptionTest;
			poi5.NameEn = "---";
			poi5.NameFr = "---";
			poi5.Visited = false;
			poi5.Beacon = nicelyDrawnBeaconTest;

			PointOfInterest poi6 = new PointOfInterest(0.333f, 0.901f, floor2);
			poi6.Description = nicelyDrawnBeaconDescriptionTest;
			poi6.NameEn = "---";
			poi6.NameFr = "---";
			poi6.Visited = false;
			poi6.Beacon = nicelyDrawnBeaconTest;

			WayPoint waypoint9 = new WayPoint(0.176f, 0.894f, floor2);

			PointOfInterest poi7 = new PointOfInterest(0.173f, 0.853f, floor2);
			poi7.Description = nicelyDrawnBeaconDescriptionTest;
			poi7.NameEn = "---";
			poi7.NameFr = "---";
			poi7.Visited = false;
			poi7.Beacon = nicelyDrawnBeaconTest;

			WayPoint waypoint10 = new WayPoint(0.174f, 0.612f, floor2);

			PointOfInterest poi8 = new PointOfInterest(0.080f, 0.612f, floor2);
			poi8.Description = nicelyDrawnBeaconDescriptionTest;
			poi8.NameEn = "---";
			poi8.NameFr = "---";
			poi8.Visited = false;
			poi8.Beacon = nicelyDrawnBeaconTest;

			nicelyDrawn.AddMapElement(poi0);
			nicelyDrawn.AddMapElement(waypoint3);
			nicelyDrawn.AddMapElement(poi1);
			nicelyDrawn.AddMapElement(waypoint4);
			nicelyDrawn.AddMapElement(poi2);
			nicelyDrawn.AddMapElement(poi3);
			nicelyDrawn.AddMapElement(waypoint5);
			nicelyDrawn.AddMapElement(waypoint6);
			nicelyDrawn.AddMapElement(waypoint7);
			nicelyDrawn.AddMapElement(poi4);
			nicelyDrawn.AddMapElement(waypoint8);
			nicelyDrawn.AddMapElement(poi5);
			nicelyDrawn.AddMapElement(poi6);
			nicelyDrawn.AddMapElement(waypoint9);
			nicelyDrawn.AddMapElement(poi7);
			nicelyDrawn.AddMapElement(waypoint10);
			nicelyDrawn.AddMapElement(poi8);

			Storylines.Add (nicelyDrawn);

			////////////////////////////////////////////
			//make storyline 6 a demo for shortest path
			//story6.MapElements = Exposeum.Utilities.DeepCloneUtility.Clone(nicelyDrawn.MapElements);
			story6.MapElements = nicelyDrawn.MapElements;
			story6.PoiList = nicelyDrawn.PoiList;
			story6.PoiList.Last ().Beacon = story6.PoiList [1].Beacon;
			story6.PoiList [1].Beacon = nicelyDrawnBeaconTest;

			story6.MapElements.Reverse();
			story6.PoiList.Reverse ();
			for (int i = 0; i < 16; i++) {
				story6.MapElements [i].Visited = true;
			}
			story6.Status = Status.InProgress;
			///////////////////////////////////////////

		}


		public void SetActiveShortestPath(Path path){
			_activeShortestPath = path;
		}

		public Path GetActiveShortestPath(){
			return _activeShortestPath;
		}
    }
}
