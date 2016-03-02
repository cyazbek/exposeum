using System;

namespace Exposeum
{
	public class PointOfInterestNotVisitedException : Exception
	{
		public PointOfInterestNotVisitedException (string message): base(message){}
	}
}

