using SQLite;
using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Exposeum
{
    [Activity(Label = "New POI")]
    public class POI_insertion : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var pathToDatabase = System.IO.Path.Combine(docsFolder, "db_sqlcompnet.db");
            base.OnCreate(savedInstanceState);
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));
            var txtResult = FindViewById<TextView>(Resource.Id.displayNum);
            SetContentView(Resource.Layout.POI_Insertion);
            var btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            btnSubmit.Click += (sender, e) =>
            {
                POI poi = new POI { name_fr = "Firas", name_en = "Firas", dscription_fr = "Prettier than Oli", dscription_en = "Plus Beau que Oli" };
                AddStock(conn, poi);
                var textView = FindViewById<TextView>(Resource.Id.displayNum);
                var table = conn.Table<POI>();
                foreach (var s in table)
                {
                    Console.WriteLine(s.name_en + " " + s.name_fr);
                }
            };

        }
        public static void AddStock(SQLiteConnection db, POI poi)
        {
            db.Insert(poi);
        }
        public static IEnumerable<POI> QueryValuations(SQLiteConnection db, POI poi)
        {
            return db.Query<POI>("select * from POI where name_en = ?", poi.name_en);
        }
    }
}
