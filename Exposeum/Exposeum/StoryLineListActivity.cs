using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Exposeum.Models;
using Exposeum.Controllers;

namespace Exposeum
{
    [Activity(Label = "StoryLineListActivity", Theme = "@android:style/Theme.Holo.Light")]
    public class StoryLineListActivity : Activity
    {
        public Map Map;
		StorylineController _storylineController = StorylineController.GetInstance();

        protected override void OnCreate(Bundle bundle)
        {
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
                base.OnBackPressed();
            };


            //=========================================================================================================

            ListView listView;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StoryLineListView);
            listView = FindViewById<ListView>(Resource.Id.List);
			listView.Adapter = _storylineController.GetStoryLines(this);
            listView.ItemClick += ListViewItemClick;
        }

        private void ListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
			_storylineController.SelectStoryLine (e.Position);
			_storylineController.ShowSelectedStoryLineDialog (transaction, this);

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.Menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {


                case Resource.Id.LanguageItem:
                    User.GetInstance().ToogleLanguage();
                    //var intent = new Intent(this, typeof(VisitActivityFr));
                    //StartActivity(intent);
                    return true;
                case Resource.Id.PauseItem:
                    //do something
                    return true;
                case Resource.Id.QRScannerItem:
                    //do something
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}

