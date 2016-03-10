using System;
using Exposeum.Models;

namespace Exposeum
{
	public static class StoryLineService
	{
		private static Map _instance;

		public static Map GetMapInstance(){
			if (_instance == null)
				_instance = Map.GeMapInstance();

			return _instance;
		}
	}
}

