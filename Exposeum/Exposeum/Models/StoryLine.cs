
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Exposeum.Models
{
    public class StoryLine
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }
        /*A list containing all POI's relative to storyline*/
        public List<POI> POIS;
        /*constructor for the StoryLine*/
        public StoryLine()
        {
           POIS = new List<POI>();
        }
        //Method to add a POI to a storyline. It makes sure that the POI is set to not veisited
        public void addPOI(POI poi)
        {
            poi.visited = false;
            POIS.Add(poi); 
        }

        //returns a poi given an ID
        public POI getPOI(int id)
        {
           return this.POIS.FirstOrDefault(x => x.ID == id);  
        }

        public POI getBeaconPOI(int id)
        {
            return this.POIS.FirstOrDefault(x => x.beaconId == id);
        }

        public POI getQRPOI(int id)
        {
            return this.POIS.FirstOrDefault(x => x.QrCodeId == id);
        }

        public string name_en { get; set; }
        //Changed the status of a POI to visited. 
        public void visitPoi(POI poi)
        {
            poi.visited = true; 
        }


        public string name_fr { get; set; }

        public string preview_en { get; set; }

        public string preview_fr { get; set; }

        public string target_en { get; set; }

        public string target_fr { get; set; }

        public string duration { get; set; }
    }
}