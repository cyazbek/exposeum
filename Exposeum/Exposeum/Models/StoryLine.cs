using System.Collections.Generic;
using Exposeum.Exceptions;

namespace Exposeum.Models
{
	public class StoryLine: IPath
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
            PoiList = new List<PointOfInterest>();
			MapElements = new List<MapElement> ();
			CurrentStatus = Status.IsNew;
        }

        public StoryLine(int id)
        {
            Id = id;
			PoiList = new List<PointOfInterest>();
			MapElements = new List<MapElement> ();
			CurrentStatus = Status.IsNew;
        }

		public StoryLine(string nameEn, string nameFr, string audienceEn, string audienceFr, string descriptionEn, string descriptionFr, int duration, int imageId)
		{
			NameEn = nameEn;
			NameFr = nameFr;
			AudienceEn = audienceEn;
			AudienceFr = audienceFr;
			DescEn = descriptionEn;
			DescFr = descriptionFr;
			Duration = duration;
			ImageId = imageId;

			MapElements = new List<MapElement> ();
			PoiList = new List<PointOfInterest>();
			CurrentStatus = Status.IsNew;
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
            return _lastPointOfInterestVisited;
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
            _lastPointOfInterestVisited = lastPoiVisited;

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

		        //Now that the leftbound is found, pop the stack and set as visited all the Nodes in it
		        while (nodeStack.Count > 0) {

					MapElement currentNode = nodeStack.Pop();
		            currentNode.Visited = true;
		        }

				//If the rightBoundLinkedNode is a point of interest save it as _lastPointOfInterestVisited
				if (rightBoundLinkedNode.Value.GetType() == typeof (PointOfInterest))
					_lastPointOfInterestVisited = (PointOfInterest)rightBoundLinkedNode.Value;

				//finally, of this node is the last node of the tour, set the tour as completed
				if (rightBoundLinkedNode.Next == null)
					CurrentStatus = Status.IsVisited;
				else if (CurrentStatus == Status.IsNew)
					CurrentStatus = Status.InProgress;
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
            Language lang = User.GetInstance().Language;
            string storyName;

            if (lang.Equals(Language.Fr))
                storyName = NameFr;
            else
                storyName = NameEn;

            return storyName;
        }

        public string GetDescription()
        {
            Language lang = User.GetInstance().Language;
            string storyDesc;

            if (lang.Equals(Language.Fr))
                storyDesc = DescFr;
            else
                storyDesc = DescEn;

            return storyDesc;
        }

        public string GetAudience()
        {
            Language lang = User.GetInstance().Language;
            string storyAudience;
            if (lang.Equals(Language.Fr))
                storyAudience = AudienceFr; 
            else
                storyAudience = AudienceEn;

            return storyAudience;
        }

        

    }
}