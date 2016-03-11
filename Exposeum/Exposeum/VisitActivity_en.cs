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
using Android.Content.PM;

namespace Exposeum
{
    [Activity(Label = "Choose your Tour", Theme = "@android:style/Theme.Holo.Light", ScreenOrientation = ScreenOrientation.Portrait)]
    public class VisitActivityEn : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VisitActivity_en);
            var freeVisitButton = FindViewById<Button>(Resource.Id.freeTour);
            var storylineButton = FindViewById<Button>(Resource.Id.storyLine);
            var languageSelector = FindViewById<Button>(Resource.Id.languageButton);
            languageSelector.Click += (o, e) => {
                    Language.ToogleLanguage();
                    var intent = new Intent(this, typeof(VisitActivityFr));
                    StartActivity(intent);
            };

            freeVisitButton.Click += (sender, e) =>
            {
                ExposeumApplication.IsExplorerMode = true;
                var intent = new Intent(this, typeof(MapActivity));
                StartActivity(intent);

            };
            storylineButton.Click += (sender, e) =>
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
            MenuInflater.Inflate(Resource.Layout.Menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {


                case Resource.Id.Language_option:
                    //do something
                    return true;
                case Resource.Id.Pause_option:
                    //do something
                    return true;
                case Resource.Id.QRScanner_option:
                    //do something
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}