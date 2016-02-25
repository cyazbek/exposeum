using System;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;

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
		private float _icon_scale_factor = 0.2f;

        public PointOfInterestDescription description { get; set; }

        private Drawable _visited_icon;
        private Drawable _unvisited_icon;

        public PointOfInterest()
        {
			setVisitedUnvisitedIcons ();

			visited = false;

			_visited_icon.SetBounds (0, 0, _visited_icon.IntrinsicWidth, _visited_icon.IntrinsicHeight);
			_unvisited_icon.SetBounds (0, 0, _unvisited_icon.IntrinsicWidth, _unvisited_icon.IntrinsicHeight);
        }

        public PointOfInterest(float u, float v)
        {
			setVisitedUnvisitedIcons ();

            this._u = u;
            this._v = v;

			visited = false;

			_visited_icon.SetBounds (0, 0, _visited_icon.IntrinsicWidth, _visited_icon.IntrinsicHeight);
			_unvisited_icon.SetBounds (0, 0, _unvisited_icon.IntrinsicWidth, _unvisited_icon.IntrinsicHeight);
        }

		public void setVisitedUnvisitedIcons(){

			try{
				    _visited_icon = Application.Context.Resources.GetDrawable (Resource.Drawable.Beacon_Activated);
			}catch(Exception e){
				_visited_icon = new ColorDrawable ();
			}

			try{
                    _unvisited_icon = Application.Context.Resources.GetDrawable (Resource.Drawable.Beacon_Inactivated);
			}catch(Exception e){
				_unvisited_icon = new ColorDrawable ();
			}

		}
        
        public float Radius
        {
			get { return _icon_scale_factor * (this._visited_icon.IntrinsicWidth / 2.0f);}
        }

        public void Draw(Canvas canvas, float mapWidth, float mapHeight)
		{
			canvas.Translate (_u * mapWidth, _v * mapHeight);
			canvas.Scale (_icon_scale_factor, _icon_scale_factor);
			canvas.Translate (-_unvisited_icon.IntrinsicWidth / 2.0f, -_unvisited_icon.IntrinsicHeight / 2.0f);

			if (visited)
				_visited_icon.Draw (canvas);
			else
				_unvisited_icon.Draw (canvas);

			canvas.Translate (_unvisited_icon.IntrinsicWidth / 2.0f, _unvisited_icon.IntrinsicHeight / 2.0f);
			canvas.Scale (1.0f/_icon_scale_factor, 1.0f/_icon_scale_factor);
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
                summary = String.Format("<html><body><h1>Vous avez selectionnez {0}!{1}</h1><br><br></body></html>", name_fr ,"@string/app_name");
            else
                summary = description.htmlFormat();
            //summary = String.Format("<html><body>You selected {0}!<br><br></body></html>", name_en );

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