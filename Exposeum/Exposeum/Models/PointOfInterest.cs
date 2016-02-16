using System;
using Android.App;
using Android.Graphics;

namespace Exposeum.Models
{
    public class PointOfInterest
    {
        public float _u { get; set; }
        public float _v { get; set; }
        public float _radius = 80;
        public readonly Paint _paint = new Paint();
        public Beacon beacon { get; set; }
        public string name_en { get; set; }
        public string name_fr { get; set; }
        public string description_en { get; set; }
        public string description_fr { get; set; }
        public int id { get; set; }
        public int storyID { get; set; }
        public Boolean visited { get; set; }

        public PointOfInterest()
        {
            _paint.SetStyle(Paint.Style.Fill);
            _paint.Color = Color.OrangeRed;
        }

        public PointOfInterest(float u, float v)
        {
            this._u = u;
            this._v = v;

            _paint.SetStyle(Paint.Style.Fill);
            _paint.Color = Color.OrangeRed;
        }
        
        public void Draw(Canvas canvas, float mapWidth, float mapHeight)
        {
            canvas.DrawCircle(_u * mapWidth, _v * mapHeight, _radius, _paint);
        }

        public void SetTouched()
        {
            _paint.Color = Color.ForestGreen;
        }

        public String getHTML()
        {
            string summary;

            if (Language.getLanguage() == "fr")
                summary = String.Format("<html><body>Vous avez selectionnez {0}!<br><br></body></html>", name_fr);
            else
                summary = String.Format("<html><body>You selected {0}!<br><br></body></html>", name_en );

            return summary;
        }

        public string getDescription()
        {
            if (Language.getLanguage() == "fr")
                return this.description_fr;
            else
                return this.description_en;
        }

        public string getName()
        {
            if (Language.getLanguage() == "fr")
                return this.name_fr;
            else
                return this.name_en;
        }

        public bool checkBeacon(Beacon b)
        {
            if (this.beacon == null)
                return false;
            else if (this.beacon.uuid.Equals(b.uuid) & this.beacon.minor == b.minor & this.beacon.major == b.major)
                return true;
            else
                return false;
        }

        public void convertFromData(Data.POIData poi)
        {
            // this.visited = poi.visited;
            this.name_en = poi.name_en;
            this.name_fr = poi.name_fr;
            this.description_en = poi.dscription_en;
            this.description_fr = poi.dscription_fr;
            this._u = poi.uCoord;
            this._v = poi.vCoord;
            this.id = poi.ID;
        }

        public string toString()
        {
            return this.id + " " + this.getName() + " " + this.getDescription();
        }

    }

}