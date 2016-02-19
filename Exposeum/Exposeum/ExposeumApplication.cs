using System;
using Android.App;

namespace Exposeum
{
	public class ExposeumApplication: Application
	{
		public ExposeumApplication ()
		{
		}

		public override void OnCreate(){
			initSigletons ();
		}

		protected void initSigletons(){
			
		}
	}
}

