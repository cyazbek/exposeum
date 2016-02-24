using System;
using System.IO;
using Exposeum.Data;
using Exposeum.Models;
using SQLite;
using Java.Util;
using System.Collections.Generic;

namespace Exposeum.Controllers
{
    class DBController
    {
        public string createDB()
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
                db.CreateTable<POIData>();
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

        public string insertBeacon(Beacon beacon)
        {
            try {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
                var db = new SQLiteConnection(dbPath);
                BeaconData data = new BeaconData();
                data.convertBeaconToData(beacon);
                db.Insert(data);
                return "Added Successfully";
            }catch (Exception ex)
            {
                return "Error :" + ex.Message;
            }
        }





        public string insertPoi(PointOfInterest poi)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
                var db = new SQLiteConnection(dbPath);
                BeaconData beacon = new BeaconData();
                int beaconId=beacon.convertBeaconToData(poi.beacon);
                db.Insert(beacon);
                POIData poiData = new POIData();
                poiData.convertPOIToData(poi, beaconId);
                db.Insert(poiData);
                return "POI inserted correctly";
            }
            catch(Exception ex)
            {
                return "Error " + ex.Message; 
            }
        }
        public String insertPoiStory(int poiId, int storId)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
                var db = new SQLiteConnection(dbPath);
                PoiStoryData poiStory = new PoiStoryData { poiId = poiId, storyId = storId };
                db.Insert(poiStory);
                return "Success";
            }
            catch(Exception ex)
            {
                return "Error " + ex.Message;
            }
        }
        public string insertStory(StoryLine story)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
                var db = new SQLiteConnection(dbPath);
                StoryData storyData = new StoryData();
                storyData.audience_en = story.audience_en;
                storyData.audience_fr = story.audience_fr;
                storyData.desc_en = story.desc_en;
                storyData.desc_fr = story.desc_fr;
                storyData.duration = story.duration;
                storyData.name_en = story.name_en;
                storyData.name_fr = story.name_fr;
                db.Insert(storyData);
                POIData poiData;
                BeaconData beaconData; 
                foreach(var poi in story.poiList)
                {
                    poiData = new POIData();
                    beaconData = new BeaconData();
                    int beaconId = beaconData.convertBeaconToData(poi.beacon);
                    int id = poiData.convertPOIToData(poi, beaconId);
                    db.Insert(beaconData);
                    db.Insert(poiData);
                    insertPoiStory(id, storyData.id); 
                }
                return "insert complete"; 
                
            }
            catch (Exception ex)
            {
                return "Error " + ex.Message;
            }
        }
        public SQLiteConnection getConnection()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
            var db = new SQLiteConnection(dbPath);
            return db; 
        }

        public PointOfInterest getPoi(int id)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
            var db = new SQLiteConnection(dbPath);
            var result = db.Get<POIData>(id);
            PointOfInterest poi = new PointOfInterest();
            poi.convertFromData(result);
            var result2 = db.Get<BeaconData>(id);
            Beacon beacon= new Beacon();
            beacon.convertFromData(result2);
            poi.beacon = beacon;
            return poi; 
        }

        public Beacon getBeacon(int id)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
            var db = new SQLiteConnection(dbPath);
            BeaconData beaconData = new BeaconData();
            beaconData = db.Get<BeaconData>(id);
            Beacon beacon = new Beacon();
            beacon.convertFromData(beaconData);
            return beacon;
        }
        public StoryLine getStory(int id)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Exposeum.db3");
            var db = new SQLiteConnection(dbPath);
            StoryData storyData = new StoryData();
            storyData = db.Get<StoryData>(id);
            int storyId = storyData.id;
            var poiList = from poi in db.Table<PoiStoryData>()
                          where poi.storyId.Equals(storyId)
                          select poi.poiId;
            StoryLine storyLine = new StoryLine();
            storyLine.convertFromData(storyData);
            foreach(int i in poiList)
            {
                storyLine.poiList.Add(getPoi(i)); 
            }

            return storyLine; 
        }

    }
}