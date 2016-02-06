using System;
using System.Collections.Generic;
using SQLite;
using Exposeum.Models;
using Java.Util;

namespace Exposeum.Model
{
    class POIDatabaseControl
    {
        private String folder; 
        private SQLiteConnection dbConnection; 
        public POIDatabaseControl()
        {
            this.folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            this.dbConnection = this.connect(); 
        }
        private SQLiteConnection connect()
        {
            return new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));
        }
        public SQLiteConnection getConnection()
        {
            return this.dbConnection;
        }
        public void CreateTable()
        {
            dbConnection.CreateTable<POI>();
        }
        public void AddPoi(POI poi)
        {
            dbConnection.Insert(poi);
        }
        public IEnumerable<POI> getPOI(UUID id)
        {
            return dbConnection.Query<POI>("select * from [POI] Where POI.beaconId= {0}", id);
        }

    }
}