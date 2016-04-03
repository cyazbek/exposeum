using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Controllers;
using Exposeum.Models;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Exposeum.Resources.layout;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Exposeum
{
	[Activity(Label = "@string/map_activity", Theme = "@style/CustomActionBarTheme", ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]	
	public class MapActivity : Activity, View.IOnTouchListener
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

            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();

            var floorselectofrag = new FloorSelectorFragment();
            FrameLayout floorFrameLayout = FindViewById<FrameLayout>(Resource.Id.floor_selector_frame);
            fragmentTx.Add(Resource.Id.floor_selector_frame, floorselectofrag);
            fragmentTx.Commit();




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

                case Resource.Id.QRScannerItem:
                    Toast.MakeText(this, "Not Available", ToastLength.Long).Show();
                    return true;

                case Resource.Id.PauseItem:
                    StorylineController storylineController = StorylineController.GetInstance();
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    storylineController.ShowPauseStoryLineDialog(transaction, this);
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

	    public bool OnTouch(View v, MotionEvent e)
	    {
	        throw new System.NotImplementedException();
	    }
	}
}
