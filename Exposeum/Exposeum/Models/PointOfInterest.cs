using System;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Java.Util;
using System.Collections.Generic;
using Exposeum.Mappers;

namespace Exposeum.Models
{
    public class PointOfInterest : MapElement 
    {
        public Beacon Beacon { get; set; }
        public int StoryId { get; set; }
        private readonly float _iconScaleFactor = 0.2f;
        public PointOfInterestDescription Description { get; set; }
        public List<ExhibitionContent> ExhibitionContent { get; set; }
        private Drawable _visitedIcon;
        private Drawable _unvisitedIcon;

        //TODO: Remove this constructor
        public PointOfInterest()
        {
            SetVisitedUnvisitedIcons();

            Visited = false;
            Beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 00000, 00000);
        }

        //TODO: Remove this constructor
        public PointOfInterest(float UCoordinate, float VCoordinate)
        {
            UCoordinate = UCoordinate;
            VCoordinate = VCoordinate;

            Beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 00000, 00000);
            Visited = false;

            SetVisitedUnvisitedIcons();
        }

        public PointOfInterest(float UCoordinate, float VCoordinate, Floor floor) : base(UCoordinate, VCoordinate, floor)
        {
            Beacon = new Beacon(UUID.FromString("b9407f30-f5f8-466e-aff9-25556b57fe6d"), 00000, 00000);
            SetVisitedUnvisitedIcons();
        }

        public void SetVisitedUnvisitedIcons()
        {
            try
            {
                _visitedIcon = Application.Context.Resources.GetDrawable(Resource.Drawable.Beacon_Activated);
            }
            catch (Exception e)
            {
                _visitedIcon = new ColorDrawable();
            }

            try
            {
                _unvisitedIcon = Application.Context.Resources.GetDrawable(Resource.Drawable.Beacon_Inactivated);
            }
            catch (Exception e)
            {
                _unvisitedIcon = new ColorDrawable();
            }

            _visitedIcon.SetBounds(0, 0, _visitedIcon.IntrinsicWidth, _visitedIcon.IntrinsicHeight);
            _unvisitedIcon.SetBounds(0, 0, _unvisitedIcon.IntrinsicWidth, _unvisitedIcon.IntrinsicHeight);

        }

        public float Radius
        {
            get { return _iconScaleFactor * (_visitedIcon.IntrinsicWidth / 2.0f); }
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Save();

			canvas.Translate(UCoordinate * Floor.Width, VCoordinate * Floor.Height);
            canvas.Scale(_iconScaleFactor, _iconScaleFactor);
            canvas.Translate(-_unvisitedIcon.IntrinsicWidth / 2.0f, -_unvisitedIcon.IntrinsicHeight / 2.0f);

            if (Visited)
                _visitedIcon.Draw(canvas);
            else
                _unvisitedIcon.Draw(canvas);

            canvas.Restore();
        }

        public String GetHtml()
        {
            if (!Visited)
            {
                return String.Format("<html><body>" + Description.Title + Description.Summary + "</body></html>");
            }
            else
            {
                return String.Format("<html><body>" + Description.Title + Description.Description + ExhibitionContentBuild()+ "</body></html>");
            }
        }

        private string ExhibitionContentBuild()
        {
            if (ExhibitionContent == null || ExhibitionContent.Count < 1)
            {
                return "";
            }
            else
            {
                string str = "";
                foreach (var content in ExhibitionContent)
                {
                    str += content.HtmlFormat();
                }
                return str;
            }
        }

        public string GetDescription()
        {
            return Description.Description;
        }

        public string GetName()
        {
            return Description.Title;
        }

        public string GetSummary()
        {
            return Description.Summary;
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

        public bool AreEquals(PointOfInterest other)
        {
            return other.Id == Id &&
                   other.Visited == Visited &&
                   other.IconPath.Equals(IconPath) &&
                   Math.Abs(other.UCoordinate - UCoordinate) <= 0 &&
                   Math.Abs(other.VCoordinate - VCoordinate) <= 0 &&
                   other.Floor.IsEqual(Floor) && 
                    Models.ExhibitionContent.ListEquals(ExhibitionContent, other.ExhibitionContent) &&
                    Beacon.Equals(other.Beacon) &&
                    StoryId == other.StoryId &&
                    Description.Equals(other.Description);
        }

       

       
    }
}