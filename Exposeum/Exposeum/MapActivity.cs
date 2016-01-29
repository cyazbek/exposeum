namespace Exposeum
{
	using Android.App;
	using Android.OS;
	using Android.Views;

	[Activity(Label = "@string/map_activity")]
	public class MapActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			View v = new MapView (this);
			SetContentView (v);
		}
	}
}
