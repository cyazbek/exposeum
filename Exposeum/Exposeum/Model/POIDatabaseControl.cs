using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using Exposeum.Models;
using Java.Util;

namespace Exposeum.Model
{
    class POIDatabaseControl
    {
        private SQLiteConnection dbconnection;
        private String folder; 
        SQLiteConnection conn; 
        public POIDatabaseControl()
        {
            this.folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            this.conn = this.connect(); 
        }
        private SQLiteConnection connect()
        {
            return new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));
        }
        public SQLiteConnection getConnection()
        {
            return this.dbconnection;
        }
        public void CreateTable()
        {
            dbconnection.CreateTable<POI>();
        }
        public void AddPoi(POI poi)
        {
            dbconnection.Insert(poi);
        }
        public IEnumerable<POI> getPOI(UUID id)
        {
            return dbconnection.Query<POI>("select * from [POI] Where POI.beaconId= {0}", id);
        }

    }
}