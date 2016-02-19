using System;
using Android.App;
using Android.Runtime;

namespace Exposeum
{
	[Application]
	public class ExposeumApplication: Application
	{
		public ExposeumApplication (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}
			
		public override void OnCreate(){
			base.OnCreate ();
			initSingletons ();
		}

		protected void initSingletons(){
			BeaconFinder.initInstance (this);
		}
	}
}

