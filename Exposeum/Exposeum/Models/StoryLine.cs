using System.Collections.Generic;
using Exposeum.Exceptions;
using Android.Graphics.Drawables;
using Exposeum.Mappers;

namespace Exposeum.Models
{
    public class StoryLine : IPath
    {

        public int StorylineId { get; set; }
        public string ImgPath { get; set; }
        public int Duration { get; set; }
        public int FloorsCovered { get; set; }
        public Status Status { get; set; }
        public StorylineDescription StorylineDescription { get; set; }
        public List<MapElement> MapElements { get; set; }
        public PointOfInterest LastPointOfInterestVisited { get; set; }
        public int ImageId { get; set; }
        public Drawable Image { get; set; }
        public List<PointOfInterest> PoiList { get; set; }

        public StoryLine()
        {
            PoiList = new List<PointOfInterest>();
			MapElements = new List<MapElement> ();
        }

        public StoryLine(int id)
        {
            StorylineId = id;
			PoiList = new List<PointOfInterest>();
			MapElements = new List<MapElement> ();
        }
        /// <summary>
        /// DEPRECATED: use addMapElement() instead
        /// </summary>
        public void AddPoi(PointOfInterest poi)
        {
            MapElements.Add(poi);
            PoiList.Add(poi);
        }


        public void AddMapElement(MapElement e)
        {
            MapElements.Add(e);
            //to be removed when poiList is removed
            if (e.GetType() == typeof(PointOfInterest))
                PoiList.Add(e as PointOfInterest);
        }


        /// <summary>
        /// This method will update the progress of the storyline using the passed node.
        /// </summary>
        public void UpdateProgress(MapElement mapElement)
        {

            bool foundUnvisitedPoi = false;

            //convert ListMapElements to a LinkedList
            LinkedList<MapElement> nodeList = new LinkedList<MapElement>(MapElements);

            //Find the supplied mapElement in the LinkedList
            LinkedListNode<MapElement> rightBoundLinkedNode = nodeList.Find(mapElement);

            if (rightBoundLinkedNode != null)
            {
                LinkedListNode<MapElement> currentLinkedNode = rightBoundLinkedNode.Previous;
                Stack<MapElement> nodeStack = new Stack<MapElement>();

                nodeStack.Push(rightBoundLinkedNode.Value);

                //first pass, find the leftBound
                while (currentLinkedNode != null && !currentLinkedNode.Value.Visited)
                {
                    if (!foundUnvisitedPoi && currentLinkedNode.Value.GetType() == typeof (PointOfInterest))
                    {
                        foundUnvisitedPoi = true;
                    }

                    nodeStack.Push(currentLinkedNode.Value);
                    currentLinkedNode = currentLinkedNode.Previous;
         
                }

                if (foundUnvisitedPoi)
                {
                    throw new PointOfInterestNotVisitedException(
                        "Unvisited POI(s) between the current POI and the previously visited POI.", nodeStack);
                }

                //Now that the leftbound is found, pop the stack and set as visited all the Nodes in it
                while (nodeStack.Count > 0)
                {

					MapElement currentNode = nodeStack.Pop();
		            currentNode.SetVisited(true);
		        }

				//If the rightBoundLinkedNode is a point of interest save it as LastPointOfInterestVisited
				if (rightBoundLinkedNode.Value.GetType() == typeof (PointOfInterest))
					LastPointOfInterestVisited = (PointOfInterest)rightBoundLinkedNode.Value;

				//finally, of this node is the last node of the tour, set the tour as completed
				if (rightBoundLinkedNode.Next == null)
					SetStatus(Status.IsVisited); 
				else if (Status == Status.IsNew)
                    SetStatus(Status.InProgress);  
		    }
		}

    public PointOfInterest FindPoi(EstimoteSdk.Beacon beacon)
    {
        return PoiList.Find(x => x.Beacon.CompareBeacon(beacon));
    }

		public PointOfInterest FindPoi(Beacon beacon)
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
            return StorylineDescription.Title;
        }

        public string GetDescription()
        {
            return StorylineDescription.Description;
        }

        public string GetAudience()
        {
            return StorylineDescription.IntendedAudience;
    	}

        public bool AreEquals(StoryLine other)
        {
            return StorylineId == other.StorylineId &&
                   ImgPath == other.ImgPath &&
                   Duration == other.Duration &&
                   FloorsCovered == other.FloorsCovered &&
                   StorylineDescription.Equals(other.StorylineDescription) &&
                   MapElement.ListEquals(MapElements, other.MapElements) &&
                   LastPointOfInterestVisited.AreEquals(other.LastPointOfInterestVisited);
        }

        public void SetStatus(Status status)
        {
            this.Status = status;
            StorylineMapper.GetInstance().UpdateStoryline(this);
            
        }
    }
}
 