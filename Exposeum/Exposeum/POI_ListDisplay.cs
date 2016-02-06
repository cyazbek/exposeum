using SQLite;
using Android.App;
using Android.OS;
using Android.Widget;
using Exposeum.Models;
using System.Threading;


namespace Exposeum
{
    [Activity(Label = "List of POIs")]
    public class POI_ListDisplay : Activity
    { 

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Set path, create connection
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));
            // load information based on system language missing
            SetContentView(Resource.Layout.POI_List);
            
            LinearLayout ll = FindViewById<LinearLayout>(Resource.Layout.POI_List);

            var textView = FindViewById<TextView>(Resource.Id.listOfPOI);
            var query = conn.Table<POI>();

            foreach (var poi in query)
            {
                //recognizing the language of the system to see which language to display
                string lang = Thread.CurrentThread.CurrentCulture.Name; 

                textView.Text += "\n POI ID: " + poi.ID;
                if (lang.Contains("fr"))
                {
                    textView.Text += "\n\t\t POI french name: " + poi.name_fr;
                    textView.Text += "\n\t\t POI french description: " + poi.dscription_fr;
                }

                else
                {
                    textView.Text += "\n\t\t POI english name: " + poi.name_en;
                    textView.Text += "\n\t\t POI english description: " + poi.dscription_en;
                }
            }

            /*
            //List<Button> buttons = new List<Button>();
            for (int i = 0; i < numberOfPOI; i++)
            {
                Button newButton = new Button(this);
                // newButton.SetText(conn.ExecuteScalar<int>("select ID from POI where ID = ?", i));
                // buttons.Add(newButton);
                ll.AddView(newButton);
            }
            */


        }
    }
}