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
    [Activity(Label = "Choice Of Visit", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class VisitActivity_en : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VisitActivity_en);
            var freeVisitButton = FindViewById<Button>(Resource.Id.freeTour);
            var storylineButton = FindViewById<Button>(Resource.Id.storyLine);
            var languageSelector = FindViewById<Button>(Resource.Id.languageButton);
            languageSelector.Click += (o, e) => {
                    Language.toogleLanguage();
                    var intent = new Intent(this, typeof(VisitActivity_fr));
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
        }
}