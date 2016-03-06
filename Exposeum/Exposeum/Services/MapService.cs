using System;
using Exposeum.Models;

namespace Exposeum
{
	public static class MapService
	{
		private static Map instance;

		public static Map GetMapInstance(){
			if (instance == null)
				instance = new Map ();

			return instance;
		}
	}
}

