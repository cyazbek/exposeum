using SQLite;
using Android.App;
using Android.OS;
using Android.Widget;
using Exposeum.Models;

namespace Exposeum
{
    [Activity(Label = "List of POIs")]
    public class PoiListDisplay : Activity
    { 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Models.POIDatabaseControl db = new Models.POIDatabaseControl(); 
            base.OnCreate(savedInstanceState);

            // Set path, create connection
            /*string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));*/
            // load information based on system language missing
            SetContentView(Resource.Layout.POI_List);
            
            LinearLayout ll = FindViewById<LinearLayout>(Resource.Layout.POI_List);

            var textView = FindViewById<TextView>(Resource.Id.listOfPOI);

            //textView.Text+=db.getPoi(1);
            /*foreach (var poi in query)
            {
                //Displaying the POI's information basd on the language of the phone.
                
                    textView.Text += "\n\t\t POI name: " + poi.getName();
                    textView.Text += "\n\t\t POI description: " + poi.getDescription();  
            }*/

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