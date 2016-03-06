using System;
using Exposeum.Models;

namespace Exposeum
{
	public class PointOfInterestNotVisitedException : Exception
	{
		public PointOfInterest POI{ get;}

		public PointOfInterestNotVisitedException (string message, PointOfInterest poi): base(message){
			this.POI = poi;
		}
	}
}

