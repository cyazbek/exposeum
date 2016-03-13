using System;
using SQLite;
using System.IO;


namespace Exposeum.Tables
{
    public class DBManager
    {
        private static DBManager _dbManager;
        public SQLiteConnection _db;
        public readonly string _path = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "Exposeum.db3");

        public static DBManager GetInstance()
        {
            if (_dbManager == null)
            {
                _dbManager = new DBManager();
            }
            return _dbManager;
        }
        private DBManager()
        {
            _db = new SQLiteConnection(_path);
            _db.CreateTable<Storyline>();
            _db.CreateTable<StoryLineMapElementList>();
            _db.CreateTable<StoryLineDescriptionFr>();
            _db.CreateTable<StoryLineDescriptionEn>();
            _db.CreateTable<Edge>();
            _db.CreateTable<Floor>();
            _db.CreateTable<MapElements>();
            _db.CreateTable<PoiDescriptionEn>();
            _db.CreateTable<PoiDescriptionFr>();
            _db.CreateTable<Beacon>();
            _db.CreateTable<ExhibitionContentEn>();
            _db.CreateTable<ExhibitionContentFr>();
            _db.CreateTable<Images>();
            _db.CreateTable<Icon>();
        }
        public SQLiteConnection getConnection()
        {
            return _db;
        }
    }
}