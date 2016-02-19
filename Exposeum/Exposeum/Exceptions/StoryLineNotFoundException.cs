using System;

namespace Exposeum
{
	public class StoryLineNotFoundException: Exception
	{
		public StoryLineNotFoundException (string message) : base(message)
		{
		}
	}
}

