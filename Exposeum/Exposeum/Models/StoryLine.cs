
using System.Collections.Generic;
using Exposeum.Data;

namespace Exposeum.Models
{
    public class StoryLine
    {

        public string name_en {get; set;}
        public string name_fr { get; set; }
        public string audience_en { get; set; }
        public string audience_fr { get; set; }
        public string desc_en { get; set; }
        public string desc_fr { get; set; }
        public string duration { get; set; }
        public int ID { get; set; }
        public string imgPath { get; set; }
        public int FloorsCovered { get; set; }
        public List<MapElement> ListMapElements { get; set; } 
        public List<PointOfInterest> poiList { get; set; }
        private PointOfInterest _lastPointOfInterestVisited ;
		private bool isComplete { get; set; }
        
        public StoryLine() 
        {
            this.poiList = new List<PointOfInterest>();
			isComplete = false;
	
        }

        public StoryLine(int ID)
        {
            this.ID = ID;
        }

        public void addPoi(PointOfInterest poi)
        {
            poiList.Add(poi);
        }

        public void SetLastPointOfInterestVisited(PointOfInterest lastPoiVisited)
        {
            this._lastPointOfInterestVisited = lastPoiVisited;
        }

        public PointOfInterest findPOI(EstimoteSdk.Beacon beacon)
        {
            return poiList.Find(x => x.beacon.compareBeacon(beacon));
        }

        public bool hasBeacon(EstimoteSdk.Beacon beacon)
        {
            foreach (var poi in poiList)
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
            string lang = Language.getLanguage();
            string storyName;

            if (lang.Equals("fr"))
                storyName = this.name_fr;
            else
                storyName = this.name_en;

            return storyName;
        }

        public string getDescription()
        {
            string lang = Language.getLanguage();
            string storyDesc;

            if (lang.Equals("fr"))
                storyDesc = this.desc_fr;
            else
                storyDesc = this.desc_en;

            return storyDesc;
        }

        public string getAudience()
        {
            string lang = Language.getLanguage();
            string storyAudience;

            if (lang.Equals("fr"))
                storyAudience = this.audience_fr;
            else
                storyAudience = this.audience_en;

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