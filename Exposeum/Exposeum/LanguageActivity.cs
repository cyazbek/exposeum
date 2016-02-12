using Android.App;
using Android.Content;
using Android.OS;
using Android.Renderscripts;
using Android.Widget;
using Exposeum.Controller;
using Exposeum.Models;
using System.ComponentModel;
using System.Globalization;
using System.Threading;


namespace Exposeum
{
    [Activity(Label = "@string/language_activity")]		
	public class LanguageActivity : Activity
	{
        /*private Models.Database myDB = new Models.Database();*/

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Language);
            var frenchButton = FindViewById<Button>(Resource.Id.frenchLang);
            var englishButton = FindViewById<Button>(Resource.Id.englishLang);
            Language.getInstance(); 
            frenchButton.Click += (sender, e)=>
            {
                Language.setLanguage("fr");
                Toast.MakeText(this, Language.getLanguage(), ToastLength.Short).Show();
                var intent = new Intent(this, typeof(VisitActivity_fr));
                StartActivity(intent);

            };

            englishButton.Click += (sender, e) =>
            {
                Language.setLanguage("en");
                Toast.MakeText(this, Language.getLanguage(), ToastLength.Short).Show();
                var intent = new Intent(this, typeof(VisitActivity_en));
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





