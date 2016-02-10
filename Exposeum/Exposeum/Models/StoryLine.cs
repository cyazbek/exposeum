
using Java.Util;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EstimoteSdk;
using Exposeum.Data;

namespace Exposeum.Models
{
    public class StoryLine
    {
        
        
        public string name_en
         {
            get; set;
        }
        public string name_fr
         {
            get; set;
        }
        public string audience_en
         {
            get; set;
        }
        public string audience_fr
         {
            get; set;
        }
        public string desc_en
         {
            get; set;
        }
        public string desc_fr 
         {
            get; set;
        }
        public string duration
        {
            get; set;
        }
        public List<POI> poiList
        {
            get; set;
        }
        public StoryLine()
        {
            this.poiList = new List<POI>();
        }
        public void addPoi(POI poi)
        {
            poiList.Add(poi);
        }
        public POI findPOI(EstimoteSdk.Beacon beacon)
        {
            return poiList.Find(x => x.beacon.compareBeacon(beacon));
        }
        public bool hasBeacon(EstimoteSdk.Beacon beacon)
        {
            foreach(var poi in poiList)
            {
                if (poi.beacon.compareBeacon(beacon))
                    return true;
            }
            return false;
        }
        public int getSize()
        {
            return poiList.Count; 
        }

        
        public string getName()
        {
            string lang = Thread.CurrentThread.CurrentCulture.Name;
            string storyName;
            if (lang.Contains("fr"))
            {
                storyName = this.name_fr;
            }
            else storyName = this.name_en;
            return storyName;
        }

        public string getDescription()
        {
            string lang = Thread.CurrentThread.CurrentCulture.Name;
            string storyDesc;
            if (lang.Contains("fr"))
            {
                storyDesc = this.desc_fr;
            }
            else storyDesc = this.desc_en;
            return storyDesc;
        }


        public string getAudience()
        {
            string lang = Thread.CurrentThread.CurrentCulture.Name;
            string storyAudience;
            if (lang.Contains("fr"))
            {
                storyAudience = this.audience_fr;
            }
            else storyAudience = this.audience_en;
            return storyAudience;
        }
        public void convertFromData(StoryData story)
        {
            this.audience_en = story.audience_en;
            this.audience_fr = story.audience_fr;
            this.desc_en = story.desc_en;
            this.desc_fr = story.desc_fr;
            this.duration = story.duration;
            this.name_en = story.name_en;
            this.name_fr = story.name_fr;
        }
        
    }
}