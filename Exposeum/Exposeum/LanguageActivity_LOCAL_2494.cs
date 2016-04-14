﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Exposeum.Models;


namespace Exposeum
{
	[Activity(Label = "@string/app_name", Icon = "@drawable/Logo", MainLauncher = true, Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen",ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Locale)]	
	public class LanguageActivity : Activity
	{
        /*private Models.Database myDB = new Models.Database();*/
	    readonly User _user = User.GetInstance();

	    private Spinner spinnerLang;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            SetContentView(Resource.Layout.Language);



            WebView gifView = FindViewById<WebView>(Resource.Id.gifView);

            gifView.LoadDataWithBaseURL("file:///android_asset/",
                "<style>img{display: inline;max-height: 100%;max-width: 100%;}</style>"+
                "<html><body><img src=\"languageBanner.gif\" ></body></html>", "text/html", "utf-8", null);


            var langList = new string[]
            {
                "","Francais", "English"
            };
            spinnerLang = FindViewById<Spinner>(Resource.Id.spinnerLang);
            spinnerLang.Adapter = new ArrayAdapter<string>(this, 
                Android.Resource.Layout.SelectDialogItem, langList);

            spinnerLang.ItemSelected += (sender, args) =>
            {
                if (args.Position == 1)
                {
                    _user.SwitchLanguageTo(Language.Fr);
                    var intent = new Intent(this, typeof(WalkthroughActivity));
                    StartActivity(intent);
                }
                else if (args.Position == 2)
                {
                    _user.SwitchLanguageTo(Language.En);
                    var intent = new Intent(this, typeof(WalkthroughActivity));
                    StartActivity(intent);
                }

            };
        }

    }
    

}



