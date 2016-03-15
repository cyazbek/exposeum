using System;
using System.IO;
using Exposeum.Data;
using Exposeum.Models;
using SQLite;

namespace Exposeum.Controllers
{
    class DbController
    {
        public string CreateDb()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
            var db = new SQLiteConnection(dbPath);
            return "Database Created";
        }
        public string createTables()
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<PoiData>();
                db.CreateTable<StoryData>();
                db.CreateTable<BeaconData>();
                db.CreateTable<PoiStoryData>();
                return "Tables created"; 
            }
            catch(Exception ex)
            {
                return "Error: Creating Message"+ex.Message;
            }
        }

        internal object CreateTables()
        {
            throw new NotImplementedException();
        }

        public string InsertBeacon(Beacon beacon)
        {
            try {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
                var db = new SQLiteConnection(dbPath);
                BeaconData data = new BeaconData();
                data.ConvertBeaconToData(beacon);
                db.Insert(data);
                return "Added Successfully";
            }catch (Exception ex)
            {
                return "Error :" + ex.Message;
            }
        }





        public string InsertPoi(PointOfInterest poi)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
                var db = new SQLiteConnection(dbPath);
                BeaconData beacon = new BeaconData();
                int beaconId=beacon.ConvertBeaconToData(poi.Beacon);
                db.Insert(beacon);
                PoiData poiData = new PoiData();
                poiData.ConvertPoiToData(poi, beaconId);
                db.Insert(poiData);
                return "POI inserted correctly";
            }
            catch(Exception ex)
            {
                return "Error " + ex.Message; 
            }
        }
        public String InsertPoiStory(int poiId, int storId)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
                var db = new SQLiteConnection(dbPath);
                PoiStoryData poiStory = new PoiStoryData { PoiId = poiId, StoryId = storId };
                db.Insert(poiStory);
                return "Success";
            }
            catch(Exception ex)
            {
                return "Error " + ex.Message;
            }
        }
        public string InsertStory(StoryLine story)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
                var db = new SQLiteConnection(dbPath);
                StoryData storyData = new StoryData();
                storyData.AudienceEn = story.AudienceEn;
                storyData.AudienceFr = story.AudienceFr;
                storyData.DescEn = story.DescEn;
                storyData.DescFr = story.DescFr;
                storyData.Duration = story.Duration;
                storyData.NameEn = story.NameEn;
                storyData.NameFr = story.NameFr;
                db.Insert(storyData);
                PoiData poiData;
                BeaconData beaconData; 
                foreach(var poi in story.PoiList)
                {
                    poiData = new PoiData();
                    beaconData = new BeaconData();
                    int beaconId = beaconData.ConvertBeaconToData(poi.Beacon);
                    int id = poiData.ConvertPoiToData(poi, beaconId);
                    db.Insert(beaconData);
                    db.Insert(poiData);
                    InsertPoiStory(id, storyData.Id); 
                }
                return "insert complete"; 
                
            }
            catch (Exception ex)
            {
                return "Error " + ex.Message;
            }
        }
        public SQLiteConnection GetConnection()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
            var db = new SQLiteConnection(dbPath);
            return db; 
        }

        public PointOfInterest GetPoi(int id)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
            var db = new SQLiteConnection(dbPath);
            var result = db.Get<PoiData>(id);
            PointOfInterest poi = new PointOfInterest();
            poi.ConvertFromData(result);
            var result2 = db.Get<BeaconData>(id);
            Beacon beacon= new Beacon();
            beacon.ConvertFromData(result2);
            poi.Beacon = beacon;
            return poi; 
        }

        public Beacon GetBeacon(int id)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
            var db = new SQLiteConnection(dbPath);
            BeaconData beaconData = new BeaconData();
            beaconData = db.Get<BeaconData>(id);
            Beacon beacon = new Beacon();
            beacon.ConvertFromData(beaconData);
            return beacon;
        }
        public StoryLine GetStory(int id)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
            var db = new SQLiteConnection(dbPath);
            StoryData storyData = new StoryData();
            storyData = db.Get<StoryData>(id);
            int storyId = storyData.Id;
            var poiList = from poi in db.Table<PoiStoryData>()
                          where poi.StoryId.Equals(storyId)
                          select poi.PoiId;
            StoryLine storyLine = new StoryLine();
            storyLine.ConvertFromData(storyData);
            foreach(int i in poiList)
            {
                storyLine.PoiList.Add(GetPoi(i)); 
            }

            return storyLine; 
        }

    }
}