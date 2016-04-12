using System;
using Android.App;
using Android.Runtime;
using Exposeum.Models;
using Ninject;
using Exposeum.Services;
using Exposeum.Services.Service_Providers;
using Exposeum.TDGs;
using Exposeum.Mappers;

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

            UserTdg userTdg = UserTdg.GetInstance();
            User user = User.GetInstance(); 
            if (userTdg.GetSize() == 0)
            {
                user.Id = 0; 
                UserMapper.GetInstance().AddUser(user);
            }
            else
            {
                user = UserMapper.GetInstance().GetUser(0);
            }

		    if (user.Visitor == false)
		    {
                new MapJSONParser().FetchAndParseMapJSON();
                user.SetVisitor(true);
            }
            MapMapper.GetInstance().ParseMap();


        }

		protected void InitSingletons(){
			BeaconFinder.InitInstance (this);
		}

		protected void InitIoCContainer(){
			IoCContainer = new Ninject.StandardKernel();
		}

		protected void InitIoCBindings(){
			IoCContainer.Bind<IStoryLineService> ().To<StoryLineServiceProvider> ().InSingletonScope();
			IoCContainer.Bind<IShortestPathService> ().To<ShortestPathServiceProvider> ();
			IoCContainer.Bind<IGraphService> ().To<GraphServiceProvider> ().InSingletonScope();
			IoCContainer.Bind<IExplorerService> ().To<ExplorerServiceProvider> ();
		}
       
	}
}

