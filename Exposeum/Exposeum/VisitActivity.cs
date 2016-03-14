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
        Button freeVisitButton;
        Button storylineButton;
        Button languageSelector;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VisitActivity);
            freeVisitButton = FindViewById<Button>(Resource.Id.freeTour);
            storylineButton = FindViewById<Button>(Resource.Id.storyLine);
            languageSelector = FindViewById<Button>(Resource.Id.languageButton);
            freeVisitButton.Text = user.GetButtonText("freeTour");
            storylineButton.Text = user.GetButtonText("storyLine");
            languageSelector.Text = user.GetButtonText("languageButton");


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



            languageSelector.Click += (o, e) => {
                user.ToogleLanguage();
                freeVisitButton.Text = user.GetButtonText("freeTour");
                storylineButton.Text = user.GetButtonText("storyLine");
                languageSelector.Text = user.GetButtonText("languageButton");
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


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.MenuStoryline, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            
            //Toast.MakeText(this, txt, ToastLength.Long);
            switch (item.ItemId)
            {
               
                case Resource.Id.LanguageItem:
                    User.GetInstance().ToogleLanguage();
                    freeVisitButton.Text = user.GetButtonText("freeTour");
                    storylineButton.Text = user.GetButtonText("storyLine");
                    languageSelector.Text = user.GetButtonText("languageButton");
                    return true;
                case Resource.Id.PauseItem:
                    //Toast.MakeText(context, item.GetType().ToString(), ToastLength.Long);

                    return true;
                case Resource.Id.QRScannerItem:
                    
                    //do something
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}