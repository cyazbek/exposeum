using System.Collections.Generic;
using Exposeum.Models;

namespace Exposeum.Services
{
	public interface IStoryLineService
	{
		List<StoryLine> GetStoryLines ();
		StoryLine GetActiveStoryLine ();
		void SetActiveStoryLine (StoryLine storyline);
		StoryLine GetStoryLineById(int id);
	}
}

