using SQLite;
using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Content;
using Exposeum.Models;
using Exposeum.Controller;
using Java.Util;

namespace Exposeum
{
    [Activity(Label = "New POI")]
    public class POI_insertion : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            DBController db = new DBController();
            var result = db.createTables();
            Toast.MakeText(this, result, ToastLength.Short).Show();
            POI poi = new POI { name_en = "Point Of interest Firas", name_fr = "Point d'interet Firas", dscription_en = "This is the point of interest Firas", dscription_fr = "Celui là est le premier point de Firas" };
            Beacon beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
            poi.beacon = beacon;
            result= db.insertPoi(poi);
            Toast.MakeText(this, result, ToastLength.Short).Show();

            // Set path, create connection and create table
            //POIDatabaseControl db = new POIDatabaseControl();
            //db.createTable (); 
            /*string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));*/
            SetContentView(Resource.Layout.POI_Insertion);

            var btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            btnSubmit.Click += (sender, e) =>
            {
                
                String nameEnglish = FindViewById<EditText>(Resource.Id.POI_name_en_text).Text.ToString();
                String nameFrench = FindViewById<EditText>(Resource.Id.POI_name_fr_text).Text.ToString();
                String descriptionEnglish = FindViewById<EditText>(Resource.Id.POI_desc_en_text).Text.ToString();
                String descriptionFrench = FindViewById<EditText>(Resource.Id.POI_desc_fr_text).Text.ToString();
                var textView = FindViewById<TextView>(Resource.Id.successMessage);


                /*string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Exposeum.db"));
                var query = conn.Table<Data.POIData>();

                foreach (var poiq in query)
                {
                    textView.Text += "POI ID: " + poiq.ID;
                    textView.Text += "POI english name: " + poiq.name_en;
                    textView.Text += "\nPOI french name: " + poiq.name_fr;
                    textView.Text += "\n POI english description: " + poiq.dscription_en;
                    textView.Text += "\n POI english description: " + poiq.dscription_fr;
                    textView.Text += "\n POI english description: " + poiq.dscription_fr +"\n";
                    textView.Text += conn.ExecuteScalar<int>("select count(*) from POI");
                }
                

                var intent = new Intent(this, typeof(LanguageActivity));
                 StartActivity(intent);*/
            };
            

        }
        /*public static void AddPoi(SQLiteConnection db, POI myPoi)
        {
            var poi = myPoi;
            db.Insert(poi);
        }

        public static IEnumerable<POI> QueryValuations(SQLiteConnection db, POI poi)
        {
            return db.Query<POI>("select * from POI", poi.name_en);
        }*/
    }
}
