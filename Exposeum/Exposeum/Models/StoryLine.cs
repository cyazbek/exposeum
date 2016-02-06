
using Java.Util;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Exposeum.Models
{
    public class StoryLine
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }
        /*A list containing all POI's relative to storyline*/
        public List<POI> POIS { get; set; }
        /*constructor for the StoryLine*/
        public StoryLine()
        {
           POIS = new List<POI>();
        }
        //Method to add a POI to a storyline. It makes sure that the POI is set to not veisited
        public void addPOI(POI poi)
        {
            POIS.Add(poi); 
        }

        //returns a poi given an ID
        public POI getPOI(int id)
        {
           return this.POIS.Find(x => x.ID == id);  
        }
        //get a POI given a beaconID
        public POI getBeaconPOI(UUID id)
        {
            return this.POIS.Find(x => x.beaconId == id);
        }
        //get a POI given a QRcode id
        public POI getQRPOI(int id)
        {
            return this.POIS.Find(x => x.QrCodeId == id);
        }

        
        //Changed the status of a POI to visited. 
        public void visitPoi(POI poi)
        {
            poi.visited = true; 
        }

        public string name_en { get; set; }
        public string name_fr { get; set; }
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
        public string preview_en { get; set; }

        public string preview_fr { get; set; }

        public string getPreview()
        {
            string lang = Thread.CurrentThread.CurrentCulture.Name;
            string storyPreview;
            if (lang.Contains("fr"))
            {
                storyPreview = this.preview_fr;
            }
            else storyPreview = this.preview_fr;
            return storyPreview;
        }
        public string audience_en { get; set; }

        public string audience_fr { get; set; }
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
        public int duration { get; set; }
        
    }
}