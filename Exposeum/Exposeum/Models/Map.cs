using System;
using System.Collections.Generic;
using Android.Content.Res;
using Java.Util;
using Android.Graphics.Drawables;

namespace Exposeum.Models
{
	public class Map
	{
		private List<Floor> _floors;
		private Floor _currentFloor;
        private StoryLine _currentStoryline;
		private List<MapElement> _elements;
		private List<Edge> _edges;
        private List<StoryLine> _storyLines;

        public Map ()
		{
			_storyLines = new List<StoryLine>();
			seedData ();
        }
        
		private void seedData(){

            Drawable floorplan1, floorplan2, floorplan3, floorplan4, floorplan5;

            try
            {
                floorplan1 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_1);
                floorplan2 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_2);
                floorplan3 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_3);
                floorplan4 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_4);
                floorplan5 = Android.App.Application.Context.Resources.GetDrawable(Resource.Drawable.floor_5);
            }
            catch(Exception e)
            {
                floorplan1 = new ColorDrawable();
                floorplan2 = new ColorDrawable();
                floorplan3 = new ColorDrawable();
                floorplan4 = new ColorDrawable();
                floorplan5 = new ColorDrawable();
            }

			StoryLine storyline = new StoryLine("Nipper the dog", "Le Chien Nipper","Adults", "Adultes", "A walk through different sections of RCA Victor’s production site, constructed over a period of roughly 25 years. This tour takes you through three different time zones, the 1920s, back when Montreal was the world’s largest grain hub and Canada’s productive power house, Montreal’s entertainment rich 1930s and 1943, when production at RCA Victor diversified to serve military needs.", "Une promenade à travers les différentes sections du site de production de RCA Victor, construit sur une période d’environ 25 ans. Ce circuit vous emmène à travers trois fuseaux horaires différents, les années 1920, époque où Montréal était le plus grand centre de grains du monde et la maison de puissance productive du Canada, de divertissement riches années 1930 à Montréal et 1943, lorsque la production chez RCA Victor diversifiée pour répondre aux besoins militaires.", 120 , Resource.Drawable.NipperTheDog);

            Floor floor1 = new Floor(floorplan1);
            Floor floor2 = new Floor(floorplan2);
            Floor floor3 = new Floor(floorplan3);
            Floor floor4 = new Floor(floorplan4);
            Floor floor5 = new Floor(floorplan5);

			_floors = new List<Floor> ();

			_floors.Add (floor1);
			_floors.Add (floor2);
			_floors.Add (floor3);
			_floors.Add (floor4);
			_floors.Add (floor5);

			_currentFloor = floor1;

			//set up POIs

			Beacon beacon1 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
			PointOfInterest p1 = new PointOfInterest(0.53f, 0.46f, floor1);
            PointOfInterestDescription description1 = new PointOfInterestDescription("The First :: Title"
                    , "A Summary about the first :: summary", "A Full Description about the first :: Description");
		    p1.description = description1;
            p1.name_en = "POINT 1";
			p1.name_fr = "Le POINT 1";
            p1.Visited = false;
			p1.beacon = beacon1;

			PointOfTravel pot1 = new PointOfTravel(0.60f, 0.82f, floor1);

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
            p3.description = description3;
            p3.name_en = "POINT 3";
			p3.name_fr = "Le POINT 3";
			p3.Visited = false;
			p3.beacon = beacon3;

			PointOfInterest p4 = new PointOfInterest(0.40f, 0.42f, floor1);
            PointOfInterestDescription description4 = new PointOfInterestDescription("The Fourth :: Title"
                    , "A Summary about the fourth :: summary", "A Full Description about the fourth :: Description");
            p4.description = description4;
            p4.name_en = "POINT 4";
			p4.name_fr = "Le POINT 4";

			PointOfInterest p5 = new PointOfInterest(0.30f, 0.12f, floor1);
            PointOfInterestDescription description5 = new PointOfInterestDescription("The Fifth :: Title"
                    , "A Summary about the fifth :: summary", "A Full Description about the fifth :: Description");
            p5.description = description5;
            p5.name_en = "POINT 5";
			p5.name_fr = "Le POINT 5";

			PointOfInterest p6 = new PointOfInterest(0.48f, 0.12f, floor1);
            PointOfInterestDescription description6 = new PointOfInterestDescription("The Sixth :: Title"
                    , "A Summary about the sixth :: summary", "A Full Description about the sixth :: Description");
            p6.description = description6;
            p6.name_en = "POINT 6";
			p6.name_fr = "Le POINT 6";

			PointOfInterest p7 = new PointOfInterest(0.38f, 0.62f, floor2);
            PointOfInterestDescription description7 = new PointOfInterestDescription("The Seventh :: Title"
                    , "A Summary about the seventh :: summary", "A Full Description about the seventh :: Description");
            p7.description = description7;
            p7.name_en = "POINT 7";
			p7.name_fr = "Le POINT 7";

			PointOfInterest p8 = new PointOfInterest(0.18f, 0.92f, floor2);
            PointOfInterestDescription description8 = new PointOfInterestDescription("The Eighth :: Title"
                    , "A Summary about the eighth :: summary", "A Full Description about the eighth :: Description");
            p8.description = description8;
            p8.name_en = "POINT 8";
			p8.name_fr = "Le POINT 8";

			PointOfInterest p9 = new PointOfInterest(0.53f, 0.46f, floor5);
            PointOfInterestDescription description9 = new PointOfInterestDescription("The Ninth :: Title"
                    , "A Summary about the nineth :: summary", "A Full Description about the ninth :: Description");
            p9.description = description9;

			Beacon beacon2 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 49800, 5890);
			PointOfInterest p10 = new PointOfInterest(0.53f, 0.76f, floor5);
            PointOfInterestDescription description10 = new PointOfInterestDescription("The Tenth :: Title"
                    , "A Summary about the tenth :: summary", "A Full Description about the tenth :: Description");
            p10.description = description10;
            p10.beacon = beacon2;

			PointOfInterest p11 = new PointOfInterest (0.53f, 0.46f, floor4);
			PointOfInterestDescription description11 = new PointOfInterestDescription("The Eleventh :: Title"
				, "A Summary about the eighth :: summary", "A Full Description about the eleventh :: Description");
			p11.description = description11;
			p11.name_en = "POINT 11";
			p11.name_fr = "Le POINT 11";

			PointOfInterest p12 = new PointOfInterest (0.73f, 0.16f, floor4);
			PointOfInterestDescription description12 = new PointOfInterestDescription("The Twelfth :: Title"
				, "A Summary about the eighth :: summary", "A Full Description about the twelfth :: Description");
			p12.description = description12;
			p12.name_en = "POINT 12";
			p12.name_fr = "Le POINT 12";


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

			story2.currentStatus = Status.inProgress;
			story3.currentStatus = Status.isVisited;

			this._storyLines.Add(storyline);
			this._storyLines.Add(story2);
			this._storyLines.Add(story3);
			this._storyLines.Add(story4);
			this._storyLines.Add(story5);
			this._storyLines.Add(story6);

			//Set the storyline for the explorer mode
			_currentStoryline = storyline;

		}

		public void SetCurrentFloor(Floor floor)
		{
			_currentFloor = floor;
		}

		public Floor CurrentFloor
		{
			get { return this._currentFloor; }
			set { this._currentFloor = value; }
		}

		public List<Floor> Floors
		{
			get { return this._floors; }
			set { this._floors = value; }
		}

		public StoryLine CurrentStoryline
		{
			get { return this._currentStoryline; }
			set { this._currentStoryline = value; }
		}
        public void addStoryLine(StoryLine storyline)
        {
            this._storyLines.Add(storyline);
        }
        public List<StoryLine> getStoryLineList
        {
            get { return this._storyLines; }
            set { this._storyLines = value; }
        }
    }
}
