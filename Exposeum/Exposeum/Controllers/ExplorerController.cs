using Exposeum.Services.Service_Providers;

namespace Exposeum.Controllers
{
	public class ExplorerController
	{
		private static ExplorerController _instance;
		private readonly ExplorerServiceProvider _explorerService;

		private ExplorerController ()
		{
			_explorerService = new ExplorerServiceProvider ();
		}

		public static ExplorerController GetInstance(){

				if (_instance == null)
					_instance = new ExplorerController();
				return _instance;
		}

		public void InitializeExplorerMode(){
			_explorerService.SetExplorerStoryLineAsActive();
		}
	}
}

