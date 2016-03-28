using System;

namespace Exposeum.Exceptions
{
	public class PathNotFoundException: Exception
	{
		public PathNotFoundException (string message) : base(message)
		{
		}
	}
}

