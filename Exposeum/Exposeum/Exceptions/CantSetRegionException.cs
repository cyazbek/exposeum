using System;

namespace Exposeum.Exceptions
{
	public class CantSetRegionException : Exception
	{

		public CantSetRegionException(string message): base(message){}
	}

}