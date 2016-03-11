﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Renderscripts;
using Android.Widget;
using Exposeum.Controllers;
using Exposeum.Models;
using System.ComponentModel;
using System.Globalization;
using System.Threading;


namespace Exposeum
{
    [Activity(Label = "@string/language_activity", Theme = "@android:style/Theme.Holo.Light",ScreenOrientation = ScreenOrientation.Portrait)]		
	public class LanguageActivity : Activity
	{
        /*private Models.Database myDB = new Models.Database();*/

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Language);

            //=========================================================================================
            
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

            //=========================================================================================

            var frenchButton = FindViewById<Button>(Resource.Id.frenchLang);
            var englishButton = FindViewById<Button>(Resource.Id.englishLang);
//            Language.getInstance(); 
            frenchButton.Click += (sender, e)=>
            {
                Language.SetLanguage("fr");
                Toast.MakeText(this, Language.GetLanguage(), ToastLength.Short).Show();
                var intent = new Intent(this, typeof(WalkthroughActivity));
                StartActivity(intent);

            };

            englishButton.Click += (sender, e) =>
            {
                Language.SetLanguage("en");
                Toast.MakeText(this, Language.GetLanguage(), ToastLength.Short).Show();
                var intent = new Intent(this, typeof(WalkthroughActivity));
                StartActivity(intent);
            };
        }
        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }//end onBackPressed()
    }
    

}





