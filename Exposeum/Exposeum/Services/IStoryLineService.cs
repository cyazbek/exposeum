using System;
using System.Collections.Generic;
using Exposeum.Models;

namespace Exposeum
{
	public interface IStoryLineService
	{
		List<StoryLine> GetStoryLines ();
		StoryLine GetActiveStoryLine ();
		void SetActiveStoryLine (StoryLine storyline);
		StoryLine GetStoryLineById(int id);
	}
}

