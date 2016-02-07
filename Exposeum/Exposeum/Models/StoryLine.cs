
using Java.Util;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EstimoteSdk;

namespace Exposeum.Models
{
    public class StoryLine
    {
        private string name_en
         {
            get; set;
        }
        private string name_fr
         {
            get; set;
        }
        private string audience_en
         {
            get; set;
        }
        private string audience_fr
         {
            get; set;
        }
        private string desc_en
         {
            get; set;
        }
        private string desc_fr 
         {
            get; set;
        }
        private string duration
        {
            get; set;
        }
        private List<POI> poiList
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
        
    }
}