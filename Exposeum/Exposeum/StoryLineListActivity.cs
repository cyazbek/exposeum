using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Models;
using Exposeum.Controllers;

namespace Exposeum
{
	[Activity(Label = "StoryLineListActivity", Theme = "@android:style/Theme.Holo.Light", ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]
    public class StoryLineListActivity : Activity
    {
        public Map Map;
	    readonly StorylineController _storylineController = StorylineController.GetInstance();
        private Bundle _bundle;
        protected override void OnCreate(Bundle bundle)
        {
            _bundle = bundle;
            base.OnCreate(bundle);
            //=========================================================================================================
            //remove default bar
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetDisplayShowTitleEnabled(false);

            //add custom bar
            ActionBar.SetCustomView(Resource.Layout.ActionBar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            var backActionBarButton = FindViewById<ImageView>(Resource.Id.BackImage);
            backActionBarButton.Click += (s, e) =>
            {
                OnBackPressed();
            };


            //=========================================================================================================

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StoryLineListView);
            var listView = FindViewById<ListView>(Resource.Id.List);
			listView.Adapter = _storylineController.GetStoryLinesListAdapter(this);
			_storylineController.SetContext (this);
            listView.ItemClick += ListViewItemClick;
        }

        protected override void OnRestart()
        {
            OnCreate(_bundle);
        }

        private void ListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
			_storylineController.SelectStoryLine (e.Position);
			_storylineController.ShowSelectedStoryLineDialog (transaction, this);

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.MenuExplorer, menu);
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

