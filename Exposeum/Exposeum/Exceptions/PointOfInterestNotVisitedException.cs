using System;
using System.Collections.Generic;
using Exposeum.Models;

namespace Exposeum.Exceptions
{
	public class PointOfInterestNotVisitedException : Exception
	{
	    public IEnumerable<MapElement> UnvistedMapElements { get; }

	    public PointOfInterestNotVisitedException (string message, IEnumerable<MapElement> elements): base(message){
            UnvistedMapElements = elements;
		}
	}
}

