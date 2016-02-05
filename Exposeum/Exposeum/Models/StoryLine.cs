
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Exposeum.Models
{
    public class StoryLine
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }

        public List<POI> POIS = new List<POI>();

        //this method is to fetch through the database and add all the correspondant poi's to its storyline. 
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