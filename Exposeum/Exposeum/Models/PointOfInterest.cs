using System;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Exposeum.Models
{
    public class PointOfInterest
    {
        public float _u { get; set; }
        public float _v { get; set; }
        public Beacon beacon { get; set; }
        public string name_en { get; set; }
        public string name_fr { get; set; }
        public string description_en { get; set; }
        public string description_fr { get; set; }
        public int id { get; set; }
        public int storyID { get; set; }
        public Boolean visited { get; set; }

		private Drawable _visited_icon = Application.Context.Resources.GetDrawable (Resource.Drawable.Beacon_Activated);
		private Drawable _unvisited_icon =Application.Context.Resources.GetDrawable (Resource.Drawable.Beacon_Inactivated);

        public PointOfInterest()
        {
			visited = false;

			_visited_icon.SetBounds (0, 0, _visited_icon.IntrinsicWidth, _visited_icon.IntrinsicHeight);
			_unvisited_icon.SetBounds (0, 0, _unvisited_icon.IntrinsicWidth, _unvisited_icon.IntrinsicHeight);
        }

        public PointOfInterest(float u, float v)
        {
            this._u = u;
            this._v = v;

			visited = false;

			_visited_icon.SetBounds (0, 0, _visited_icon.IntrinsicWidth, _visited_icon.IntrinsicHeight);
			_unvisited_icon.SetBounds (0, 0, _unvisited_icon.IntrinsicWidth, _unvisited_icon.IntrinsicHeight);
        }
        
        public float Radius
        {
			get { return this._visited_icon.IntrinsicWidth / 2.0f;}
        }

        public void Draw(Canvas canvas, float mapWidth, float mapHeight)
		{
			canvas.Translate (_u * mapWidth, _v * mapHeight);
			canvas.Translate (-_unvisited_icon.IntrinsicWidth / 2.0f, -_unvisited_icon.IntrinsicHeight / 2.0f);

			if (visited)
				_visited_icon.Draw (canvas);
			else
				_unvisited_icon.Draw (canvas);

			canvas.Translate (_unvisited_icon.IntrinsicWidth / 2.0f, _unvisited_icon.IntrinsicHeight / 2.0f);
			canvas.Translate (-_u * mapWidth, -_v * mapHeight);
		}

        public void SetTouched()
        {
			visited = true;
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