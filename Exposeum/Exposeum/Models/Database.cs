using System;
using SQLite;
using EstimoteSdk;

namespace Exposeum.Models
{
    class Database
    {
        private SQLiteConnection dbconnection;
        private String folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public Database()
        {
            this.dbconnection = connect();
        }

        private SQLiteConnection connect()
        {
            return new SQLiteConnection(System.IO.Path.Combine(folder, "Exposeum.db"));
        }

        public SQLiteConnection getConnection()
        {
            return this.dbconnection;
        }

        public void createTables()
        {
            dbconnection.CreateTable<POI>();
            dbconnection.CreateTable<EstimoteSdk.Beacon>();
            dbconnection.CreateTable<StoryLine>();
        }

    }
}