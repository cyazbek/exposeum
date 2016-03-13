using System;
using Exposeum.Models;

namespace Exposeum
{
	public static class StoryLineService
	{
		private static Map _instance;

		public static Map GetMapInstance(){
            return _instance = Map.GetInstance();
		}
	}
}

