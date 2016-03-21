using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Exposeum.Models;


namespace Exposeum
{
	[Activity(Label = "@string/app_name", Icon = "@drawable/Logo", MainLauncher = true, Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen",ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Locale)]	
	public class LanguageActivity : Activity
	{
        /*private Models.Database myDB = new Models.Database();*/
	    readonly User _user = User.GetInstance(); 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Language);

            var frenchButton = FindViewById<Button>(Resource.Id.frenchLang);
            var englishButton = FindViewById<Button>(Resource.Id.englishLang);
//            Language.getInstance(); 
            frenchButton.Click += (sender, e)=>
            {
                
                _user.SwitchLanguage(Language.Fr);
                var intent = new Intent(this, typeof(WalkthroughActivity));
                StartActivity(intent);

            };

            englishButton.Click += (sender, e) =>
            {
                _user.SwitchLanguage(Language.En);
                var intent = new Intent(this, typeof(WalkthroughActivity));
                StartActivity(intent);
            };
        }

    }
    

}





