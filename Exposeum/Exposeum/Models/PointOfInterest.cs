using System;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text.Style;
using Java.Util;

namespace Exposeum.Models
{
	public class PointOfInterest : MapElement
    {
        public Beacon Beacon { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionFr { get; set; }
        public int Id { get; set; }
        public int StoryId { get; set; }
		private float _iconScaleFactor = 0.2f;

        public PointOfInterestDescription Description { get; set; }

        private Drawable _visitedIcon;
        private Drawable _unvisitedIcon;

		//TODO: Remove this constructor
        public PointOfInterest()
        {
			SetVisitedUnvisitedIcons ();

			Visited = false;
            Beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 00000, 00000);
        }

		//TODO: Remove this constructor
        public PointOfInterest(float u, float v)
        {
            this.U = u;
            this.V = v;

            Beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 00000, 00000);
            Visited = false;

			SetVisitedUnvisitedIcons ();
        }

		public PointOfInterest(float u, float v, Floor floor) : base(u, v, floor)
	    {
			Beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 00000, 00000);
			SetVisitedUnvisitedIcons ();
	    }

		public void SetVisitedUnvisitedIcons()
		{
			try{
			    _visitedIcon = Application.Context.Resources.GetDrawable (Resource.Drawable.Beacon_Activated);
			}catch(Exception e){
				_visitedIcon = new ColorDrawable ();
			}

			try{
				_unvisitedIcon = Application.Context.Resources.GetDrawable (Resource.Drawable.Beacon_Inactivated);
			}catch(Exception e){
				_unvisitedIcon = new ColorDrawable ();
			}

			_visitedIcon.SetBounds (0, 0, _visitedIcon.IntrinsicWidth, _visitedIcon.IntrinsicHeight);
			_unvisitedIcon.SetBounds (0, 0, _unvisitedIcon.IntrinsicWidth, _unvisitedIcon.IntrinsicHeight);

		}
        
        public float Radius
        {
			get { return _iconScaleFactor * (this._visitedIcon.IntrinsicWidth / 2.0f);}
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Save();

			canvas.Translate (U * Floor.Image.IntrinsicWidth, V * Floor.Image.IntrinsicHeight);
			canvas.Scale (_iconScaleFactor, _iconScaleFactor);
			canvas.Translate (-_unvisitedIcon.IntrinsicWidth / 2.0f, -_unvisitedIcon.IntrinsicHeight / 2.0f);

			if (Visited)
				_visitedIcon.Draw (canvas);
			else
				_unvisitedIcon.Draw (canvas);

			canvas.Restore();
		}

        public String GetHtml()
        {
            string summary;

            if (Language.GetLanguage() == "fr")
                summary = String.Format("<html><body>Vous avez selectionnez {0}!<br><br></body></html>", NameFr);
            else
                summary = String.Format("<html><body>You selected {0}!<br><br></body></html>", NameEn );

            return summary;
        }

        public string GetDescription()
        {
            if (Language.GetLanguage() == "fr")
                return this.DescriptionFr;
            else
                return this.DescriptionEn;
        }

        public string GetName()
        {
            if (Language.GetLanguage() == "fr")
                return this.NameFr;
            else
                return this.NameEn;
        }

        public bool CheckBeacon(Beacon b)
        {
            if (this.Beacon == null)
                return false;
            else if (this.Beacon.Uuid.Equals(b.Uuid) & this.Beacon.Minor == b.Minor & this.Beacon.Major == b.Major)
                return true;
            else
                return false;
        }

        public void ConvertFromData(Data.PoiData poi)
        {
            // this.visited = poi.visited;
            this.NameEn = poi.NameEn;
            this.NameFr = poi.NameFr;
            this.DescriptionEn = poi.DscriptionEn;
            this.DescriptionFr = poi.DscriptionFr;
            this.U = poi.UCoord;
            this.V = poi.VCoord;
            this.Id = poi.Id;
        }

        public string toString()
        {
            return this.Id + " " + this.GetName() + " " + this.GetDescription();
        }
    }
}