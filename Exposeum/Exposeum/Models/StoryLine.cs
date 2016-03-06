using System.Collections.Generic;
using Exposeum.Data;
using System;

namespace Exposeum.Models
{
    public class StoryLine
    {

        public int ImageId { get; set; }
        public Status currentStatus { get; set; }
        public string name_en {get; set;}
        public string name_fr { get; set; }
        public string audience_en { get; set; }
        public string audience_fr { get; set; }
        public string desc_en { get; set; }
        public string desc_fr { get; set; }
        public int duration { get; set; }
        public int ID { get; set; }
        public string imgPath { get; set; }
        public int FloorsCovered { get; set; }
        public List<MapElement> MapElements { get; set; } 
        public List<PointOfInterest> poiList { get; set; }
        private PointOfInterest _lastPointOfInterestVisited ;
        
        public StoryLine() 
        {
            this.poiList = new List<PointOfInterest>();
			this.MapElements = new List<MapElement> ();
			this.currentStatus = Status.isNew;
        }

        public StoryLine(int ID)
        {
            this.ID = ID;
			poiList = new List<PointOfInterest>();
			MapElements = new List<MapElement> ();
			this.currentStatus = Status.isNew;
        }

		public StoryLine(string name_en, string name_fr, string audience_en, string audience_fr, string description_en, string description_fr, int duration, int imageId)
		{
			this.name_en = name_en;
			this.name_fr = name_fr;
			this.audience_en = audience_en;
			this.audience_fr = audience_fr;
			this.desc_en = description_en;
			this.desc_fr = description_fr;
			this.duration = duration;
			this.ImageId = imageId;

			this.MapElements = new List<MapElement> ();
			this.poiList = new List<PointOfInterest>();
			this.currentStatus = Status.isNew;
		}


		/// <summary>
		/// DEPRECATED: use addMapElement() instead
		/// </summary>
        public void addPoi(PointOfInterest poi)
        {
			MapElements.Add (poi);
            poiList.Add (poi);
        }

        public PointOfInterest GetLastVisitedPointOfInterest(){
            return this._lastPointOfInterestVisited;
        }

		public void AddMapElement(MapElement e)
		{
			MapElements.Add(e);
			//to be removed when poiList is removed
			if(e.GetType() == typeof(PointOfInterest))
				poiList.Add (e as PointOfInterest);
		}

        public void SetLastPointOfInterestVisited(PointOfInterest lastPoiVisited)
        {
            this._lastPointOfInterestVisited = lastPoiVisited;

        }

		/// <summary>
		/// This method will update the progress of the storyline using the passed node.
		/// </summary>
		public void updateProgress(MapElement mapElement){

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
					_lastPointOfInterestVisited = rightBoundLinkedNode.Value;

				//finally, of this node is the last node of the tour, set the tour as completed
				if(rightBoundLinkedNode.Next == null)
					this.currentStatus = Status.isVisited;
		    }
		}

        public PointOfInterest findPOI(EstimoteSdk.Beacon beacon)
        {
            return poiList.Find(x => x.beacon.compareBeacon(beacon));
        }

        public bool hasBeacon(EstimoteSdk.Beacon beacon)
        {
            foreach (var poi in poiList)
            {
                if (poi.beacon.compareBeacon(beacon))
                    return true;
            }
            return false;
        }

        public int getSize()
        {
            return poiList.Count;
        }

        public string getName()
        {
            string lang = Language.getLanguage();
            string storyName;

            if (lang.Equals("fr"))
                storyName = this.name_fr;
            else
                storyName = this.name_en;

            return storyName;
        }

        public string getDescription()
        {
            string lang = Language.getLanguage();
            string storyDesc;

            if (lang.Equals("fr"))
                storyDesc = this.desc_fr;
            else
                storyDesc = this.desc_en;

            return storyDesc;
        }

        public string getAudience()
        {
            string lang = Language.getLanguage();
            string storyAudience;

            if (lang.Equals("fr"))
                storyAudience = this.audience_fr;
            else
                storyAudience = this.audience_en;

            return storyAudience;
        }

        public void convertFromData(StoryData story)
        {
            this.audience_en = story.audience_en;
            this.audience_fr = story.audience_fr;
            this.desc_en = story.desc_en;
            this.desc_fr = story.desc_fr;
            this.duration = story.duration;
            this.name_en = story.name_en;
            this.name_fr = story.name_fr;
        }

    }
}