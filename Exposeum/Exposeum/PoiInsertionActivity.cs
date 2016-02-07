using SQLite;
using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Content;
using Exposeum.Models;

namespace Exposeum
{
    [Activity(Label = "New POI")]
    public class POI_insertion : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
         
            base.OnCreate(savedInstanceState);

            // Set path, create connection and create table
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));
            conn.CreateTable<POI>();
            SetContentView(Resource.Layout.POI_Insertion);

            var btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            btnSubmit.Click += (sender, e) =>
            {
                
                String nameEnglish = FindViewById<EditText>(Resource.Id.POI_name_en_text).Text.ToString();
                String nameFrench = FindViewById<EditText>(Resource.Id.POI_name_fr_text).Text.ToString();
                String descriptionEnglish = FindViewById<EditText>(Resource.Id.POI_desc_en_text).Text.ToString();
                String descriptionFrench = FindViewById<EditText>(Resource.Id.POI_desc_fr_text).Text.ToString();

                POI myPoi = new POI { name_en = nameEnglish, name_fr = nameFrench, dscription_en = descriptionEnglish, dscription_fr = descriptionFrench };
                AddPoi(conn, myPoi);

                var textView = FindViewById<TextView>(Resource.Id.successMessage);
                textView.Text = "POI added to the DB successfully";

                /*
                var query = conn.Table<POI>();

                foreach (var poi in query)
                {
                    textView.Text += "POI ID: " + poi.ID;
                    textView.Text += "POI english name: " + poi.name_en;
                    textView.Text += "\nPOI french name: " + poi.name_fr;
                    textView.Text += "\n POI english description: " + poi.dscription_en;
                    textView.Text += "\n POI english description: " + poi.dscription_fr;
                    textView.Text += "\n POI english description: " + poi.dscription_fr +"\n";
                    textView.Text += conn.ExecuteScalar<int>("select count(*) from POI");
                }
                */

                // var intent = new Intent(this, typeof(LanguageActivity));
                // StartActivity(intent);
            };
            

        }
        public static void AddPoi(SQLiteConnection db, POI myPoi)
        {
            var poi = myPoi;
            db.Insert(poi);
        }

        public static IEnumerable<POI> QueryValuations(SQLiteConnection db, POI poi)
        {
            return db.Query<POI>("select * from POI", poi.name_en);
        }
    }
}
