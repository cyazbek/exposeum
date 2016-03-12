using System.Collections.Generic;
using System;

namespace Exposeum.Models
{
    public class StoryLine
    {

        public int ImageId { get; set; }
        public Status CurrentStatus { get; set; }
        public string NameEn {get; set;}
        public string NameFr { get; set; }
        public string AudienceEn { get; set; }
        public string AudienceFr { get; set; }
        public string DescEn { get; set; }
        public string DescFr { get; set; }
        public int Duration { get; set; }
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int FloorsCovered { get; set; }
        public List<MapElement> MapElements { get; set; } 
        public List<PointOfInterest> PoiList { get; set; }
        private PointOfInterest _lastPointOfInterestVisited ;
        
        public StoryLine() 
        {
            this.PoiList = new List<PointOfInterest>();
			this.MapElements = new List<MapElement> ();
			this.CurrentStatus = Status.IsNew;
        }

        public StoryLine(int id)
        {
            this.Id = id;
			PoiList = new List<PointOfInterest>();
			MapElements = new List<MapElement> ();
			this.CurrentStatus = Status.IsNew;
        }

		public StoryLine(string nameEn, string nameFr, string audienceEn, string audienceFr, string descriptionEn, string descriptionFr, int duration, int imageId)
		{
			this.NameEn = nameEn;
			this.NameFr = nameFr;
			this.AudienceEn = audienceEn;
			this.AudienceFr = audienceFr;
			this.DescEn = descriptionEn;
			this.DescFr = descriptionFr;
			this.Duration = duration;
			this.ImageId = imageId;

			this.MapElements = new List<MapElement> ();
			this.PoiList = new List<PointOfInterest>();
			this.CurrentStatus = Status.IsNew;
		}


		/// <summary>
		/// DEPRECATED: use addMapElement() instead
		/// </summary>
        public void AddPoi(PointOfInterest poi)
        {
			MapElements.Add (poi);
            PoiList.Add (poi);
        }

        public PointOfInterest GetLastVisitedPointOfInterest(){
            return this._lastPointOfInterestVisited;
        }

		public void AddMapElement(MapElement e)
		{
			MapElements.Add(e);
			//to be removed when poiList is removed
			if(e.GetType() == typeof(PointOfInterest))
				PoiList.Add (e as PointOfInterest);
		}

        public void SetLastPointOfInterestVisited(PointOfInterest lastPoiVisited)
        {
            this._lastPointOfInterestVisited = lastPoiVisited;

        }

		/// <summary>
		/// This method will update the progress of the storyline using the passed node.
		/// </summary>
		public void UpdateProgress(MapElement mapElement){

			//convert ListMapElements to a LinkedList
			LinkedList<MapElement> nodeList = new LinkedList<MapElement> (MapElements);

			//Find the supplied mapElement in the LinkedList
			LinkedListNode<MapElement> rightBoundLinkedNode = nodeList.Find (mapElement);

		    if (rightBoundLinkedNode != null)
		    {
				LinkedListNode<MapElement> currentLinkedNode = rightBoundLinkedNode.Previous;
				Stack<MapElement> nodeStack = new Stack<MapElement>();

		        nodeStack.Push (rightBoundLinkedNode.Value);

		        //first pass, find the leftBound
		        while(currentLinkedNode != null && !currentLinkedNode.Value.Visited) {

		            //if the node is of type PointOfInterest then it means that the user skipped a POI, throw an exception
		            if (currentLinkedNode.Value.GetType() != typeof (PointOfInterest))
		            {
		                nodeStack.Push(currentLinkedNode.Value);
		                currentLinkedNode = currentLinkedNode.Previous;
		            }
		            else
		            {
		                throw new PointOfInterestNotVisitedException(
							"There is an unvisited POI between the current POI and the previously visited POI.", (PointOfInterest)currentLinkedNode.Value);
		            }
		        }

		        //Now that the leftbound was found, pop the stack and set as visited all the Nodes in it
		        while (nodeStack.Count > 0) {

					MapElement currentNode = nodeStack.Pop();
		            currentNode.SetVisited();
		        }

				if (rightBoundLinkedNode.Value.GetType() != typeof (PointOfInterest))
					_lastPointOfInterestVisited = (PointOfInterest)rightBoundLinkedNode.Value;

				//finally, of this node is the last node of the tour, set the tour as completed
				if(rightBoundLinkedNode.Next == null)
					this.CurrentStatus = Status.IsVisited;
		    }
		}

        public PointOfInterest FindPoi(EstimoteSdk.Beacon beacon)
        {
            return PoiList.Find(x => x.Beacon.CompareBeacon(beacon));
        }

        public bool HasBeacon(EstimoteSdk.Beacon beacon)
        {
            foreach (var poi in PoiList)
            {
                if (poi.Beacon.CompareBeacon(beacon))
                    return true;
            }
            return false;
        }

        public int GetSize()
        {
            return PoiList.Count;
        }

        public string GetName()
        {
            Language lang = User.GetInstance()._language;
            string storyName;

            if (lang.Equals(Language.FR))
                storyName = this.NameFr;
            else
                storyName = this.NameEn;

            return storyName;
        }

        public string GetDescription()
        {
            Language lang = User.GetInstance()._language;
            string storyDesc;

            if (lang.Equals(Language.FR))
                storyDesc = this.DescFr;
            else
                storyDesc = this.DescEn;

            return storyDesc;
        }

        public string GetAudience()
        {
            Language lang = User.GetInstance()._language;
            string storyAudience;
            if (lang.Equals(Language.FR))
                storyAudience = AudienceFr; 
            else
                storyAudience = this.AudienceEn;

            return storyAudience;
        }

       

    }
}