using System;
using SQLite;

namespace Exposeum.Models
{
    class Database
    {
        private String dbname;
        private SQLiteConnection dbconnection;
        private String folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public Database(String dbname)
        {
            this.dbname = dbname;
            this.dbconnection = new SQLiteConnection(System.IO.Path.Combine(folder, "POI.db"));
        }

        public SQLiteConnection getConnection()
        {
            return this.dbconnection;
        }

    }
}