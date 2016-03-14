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
    public class VisitActivity : Activity
    {
        User user = User.GetInstance(); 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VisitActivity);
            var freeVisitButton = FindViewById<Button>(Resource.Id.freeTour);
            var storylineButton = FindViewById<Button>(Resource.Id.storyLine);
            var languageSelector = FindViewById<Button>(Resource.Id.languageButton);
            freeVisitButton.Text = user.GetButtonText("freeTour");
            storylineButton.Text = user.GetButtonText("storyLine");
            languageSelector.Text = user.GetButtonText("languageButton");
            languageSelector.Click += (o, e) => {
                user.ToogleLanguage();
                this.Recreate();
            };

            freeVisitButton.Click += (sender, e) =>
            {
                ExposeumApplication.IsExplorerMode = true;
				ExplorerController.GetInstance().InitializeExplorerMode();

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