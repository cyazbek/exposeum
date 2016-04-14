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
	    private float mLastPosY;

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
			StorylineController.GetInstance().SetContext (this);

			//Configure the BeaconFinder for this activity
			_beaconFinder = BeaconFinder.GetInstance ();
			_beaconFinder.SetInFocus(true);
			_beaconFinder.SetNotificationDestination (this);

            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();
            /*
            var floorselectofrag = new FloorSelectorFragment();
            FrameLayout floorFrameLayout = FindViewById<FrameLayout>(Resource.Id.map_frag_floorSelector);
            fragmentTx.Add(Resource.Id.map_frag_floorSelector, floorselectofrag);
            fragmentTx.Commit();
            */
            //floorFrameLayout.SetOnTouchListener(this);
            


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
                    QrController.GetInstance(this).BeginQrScanning();
                    return true;

                case Resource.Id.PauseItem:
                    PauseTrigger();
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

/*	    public override void OnBackPressed()
	    {
	        PauseTrigger();
	    }
*/
	    public void PauseTrigger()
	    {
            StorylineController storylineController = StorylineController.GetInstance();
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            storylineController.ShowPauseStoryLineDialog(transaction, this);
        }

	    public bool OnTouch(View v, MotionEvent e)
	    {
	        switch (e.Action)
	        {
                case MotionEventActions.Down:
	                mLastPosY = e.GetY();
	                return true;

                case MotionEventActions.Move:
	                var currentPosition = e.GetY();
	                var deltaY = mLastPosY - currentPosition;

	                var transY = v.TranslationY;

	                transY -= deltaY;

	                if (transY < 0)
	                {
	                    transY = 0;
	                }

	                v.TranslationY = transY;

	                return true;
	        }

	        return true;
	    }
	}
}
