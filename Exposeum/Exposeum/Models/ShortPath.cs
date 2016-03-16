using System;
using Exposeum.Models;
using System.Collections.Generic;

namespace Exposeum
{
	public class ShortPath: IPath
	{
		public Status CurrentStatus { get; set; }
		public List<MapElement> MapElements { get; set; }

		public ShortPath ()
		{
		}

		public void AddMapElement (MapElement e){
			
		}

		public void UpdateProgress (MapElement mapElement){
			
		}

		public PointOfInterest FindPoi (EstimoteSdk.Beacon beacon){
			
		}

		public bool HasBeacon (EstimoteSdk.Beacon beacon){
			
		}
	}
}

