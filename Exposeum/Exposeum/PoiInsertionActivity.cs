using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Exposeum.Models;
using Exposeum.Controllers;
using Java.Util;

namespace Exposeum
{
    [Activity(Label = "New POI")]
    public class PoiInsertion : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            DbController db = new DbController();
            var result = db.createTables();
            Toast.MakeText(this, result, ToastLength.Short).Show();
            PointOfInterest poi = new PointOfInterest { NameEn = "Point Of interest Firas", NameFr = "Point d'interet Firas", DescriptionEn = "This is the point of interest Firas", DescriptionFr = "Celui là est le premier point de Firas" };
            Beacon beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
            poi.Beacon = beacon;
            result= db.InsertPoi(poi);
            Toast.MakeText(this, result, ToastLength.Short).Show();

            // Set path, create connection and create table
            //POIDatabaseControl db = new POIDatabaseControl();
            //db.createTable (); 
            /* folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
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
                PointOfInterest test = new PointOfInterest { NameEn = nameEnglish, NameFr = nameFrench, DescriptionEn = descriptionEnglish, DescriptionFr = descriptionFrench };
                Beacon beacon1 = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 13982, 54450);
                test.Beacon = beacon1;
                string testresult = "Added test POI to db "+db.InsertPoi(test);
                Toast.MakeText(this, testresult, ToastLength.Short).Show();
                PointOfInterest poiq = new PointOfInterest();
                poiq = db.GetPoi(4);
                Toast.MakeText(this, poiq.toString(), ToastLength.Short).Show();
                
                /*
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
