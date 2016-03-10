using System;
using Exposeum.Models;

namespace Exposeum
{
	public static class StoryLineService
	{
		private static Map instance;

		public static Map GetMapInstance(){
			if (instance == null)
				instance = Map.GeMapInstance();

			return instance;
		}
	}
}

