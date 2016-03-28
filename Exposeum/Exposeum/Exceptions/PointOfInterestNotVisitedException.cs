using System;
using Exposeum.Models;

namespace Exposeum.Exceptions
{
	public class PointOfInterestNotVisitedException : Exception
	{
		public PointOfInterest Poi{ get;}

		public PointOfInterestNotVisitedException (string message, PointOfInterest poi): base(message){
			Poi = poi;
		}
	}
}

