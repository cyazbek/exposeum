
using System.Collections.Generic;
using Exposeum.Data;
using System;

namespace Exposeum.Models
{
    public class StoryLine
    {

        public string name_en {get; set;}
        public string name_fr { get; set; }
        public string audience_en { get; set; }
        public string audience_fr { get; set; }
        public string desc_en { get; set; }
        public string desc_fr { get; set; }
        public string duration { get; set; }
        public int ID { get; set; }
        public string imgPath { get; set; }
        public int FloorsCovered { get; set; }
        public LinkedList<Node> nodeList { get;} 
        public List<PointOfInterest> poiList { get;}
		public List<PointOfInterest> poiVisitedList { get;}
		private bool isComplete { get; set; }
        
        public StoryLine() 
        {
            poiList = new List<PointOfInterest>();
			poiVisitedList = new List<PointOfInterest>();
			isComplete = false;
	
        }

        public StoryLine(int ID)
        {
            this.ID = ID;

			poiList = new List<PointOfInterest>();
			poiVisitedList = new List<PointOfInterest>();
			isComplete = false;
        }


		/// <summary>
		/// DEPRECATED: use addNode() instead
		/// </summary>
        public void addPoi(PointOfInterest poi)
        {
			nodeList.AddLast (poi);
            poiList.Add (poi);
        }

		public void addNode(Node node)
		{
			nodeList.AddLast (node);

			if(node.GetType() == typeof(PointOfInterest))
				poiList.Add (node as PointOfInterest);
		}
			
		//TODO: this method shoudl be removed
        public void addVisitedPoiToList(PointOfInterest poi)
        {
			if(poiList.Contains(poi))
            	poiVisitedList.Add(poi);
        }

		/// <summary>
		/// This method will update the progress of the storyline using the passed node.
		/// </summary>
		public void updateProgress(Node node){

			LinkedListNode<Node> rightBoundLinkedNode = nodeList.Find (node);
		    if (rightBoundLinkedNode != null)
		    {
		        LinkedListNode<Node> currentLinkedNode = rightBoundLinkedNode.Previous;
		        Stack<Node> nodeStack = new Stack<Node>();

		        nodeStack.Push (rightBoundLinkedNode.Value);


		        //first pass, find the leftBound
		        while(currentLinkedNode != null && !currentLinkedNode.Value.visited) {

		            //if the node is of type PointOfInterest then it means that the user skipped a POI, throw an exception
		            if (currentLinkedNode.Value.GetType() != typeof (PointOfInterest))
		            {
		                nodeStack.Push(currentLinkedNode.Value);
		                currentLinkedNode = currentLinkedNode.Previous;
		            }
		            else
		            {
		                throw new PointOfInterestNotVisitedException(
		                    "There is an unvisited POI between the current POI and the previously visited POI.");
		            }
		        }

		        //Now that the leftbound was found, pop the stack and set as visited all the Nodes in it
		        while (nodeStack.Count > 0) {


		            Node currentNode = nodeStack.Pop();
		            currentNode.SetTouched();

		            if (currentNode.GetType () != typeof(PointOfInterest))
		                addVisitedPoiToList (currentNode as PointOfInterest);
		        }
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