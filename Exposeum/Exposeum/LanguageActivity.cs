using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;

namespace Exposeum
{
    [Activity(Label = "@string/language_activity")]		
	public class LanguageActivity : Activity
	{

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Language);

            //var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //var pathToDatabase = System.IO.Path.Combine(docsFolder, "db_sqlcompnet.db");

            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));
            conn.CreateTable<POI>();

            //var result = await createDatabase(pathToDatabase);
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




