using SQLite;
using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Content;
using Exposeum.Models;
using System.Globalization;
using Java.Util;

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
                textView.Text += "\n POI ID: " + poi.ID;
                textView.Text += "\n\t\t POI english name: " + poi.name_en;
                textView.Text += "\n\t\t POI french name: " + poi.name_fr;
                textView.Text += "\n\t\t POI english description: " + poi.dscription_en;
                textView.Text += "\n\t\t POI french description: " + poi.dscription_fr;
                
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