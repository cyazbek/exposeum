using System;
using SQLite;
using System.IO;


namespace Exposeum.Tables
{
    public class DbManager
    {
        private static DbManager _dbManager;
        public SQLiteConnection Db;
        public readonly string Path = System.IO.Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "Exposeum.db3");

        public static DbManager GetInstance()
        {
            if (_dbManager == null)
            {
                _dbManager = new DbManager();
            }
            return _dbManager;
        }

        private DbManager()
        {
            Db = new SQLiteConnection(Path);
            Db.CreateTable<Storyline>();
            Db.CreateTable<StoryLineMapElementList>();
            Db.CreateTable<StoryLineDescriptionFr>();
            Db.CreateTable<StoryLineDescriptionEn>();
            Db.CreateTable<Edge>();
            Db.CreateTable<Floor>();
            Db.CreateTable<MapElements>();
            Db.CreateTable<PoiDescriptionEn>();
            Db.CreateTable<PoiDescriptionFr>();
            Db.CreateTable<Beacon>();
            Db.CreateTable<ExhibitionContentEn>();
            Db.CreateTable<ExhibitionContentFr>();
            Db.CreateTable<Images>();
            Db.CreateTable<Icon>();
        }

        public SQLiteConnection GetConnection()
        {
            return Db;
        }
    }
}