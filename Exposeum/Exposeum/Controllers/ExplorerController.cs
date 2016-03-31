using Exposeum.Services.Service_Providers;
using Exposeum.Services;
using Ninject;

namespace Exposeum.Controllers
{
	public class ExplorerController
	{
		private static ExplorerController _instance;
		private readonly IExplorerService _explorerService;

		private ExplorerController ()
		{
			_explorerService = ExposeumApplication.IoCContainer.Get<IExplorerService> ();
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

