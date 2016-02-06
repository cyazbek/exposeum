using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Exposeum
{
    [Activity(Label = "@string/language_activity")]		
	public class LanguageActivity : Activity
	{
        private Models.Database myDB = new Models.Database();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Language);

            var btnCreate = FindViewById<Button>(Resource.Id.POI_sub);
            var btnDisplay = FindViewById<Button>(Resource.Id.POI_disp);

            btnCreate.Click += (sender, e)=>
            {
                  var intent = new Intent(this, typeof(POI_insertion));
                  StartActivity(intent);
            };

            btnDisplay.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(POI_ListDisplay));
                StartActivity(intent);
            };
        }
    }
}




