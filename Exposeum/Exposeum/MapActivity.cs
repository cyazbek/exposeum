using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Controllers;
using Exposeum.Models;

namespace Exposeum
{
	[Activity(Label = "@string/map_activity", Theme = "@style/CustomActionBarTheme", ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]	
	public class MapActivity : Activity
	{

		private BeaconFinder _beaconFinder;
		private MapController _mapController;

		protected override void OnCreate (Bundle savedInstanceState)
		{
            
			base.OnCreate (savedInstanceState);

            //=========================================================================================================
            //remove default bar
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetDisplayShowTitleEnabled(false);

            //add custom bar
            ActionBar.SetCustomView(Resource.Layout.ActionBar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            var title = FindViewById<TextView>(Resource.Id.TitleActionBar);
            title.Text = User.GetInstance().GetButtonText("TourModeTitle");

            var backActionBarButton = FindViewById<ImageView>(Resource.Id.BackImage);
            backActionBarButton.Click += (s, e) =>
            {
                OnBackPressed();
            };
            //=========================================================================================================


            _mapController = MapController.GetInstance (this);

			//Configure the BeaconFinder for this activity
			_beaconFinder = BeaconFinder.GetInstance ();
			_beaconFinder.SetInFocus(true);
			_beaconFinder.SetNotificationDestination (this);

			//Bind the _totalMapView to the Activity
		    SetContentView(_mapController.TotalMapView);
		}

        protected override void OnResume()
		{
			base.OnResume();
			_beaconFinder.SetInFocus (true);

		}

		protected override void OnPause()
		{
			base.OnPause();
			_beaconFinder.SetInFocus (false);
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();
			_mapController.Destroy();
		}


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (ExposeumApplication.IsExplorerMode)
                MenuInflater.Inflate(Resource.Layout.MenuExplorer, menu);
            else
                MenuInflater.Inflate(Resource.Layout.MenuStoryline, menu);
            
            return base.OnCreateOptionsMenu(menu);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.LanguageItem:
                    User.GetInstance().ToogleLanguage(); 
                    return true;
                case Resource.Id.PauseItem:
                    StorylineController storylineController = StorylineController.GetInstance();
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    storylineController.ShowPauseStoryLineDialog(transaction, this);
                    
                    return true;
                case Resource.Id.QRScannerItem:
                    Toast.MakeText(this, "Not Available", ToastLength.Long).Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            User user = User.GetInstance();
            var menuItem1 = menu.GetItem(0).SetTitle(user.GetButtonText("LanguageItem"));
            var menuItem2 = menu.GetItem(1).SetTitle(user.GetButtonText("QRScannerItem"));
            return true;

        }
    }
}
