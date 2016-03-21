using System;

namespace Exposeum
{
	public class PathNotFoundException: Exception
	{
		public PathNotFoundException (string message) : base(message)
		{
		}
	}
}

