using System.Collections.Generic;
using Exposeum.Models;
using Java.Util;

namespace Exposeum.Services.Service_Providers
{
	public class StoryLineServiceProvider: IStoryLineService
	{
		private readonly Map _mapInstance;
		private readonly BeaconFinder _beaconFinder;
		private StoryLine _genericStoryLine;

		public StoryLineServiceProvider(){
			_mapInstance = Map.GetInstance ();
			_beaconFinder = BeaconFinder.GetInstance ();



			//TODO: to remove
			Beacon beacon1 = new Beacon (UUID.FromString ("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
			Beacon beacon2 = new Beacon (UUID.FromString ("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 49800, 5890);
			Beacon beacon3 = new Beacon (UUID.FromString ("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 55339, 19185);

			PointOfInterest p1 = new PointOfInterest (0.53f, 0.46f, _mapInstance.Floors [0]);
			p1.Beacon = beacon1;

			WayPoint pot1 = new WayPoint (0.60f, 0.82f, _mapInstance.Floors [0]);

			PointOfInterest p2 = new PointOfInterest (0.65f, 0.87f, _mapInstance.Floors [0]);
			p2.Beacon = beacon2;


			PointOfInterest p3 = new PointOfInterest (0.1f, 0.92f, _mapInstance.Floors [0]);		
			p3.Beacon = beacon3;
			PointOfInterest p4 = new PointOfInterest (0.40f, 0.42f, _mapInstance.Floors [0]);
			PointOfInterest p5 = new PointOfInterest (0.30f, 0.12f, _mapInstance.Floors [0]);
			PointOfInterest p6 = new PointOfInterest (0.48f, 0.12f, _mapInstance.Floors [0]);
			PointOfInterest p7 = new PointOfInterest (0.38f, 0.62f, _mapInstance.Floors [1]);
			PointOfInterest p8 = new PointOfInterest (0.18f, 0.92f, _mapInstance.Floors [1]);
			PointOfInterest p9 = new PointOfInterest (0.53f, 0.46f, _mapInstance.Floors [3]);
			PointOfInterest p10 = new PointOfInterest (0.53f, 0.76f, _mapInstance.Floors [3]);
			PointOfInterest p11 = new PointOfInterest (0.53f, 0.46f, _mapInstance.Floors [2]);
			PointOfInterest p12 = new PointOfInterest (0.73f, 0.16f, _mapInstance.Floors [2]);


			_genericStoryLine = new StoryLine
			{
				StorylineId = 0,
				Duration = 120,
				ImageId = Resource.Drawable.NipperTheDog
			};

			_genericStoryLine.AddMapElement(p1);
			_genericStoryLine.AddMapElement (pot1);
			_genericStoryLine.AddMapElement (p2);
			_genericStoryLine.AddMapElement (p3);
			_genericStoryLine.AddMapElement (p4);
			_genericStoryLine.AddMapElement (p5);
			_genericStoryLine.AddMapElement (p6);
			_genericStoryLine.AddMapElement (p7);
			_genericStoryLine.AddMapElement (p8);
			_genericStoryLine.AddMapElement (p9);
			_genericStoryLine.AddMapElement (p10);
			_genericStoryLine.AddMapElement (p11);
			_genericStoryLine.AddMapElement (p12);
		}

		public List<StoryLine> GetStoryLines (){
			return _mapInstance.Storylines;
		}

		public StoryLine GetActiveStoryLine (){
			return _mapInstance.CurrentStoryline;
		}

		public void SetActiveStoryLine (StoryLine storyline){
			_mapInstance.CurrentStoryline = storyline;
			_mapInstance.SetActiveShortestPath (null);
		}

		public StoryLine GetStoryLineById(int id){
			//Loop through all the storylines and return the one
			//with the matching id
			foreach(StoryLine storyLine in GetStoryLines()){

				if (storyLine.StorylineId == id)
					return storyLine;

			}

			return null;
		}

		//TODO: implement properly
		public StoryLine GetGenericStoryLine(){
			return _genericStoryLine;
		}
	}
}

