using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Models;
using Exposeum.Controllers;
using Android.Content.PM;
using Java.IO;

namespace Exposeum
{
    [Activity(Label = "Choose your Tour", Theme = "@style/CustomActionBarTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class VisitActivity : Activity
    {
        readonly User _user = User.GetInstance();
        Button _freeVisitButton;
        Button _storylineButton;
        Button _languageSelector;
        TextView _actionBarTitle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            //var _actionBarTitle = FindViewById<TextView>(Resource.Id.TitleActionBar);
            //_actionBarTitle.Text = "hhhhhhh";
            SetContentView(Resource.Layout.VisitActivity);
            _freeVisitButton = FindViewById<Button>(Resource.Id.freeTour);
            _storylineButton = FindViewById<Button>(Resource.Id.storyLine);
            _freeVisitButton.Text = _user.GetButtonText("freeTour");
            _storylineButton.Text = _user.GetButtonText("storyLine");


            //remove default action bar
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetDisplayShowTitleEnabled(false);

            //add custom action bar
            ActionBar.SetCustomView(Resource.Layout.ActionBar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            var backButtonDisable = FindViewById<ImageView>(Resource.Id.BackImage);
            backButtonDisable.Visibility = ViewStates.Invisible;

            _actionBarTitle = FindViewById<TextView>(Resource.Id.TitleActionBar);
            _actionBarTitle.Text = _user.GetButtonText("TourModeTitle");

            var backActionBarButton = FindViewById<ImageView>(Resource.Id.BackImage);
            backActionBarButton.Click += (s, e) =>
            {
                OnBackPressed();
            };


            //=========================================================================================================

            _freeVisitButton.Click += (sender, e) =>
            {
                ExposeumApplication.IsExplorerMode = true;
				ExplorerController.GetInstance().InitializeExplorerMode();

                var intent = new Intent(this, typeof(MapActivity));
                StartActivity(intent);

            };
            _storylineButton.Click += (sender, e) =>
            {
				ExposeumApplication.IsExplorerMode = false;
				var intent = new Intent(this, typeof(StoryLineListActivity));
				StartActivity(intent);

            };
        }



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.MenuExplorer, menu);
            return base.OnCreateOptionsMenu(menu);
            
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            
            //Toast.MakeText(this, txt, ToastLength.Long);
            switch (item.ItemId)
            {
               
                case Resource.Id.LanguageItem:
                    User.GetInstance().ToogleLanguage();
                    _freeVisitButton.Text = _user.GetButtonText("freeTour");
                    _storylineButton.Text = _user.GetButtonText("storyLine");
                    _actionBarTitle.Text = _user.GetButtonText("TourModeTitle");

                    return true;

                case Resource.Id.QRScannerItem:
					QrController.GetInstance(this).BeginQrScanning();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            var menuItem1 = menu.GetItem(0).SetTitle(_user.GetButtonText("LanguageItem"));
            var menuItem2= menu.GetItem(1).SetTitle(_user.GetButtonText("QRScannerItem"));
            return true; 

        }
        
    }
}