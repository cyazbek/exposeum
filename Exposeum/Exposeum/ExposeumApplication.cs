using System;
using Android.App;
using Android.Runtime;
using Exposeum.Models;
using Ninject;
using Exposeum.Services;
using Exposeum.Services.Service_Providers;

namespace Exposeum
{
	[Application]
	public class ExposeumApplication: Application
	{

	    public static bool IsExplorerMode { get; set; }
		public static StandardKernel IoCContainer { get; set; }
        

        public ExposeumApplication (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

			
		public override void OnCreate(){
			base.OnCreate ();
			InitSingletons ();          
			InitIoCContainer ();
			InitIoCBindings ();
			new MapJSONParser ().FetchAndParseMapJSON ();
        }

		protected void InitSingletons(){
			BeaconFinder.InitInstance (this);
		}

		protected void InitIoCContainer(){
			IoCContainer = new Ninject.StandardKernel();
		}

		protected void InitIoCBindings(){
			IoCContainer.Bind<IStoryLineService> ().To<StoryLineServiceProvider> ();
			IoCContainer.Bind<IShortestPathService> ().To<ShortestPathServiceProvider> ();
			IoCContainer.Bind<IGraphService> ().To<GraphServiceProvider> ().InSingletonScope();
			IoCContainer.Bind<IExplorerService> ().To<ExplorerServiceProvider> ();
		}
       
	}
}

