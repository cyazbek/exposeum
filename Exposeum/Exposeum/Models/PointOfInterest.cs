using System;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Java.Util;
using System.Collections.Generic;

namespace Exposeum.Models
{
	public class PointOfInterest : MapElement
    {
        public Beacon Beacon { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionFr { get; set; }
        public int StoryLineId { get; set; }
        public int Id { get; set; }
        public int StoryId { get; set; }
        public PointOfInterestDescription Description { get; set; }
        public List<ExhibitionContent> ExhibitionContent { get; set; }
        private readonly float _iconScaleFactor = 0.2f;

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
        public PointOfInterest(float uCoordinate, float vCoordinate)
        {
            UCoordinate = uCoordinate;
            VCoordinate = vCoordinate;

            Beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 00000, 00000);
            Visited = false;

			SetVisitedUnvisitedIcons ();
        }

		public PointOfInterest(float uCoordinate, float vCoordinate, Floor floor) : base(uCoordinate, vCoordinate, floor)
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
			get { return _iconScaleFactor * (_visitedIcon.IntrinsicWidth / 2.0f);}
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Save();

			canvas.Translate (UCoordinate * Floor.Image.IntrinsicWidth, VCoordinate * Floor.Image.IntrinsicHeight);
			canvas.Scale (_iconScaleFactor, _iconScaleFactor);
			canvas.Translate (-_unvisitedIcon.IntrinsicWidth / 2.0f, -_unvisitedIcon.IntrinsicHeight / 2.0f);

			if (Visited)
				_visitedIcon.Draw (canvas);
			else
				_unvisitedIcon.Draw (canvas);

			canvas.Restore();
		}

        public string GetDescription()
        {
            return Description.Description;
        }

	    public string GetName()
	    {
	        return Description.Title;
	    }

	    public bool CheckBeacon(Beacon b)
	    {
	        if (Beacon == null)
                return false;
	        if (Beacon.Uuid.Equals(b.Uuid) & Beacon.Minor == b.Minor & Beacon.Major == b.Major)
	            return true;
	        return false;
	    }


	    public string toString()
        {
            return Id + " " + GetName() + " " + GetDescription();
        }
    }
}