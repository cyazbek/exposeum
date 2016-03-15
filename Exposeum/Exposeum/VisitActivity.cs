using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Models;
using Android.Content.PM;

namespace Exposeum
{
    [Activity(Label = "Choose your Tour", Theme = "@android:style/Theme.Holo.Light", ScreenOrientation = ScreenOrientation.Portrait)]
    public class VisitActivity : Activity
    {
        User _user = User.GetInstance();
        Button _freeVisitButton;
        Button _storylineButton;
        Button _languageSelector;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VisitActivity);
            _freeVisitButton = FindViewById<Button>(Resource.Id.freeTour);
            _storylineButton = FindViewById<Button>(Resource.Id.storyLine);
            _freeVisitButton.Text = _user.GetButtonText("freeTour");
            _storylineButton.Text = _user.GetButtonText("storyLine");


            //=======    Action Bar   =================================================================================
            //remove default bar
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetDisplayShowTitleEnabled(false);

            //add custom bar
            ActionBar.SetCustomView(Resource.Layout.ActionBar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            var backActionBarButton = FindViewById<ImageView>(Resource.Id.BackImage);
            backActionBarButton.Click += (s, e) =>
            {
                base.OnBackPressed();
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
            public override void OnBackPressed()
            {
            var intent = new Intent(this, typeof(LanguageActivity));
            StartActivity(intent);
            }//end onBackPressed()


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
                    return true;

                case Resource.Id.QRScannerItem:
                    //do something
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