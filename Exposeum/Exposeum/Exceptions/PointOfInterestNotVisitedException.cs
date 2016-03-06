using System;
using Exposeum.Models;

namespace Exposeum
{
	public class PointOfInterestNotVisitedException : Exception
	{
		public MapElement _mapElement{ get;}

		public PointOfInterestNotVisitedException (string message, MapElement element): base(message){
			_mapElement = element;
		}
	}
}

