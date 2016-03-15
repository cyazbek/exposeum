using System;
using JavaObject = Java.Lang.Object;

namespace Exposeum
{
	public class CantSetRegionException : Exception
	{

		public CantSetRegionException(string message): base(message){}
	}

}