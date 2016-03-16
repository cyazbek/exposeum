using System;
using Exposeum.Models;
using System.Collections.Generic;

namespace Exposeum.Models
{
	public class ShortPath: IPath
	{
		public Status CurrentStatus { get; set; }
		public List<MapElement> MapElements { get; set; }
		private PointOfInterest _lastPointOfInterestVisited ;

		public ShortPath (List<MapElement> mapElements)
		{
			MapElements = mapElements;
		}

		public void AddMapElement (MapElement e){
			MapElements.Add (e);
		}

		public void UpdateProgress (MapElement mapElement){
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

				//finally, of this node is the last node of the path, set the path as completed
				if (rightBoundLinkedNode.Next == null)
					CurrentStatus = Status.IsVisited;
				else if (CurrentStatus == Status.IsNew)
					CurrentStatus = Status.InProgress;
			}
		}

		public PointOfInterest FindPoi (EstimoteSdk.Beacon beacon){
			
			foreach (MapElement mapElement in MapElements) {

				//If the Map Element is a PointOfInterest, change the descriptions 
				//and names so that the push notifications remain relevant.
				if (mapElement.GetType () == typeof(PointOfInterest)) {
					PointOfInterest poi = (PointOfInterest)mapElement;
					if (poi.Beacon.CompareBeacon (beacon))
						return poi;
				}
			}

			return null;
		}

		public bool HasBeacon (EstimoteSdk.Beacon beacon){
			if (FindPoi (beacon) != null)
				return true;
			else
				return false;
		}
	}
}

