
using System.Collections.Generic;
using Exposeum.Data;

namespace Exposeum.Models
{
    public class StoryLine
    {
       

        public int ImageId { get; set; }
        public Status currentStatus { get; set; }
        public string name_en {get; set;}
        public string name_fr { get; set; }
        public string audience_en { get; set; }
        public string audience_fr { get; set; }
        public string desc_en { get; set; }
        public string desc_fr { get; set; }
        public int duration { get; set; }
        public int ID { get; set; }
        public string imgPath { get; set; }
        public int FloorsCovered { get; set; }
        public List<MapElement> ListMapElements { get; set; } 
        public List<PointOfInterest> poiList { get; set; }
        public List<PointOfInterest> poiVisitedList = new List<PointOfInterest>();
		private bool isComplete { get; set; }
        
        public StoryLine() 
        {
            this.poiList = new List<PointOfInterest>();
			isComplete = false;
	
        }

        public StoryLine(int ID)
        {
			this.poiList = new List<PointOfInterest>();
            this.ID = ID;
			isComplete = false;
        }

		public StoryLine(string name_en, string name_fr, string audience_en, string audience_fr, string description_en, string description_fr, int duration, int imageId)
		{
			this.name_en = name_en;
			this.name_fr = name_fr;
			this.audience_en = audience_en;
			this.audience_fr = audience_fr;
			this.desc_en = description_en;
			this.desc_fr = description_fr;
			this.duration = duration;
			this.ImageId = imageId;
			this.currentStatus = Status.isNew;
			this.poiList = new List<PointOfInterest>();
			isComplete = false;
		}

        public void addPoi(PointOfInterest poi)
        {
            poiList.Add(poi);
        }

        public void addVisitedPoiToList(PointOfInterest poi)
        {
            poiVisitedList.Add(poi);
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