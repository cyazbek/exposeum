﻿using System;
using System.Collections.Generic;
using Java.Util;
using Android.Graphics.Drawables;
using System.Linq;
using Exposeum.Observers;

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
        
		private void SeedData()
		{
            //setting up Floors
		    Drawable
		        floorplan1 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_1),
		        floorplan2 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_2),
		        floorplan3 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_3),
		        floorplan4 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_4),
		        floorplan5 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_5);

		    Floor floor1 = new Floor(floorplan1),
		        floor2 = new Floor(floorplan2),
		        floor3 = new Floor(floorplan3),
		        floor4 = new Floor(floorplan4),
                floor5 = new Floor(floorplan5);

            Floors = new List<Floor>();

            Floors.Add(floor1);
            Floors.Add(floor2);
            Floors.Add(floor3);
            Floors.Add(floor4);
            Floors.Add(floor5);

            CurrentFloor = floor1;

            //setup PointOfInterestDecriptions
            PointOfInterestDescription DescriptionEn1 = new PointOfInterestDescription()
		    {
		        Id = 1,
		        Language = Language.En,
		        Summary = "This Summary of POI1",
		        Description = "This is a Description OF POI1",
		        Title = "Title for POI1"
		    };

            PointOfInterestDescription DescriptionFR1 = new PointOfInterestDescription()
            {
                Id = 1,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI1",
                Description = "Voici une description de POI1",
                Title = "Titre pour POI1"
            };

            PointOfInterestDescription DescriptionEn2 = new PointOfInterestDescription()
            {
                Id = 2,
                Language = Language.En,
                Summary = "This Summary of POI2",
                Description = "This is a Description OF POI2",
                Title = "Title for POI1"
            };

            PointOfInterestDescription DescriptionFR2 = new PointOfInterestDescription()
            {
                Id = 2,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI2",
                Description = "Voici une description de POI2",
                Title = "Titre pour POI2"
            };

            PointOfInterestDescription DescriptionEn3 = new PointOfInterestDescription()
            {
                Id = 3,
                Language = Language.En,
                Summary = "This Summary of POI3",
                Description = "This is a Description OF POI3",
                Title = "Title for POI3"
            };

            PointOfInterestDescription DescriptionFR3 = new PointOfInterestDescription()
            {
                Id = 3,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI3",
                Description = "Voici une description de POI3",
                Title = "Titre pour POI3"
            };

            PointOfInterestDescription DescriptionEn4 = new PointOfInterestDescription()
            {
                Id = 4,
                Language = Language.En,
                Summary = "This Summary of POI4",
                Description = "This is a Description OF POI4",
                Title = "Title for POI4"
            };

            PointOfInterestDescription DescriptionFR4 = new PointOfInterestDescription()
            {
                Id = 4,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI4",
                Description = "Voici une description de POI4",
                Title = "Titre pour POI4"
            };

            PointOfInterestDescription DescriptionEn5 = new PointOfInterestDescription()
            {
                Id = 5,
                Language = Language.En,
                Summary = "This Summary of POI5",
                Description = "This is a Description OF POI5",
                Title = "Title for POI5"
            };
            PointOfInterestDescription DescriptionFR5 = new PointOfInterestDescription()
            {
                Id = 5,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI5",
                Description = "Voici une description de POI5",
                Title = "Titre pour POI5"
            };

            PointOfInterestDescription DescriptionEn6 = new PointOfInterestDescription()
            {
                Id = 6,
                Language = Language.En,
                Summary = "This Summary of POI6",
                Description = "This is a Description OF POI6",
                Title = "Title for POI6"
            };
            PointOfInterestDescription DescriptionFR6 = new PointOfInterestDescription()
            {
                Id = 6,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI6",
                Description = "Voici une description de POI6",
                Title = "Titre pour POI6"
            };
            PointOfInterestDescription DescriptionEn7 = new PointOfInterestDescription()
            {
                Id = 7,
                Language = Language.En,
                Summary = "This Summary of POI7",
                Description = "This is a Description OF POI7",
                Title = "Title for POI7"
            };
            PointOfInterestDescription DescriptionFR7 = new PointOfInterestDescription()
            {
                Id = 7,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI7",
                Description = "Voici une description de POI7",
                Title = "Titre pour POI7"
            };
            PointOfInterestDescription DescriptionEn8 = new PointOfInterestDescription()
            {
                Id = 8,
                Language = Language.En,
                Summary = "This Summary of POI8",
                Description = "This is a Description OF POI8",
                Title = "Title for POI8"
            };
            PointOfInterestDescription DescriptionFR8 = new PointOfInterestDescription()
            {
                Id = 8,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI8",
                Description = "Voici une description de POI8",
                Title = "Titre pour POI8"
            };
            PointOfInterestDescription DescriptionEn9 = new PointOfInterestDescription()
            {
                Id = 9,
                Language = Language.En,
                Summary = "This Summary of POI9",
                Description = "This is a Description OF POI9",
                Title = "Title for POI9"
            };
            PointOfInterestDescription DescriptionFR9 = new PointOfInterestDescription()
            {
                Id = 9,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI9",
                Description = "Voici une description de POI9",
                Title = "Titre pour POI9"
            };
            PointOfInterestDescription DescriptionEn10 = new PointOfInterestDescription()
            {
                Id = 10,
                Language = Language.En,
                Summary = "This Summary of POI10",
                Description = "This is a Description OF POI10",
                Title = "Title for POI10"
            };
            PointOfInterestDescription DescriptionFR10 = new PointOfInterestDescription()
            {
                Id = 10,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI10",
                Description = "Voici une description de POI10",
                Title = "Titre pour POI10"
            };
            PointOfInterestDescription DescriptionEn11 = new PointOfInterestDescription()
            {
                Id = 11,
                Language = Language.En,
                Summary = "This Summary of POI11",
                Description = "This is a Description OF POI11",
                Title = "Title for POI11"
            };
            PointOfInterestDescription DescriptionFR11 = new PointOfInterestDescription()
            {
                Id = 11,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI11",
                Description = "Voici une description de POI11",
                Title = "Titre pour POI11"
            };
            PointOfInterestDescription DescriptionEn12 = new PointOfInterestDescription()
            {
                Id = 12,
                Language = Language.En,
                Summary = "This Summary of POI12",
                Description = "This is a Description OF POI12",
                Title = "Title for POI12"
            };
            PointOfInterestDescription DescriptionFR12 = new PointOfInterestDescription()
            {
                Id = 12,
                Language = Language.Fr,
                Summary = "Voici un sommaire de POI12",
                Description = "Voici une description de POI12",
                Title = "Titre pour POI12"
            };

            //set up Beacons
            Beacon beacon1 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
            Beacon beacon2 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 49800, 5890);
            Beacon beacon3 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 55339, 19185);
            Beacon nicelyDrawnBeaconTest = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 00000, 00000);


            PointOfInterest p1 = new PointOfInterest(0.53f, 0.46f, floor1);
		    p1.Description = DescriptionEn1;
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

			
			PointOfInterest p3 = new PointOfInterest(0.1f, 0.92f, floor1);
            p3.Description = DescriptionEn3;
			p3.Visited = false;
			p3.Beacon = beacon3;

			PointOfInterest p4 = new PointOfInterest(0.40f, 0.42f, floor1);
            p4.Description = DescriptionEn4;

			PointOfInterest p5 = new PointOfInterest(0.30f, 0.12f, floor1);
            p5.Description = DescriptionEn5;

			PointOfInterest p6 = new PointOfInterest(0.48f, 0.12f, floor1);
            p6.Description = DescriptionEn6;

			PointOfInterest p7 = new PointOfInterest(0.38f, 0.62f, floor2);
            p7.Description = DescriptionEn7;

			PointOfInterest p8 = new PointOfInterest(0.18f, 0.92f, floor2);
            p8.Description = DescriptionEn8;

			PointOfInterest p9 = new PointOfInterest(0.53f, 0.46f, floor5);
            p9.Description = DescriptionEn9;

			
			PointOfInterest p10 = new PointOfInterest(0.53f, 0.76f, floor5);
		    p10.Description = DescriptionEn10;

			PointOfInterest p11 = new PointOfInterest (0.53f, 0.46f, floor4);
			p11.Description = DescriptionEn11;

			PointOfInterest p12 = new PointOfInterest (0.73f, 0.16f, floor4);
			p12.Description = DescriptionEn12;

		    StorylineDescription storyDescriptionEn1 = new StorylineDescription
		    {
		        StoryLineDescriptionId = 1,
		        Title = "Nipper the dog",
		        IntendedAudience = "Adults",
		        Description = "A walk through different sections of RCA Victor’s production site, constructed over a period of roughly 25 years. This tour takes you through three different time zones, the 1920s, back when Montreal was the world’s largest grain hub and Canada’s productive power house, Montreal’s entertainment rich 1930s and 1943, when production at RCA Victor diversified to serve military needs.",
		        Language = Language.En
		    };
            StorylineDescription storyDescriptionFr1 = new StorylineDescription
            {
                StoryLineDescriptionId = 1,
                Title = "Le Chien Nipper",
                IntendedAudience = "Adultes",
                Description = "Une promenade à travers les différentes sections du site de production de RCA Victor, construit sur une période d’environ 25 ans. Ce circuit vous emmène à travers trois fuseaux horaires différents, les années 1920, époque où Montréal était le plus grand centre de grains du monde et la maison de puissance productive du Canada, de divertissement riches années 1930 à Montréal et 1943, lorsque la production chez RCA Victor diversifiée pour répondre aux besoins militaires.",
                Language = Language.Fr
            };

            StorylineDescription storyDescriptionEn2 = new StorylineDescription
            {
                StoryLineDescriptionId = 2,
                Title = "Story of Berliner",
                IntendedAudience = "Kids",
                Description = "Description Berliner",
                Language = Language.En
            };
            StorylineDescription storyDescriptionFr2 = new StorylineDescription
            {
                StoryLineDescriptionId = 2,
                Title = "L'histoire de Berliner",
                IntendedAudience = "Enfants",
                Description = "La Description 2",
                Language = Language.Fr
            };

            StorylineDescription storyDescriptionEn3 = new StorylineDescription
            {
                StoryLineDescriptionId = 3,
                Title = "Pink Panther",
                IntendedAudience = "All Audience",
                Description = "Description En",
                Language = Language.En
            };
            StorylineDescription storyDescriptionFr3 = new StorylineDescription
            {
                StoryLineDescriptionId = 3,
                Title = "Panthère Rose",
                IntendedAudience = "Tout Ages",
                Description = "Description Fr",
                Language = Language.Fr
            };

            StorylineDescription storyDescriptionEn4 = new StorylineDescription
            {
                StoryLineDescriptionId = 4,
                Title = "W.W.2",
                IntendedAudience = "Adults",
                Description = "Description in English",
                Language = Language.En
            };
            StorylineDescription storyDescriptionFr4 = new StorylineDescription
            {
                StoryLineDescriptionId = 4,
                Title = "La 2EME Guerre",
                IntendedAudience = "Adultes",
                Description = "Description en Français",
                Language = Language.Fr
            };

            StorylineDescription storyDescriptionEn5 = new StorylineDescription
            {
                StoryLineDescriptionId = 5,
                Title = "The Detective",
                IntendedAudience = "All Audience",
                Description = "Description English",
                Language = Language.En
            };
            StorylineDescription storyDescriptionFr5 = new StorylineDescription
            {
                StoryLineDescriptionId = 5,
                Title = "Le Détéctive",
                IntendedAudience = "Toute Audience",
                Description = "Desciption en Français",
                Language = Language.Fr
            };

            StorylineDescription storyDescriptionEn6 = new StorylineDescription
            {
                StoryLineDescriptionId = 6,
                Title = "1940's Radio",
                IntendedAudience = "Adults",
                Description = "Description English",
                Language = Language.En
            };
            StorylineDescription storyDescriptionFr6 = new StorylineDescription
            {
                StoryLineDescriptionId = 6,
                Title = "La radio des '40",
                IntendedAudience = "Toute Audience",
                Description = "Description en Français",
                Language = Language.Fr
            };

            StorylineDescription storyDescriptionEn7 = new StorylineDescription
            {
                StoryLineDescriptionId = 6,
                Title = "Gramophone",
                IntendedAudience = "Kids",
                Description = "Description in English",
                Language = Language.En
            };
            StorylineDescription storyDescriptionFr7 = new StorylineDescription
            {
                StoryLineDescriptionId = 6,
                Title = "Le Gramophone",
                IntendedAudience = "Enfants",
                Description = "Description en Français",
                Language = Language.Fr
            };



            //creating Nipper the dog StoryLine 
            StoryLine storyline = new StoryLine
		    {
                StorylineId = 1,
                StorylineDescription = storyDescriptionEn1,
		        Duration = 120,
		        ImageId = Resource.Drawable.NipperTheDog
		    };

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


		    StoryLine story2 = new StoryLine
		    {
                StorylineId = 2,
                StorylineDescription = storyDescriptionEn2,
                Duration = 60,
		        ImageId = Resource.Drawable.EmileBerliner,
                Status = Status.InProgress
		    };

            StoryLine story3 = new StoryLine
            {
                StorylineId = 3,
                StorylineDescription = storyDescriptionEn3,
                Duration = 90,
                ImageId = Resource.Drawable.Pink_Panther
            };

            StoryLine story4 = new StoryLine
            {
                StorylineId = 4,
                StorylineDescription = storyDescriptionEn4,
                Duration = 60,
                ImageId = Resource.Drawable.army,
                Status = Status.IsVisited
            };

            StoryLine story5 = new StoryLine
            {
                StorylineId = 5,
                StorylineDescription = storyDescriptionEn5,
                Duration = 120,
                ImageId = Resource.Drawable.detective
            };

            StoryLine story6 = new StoryLine
            {
                StorylineId = 6,
                StorylineDescription = storyDescriptionEn6,
                Duration = 120,
                ImageId = Resource.Drawable.radio2
            };

            StoryLine nicelyDrawn = new StoryLine
            {
                StorylineId = 7,
                StorylineDescription = storyDescriptionEn7,
                Duration = 10,
                ImageId = Resource.Drawable.NipperTheDog
            };



            Storylines.Add(storyline);
			Storylines.Add(story2);
			Storylines.Add(story3);
			Storylines.Add(story4);
			Storylines.Add(story5);
			Storylines.Add(story6);

			//Set the storyline for the explorer mode
			CurrentStoryline = storyline;

			

			
			PointOfInterestDescription nicelyDrawnBeaconDescriptionTest = new PointOfInterestDescription("---", "---", "---");

			PointOfInterest poi0 = new PointOfInterest(0.519f, 0.580f, floor2);
			poi0.Description = DescriptionEn1;
			poi0.Visited = false;
			poi0.Beacon = beacon1;

			WayPoint waypoint3 = new WayPoint(0.720f, 0.577f, floor2);

			PointOfInterest poi1 = new PointOfInterest(0.745f, 0.544f, floor2);
			poi1.Description = DescriptionEn3;
			poi1.Visited = false;
			poi1.Beacon = beacon3;

			WayPoint waypoint4 = new WayPoint(0.754f, 0.538f, floor2);

			PointOfInterest poi2 = new PointOfInterest(0.762f, 0.522f, floor2);
			poi2.Description = DescriptionEn4;
			poi2.Visited = false;
			poi2.Beacon = beacon2;

			PointOfInterest poi3 = new PointOfInterest(0.777f, 0.446f, floor2);
			poi3.Description = nicelyDrawnBeaconDescriptionTest;
			poi3.Visited = false;
			poi3.Beacon = nicelyDrawnBeaconTest;

			WayPoint waypoint5 = new WayPoint(0.769f, 0.549f, floor2);
			WayPoint waypoint6 = new WayPoint(0.796f, 0.605f, floor2);
			WayPoint waypoint7 = new WayPoint(0.712f, 0.615f, floor2);

			PointOfInterest poi4 = new PointOfInterest(0.656f, 0.835f, floor2);
			poi4.Description = nicelyDrawnBeaconDescriptionTest;
			poi4.Visited = false;
			poi4.Beacon = nicelyDrawnBeaconTest;

			WayPoint waypoint8 = new WayPoint(0.645f, 0.907f, floor2);

			PointOfInterest poi5 = new PointOfInterest(0.592f, 0.907f, floor2);
			poi5.Description = nicelyDrawnBeaconDescriptionTest;
			poi5.Visited = false;
			poi5.Beacon = nicelyDrawnBeaconTest;

			PointOfInterest poi6 = new PointOfInterest(0.333f, 0.901f, floor2);
			poi6.Description = nicelyDrawnBeaconDescriptionTest;
			poi6.Visited = false;
			poi6.Beacon = nicelyDrawnBeaconTest;

			WayPoint waypoint9 = new WayPoint(0.176f, 0.894f, floor2);

			PointOfInterest poi7 = new PointOfInterest(0.173f, 0.853f, floor2);
			poi7.Description = nicelyDrawnBeaconDescriptionTest;
			poi7.Visited = false;
			poi7.Beacon = nicelyDrawnBeaconTest;

			WayPoint waypoint10 = new WayPoint(0.174f, 0.612f, floor2);

			PointOfInterest poi8 = new PointOfInterest(0.080f, 0.612f, floor2);
			poi8.Description = nicelyDrawnBeaconDescriptionTest;
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
