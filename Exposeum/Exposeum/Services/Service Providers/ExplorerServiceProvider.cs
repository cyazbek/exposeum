using Exposeum.Models;

namespace Exposeum
{
	public class ExplorerServiceProvider: IExplorerService
	{
		//this service is only concerned with the Storylines so we use the story line service
		private readonly IStoryLineService _storyLineService;

		public ExplorerServiceProvider ()
		{
			_storyLineService = new StoryLineServiceProvider ();
		}

		public void SetExplorerStoryLineAsActive(){
			//TODO: temporary implementation, should set the active storyline to a storyline
			//actually dedicated to the explorer mode

			StoryLine firstStoryLine = _storyLineService.GetStoryLines() [0];
			_storyLineService.SetActiveStoryLine (firstStoryLine);
		}
	}
}

