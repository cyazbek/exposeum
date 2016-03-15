using System;
using Android.App;
using Android.Runtime;

namespace Exposeum
{
	[Application]
	public class ExposeumApplication: Application
	{

	    public static bool IsExplorerMode { get; set; }
        

        public ExposeumApplication (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}
			
		public override void OnCreate(){
			base.OnCreate ();
			InitSingletons ();
            
        }

		protected void InitSingletons(){
			BeaconFinder.InitInstance (this);
		}
       
	}
}

