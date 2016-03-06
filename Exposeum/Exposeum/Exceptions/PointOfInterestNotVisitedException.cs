using System;
using Exposeum.Models;

namespace Exposeum
{
	public class PointOfInterestNotVisitedException : Exception
	{
		public MapElement mapElement{ get;}

		public PointOfInterestNotVisitedException (string message, MapElement element): base(message){
			mapElement = element;
		}
	}
}

