using System;
using System.Collections.Generic;
using SQLite;
using Exposeum.Models;
using Java.Util;

namespace Exposeum.Model
{
    class POIDatabaseControl
    {
        private SQLiteConnection dbconnection;
        private String folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public POIDatabaseControl()
        {
            this.dbconnection = connect();
        }

        private SQLiteConnection connect()
        {
            return new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));
        }
        public void createTable()
        {
            dbconnection.CreateTable<StoryLine>();
        }
        public void addPOI(POI poi)
        {
            var myPoi = poi;
            this.dbconnection.Insert(myPoi);
        }
        public POI getPoi(int id)
        {
            return dbconnection.Get<POI>(id);
        }

    }
}