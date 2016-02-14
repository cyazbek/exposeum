using System;
using Android.App;
using Android.Graphics;

namespace Exposeum.Models
{
	public class PointOfInterest
	{
		private float _u;
		private float _v;
		private String _label = "no label";
		private float _radius = 80;
		private readonly Paint _paint = new Paint();
        private Beacon beacon { get; set; }
        private string name_en
        {
            get; set;
        }
        private string name_fr
        {
            get; set;
        }
        private string description_en
        {
            get; set;
        }
        private string description_fr
        {
            get; set;
        }
        private int id
        {
            get; set;
        }

        public PointOfInterest (float u, float v, string label)
		{
			this._u = u;
			this._v = v;

			this._label = label;

			_paint.SetStyle (Paint.Style.Fill);
			_paint.Color = Color.OrangeRed;
		}

		public string Label_en
		{
			set { this._label = value; }
			get { return this._label; }
		}
        public string Label_fr
        {
            set { this._label = value; }
            get { return this._label; }
        }


        public float U
		{
			set { this._u = value; }
			get { return this._u; }
		}

		public float V
		{
			set { this._v = value; }
			get { return this._v; }
		}

		public float Radius
		{
			set { this._radius = value; }
			get { return this._radius; }
		}

		public void Draw(Canvas canvas, float mapWidth, float mapHeight)
		{
			canvas.DrawCircle (_u * mapWidth, _v * mapHeight, _radius, _paint);
		}

		public void SetTouched()
		{
			_paint.Color = Color.ForestGreen;
		}

		public String getHTML()
		{
            string summary; 
            if (Language.getLanguage()=="fr")
            {
                summary = String.Format("<html><body>Vous avez selectionnez {0}!<br><br></body></html>", _label);
            }
            else
                summary = String.Format("<html><body>You Selected {0}!<br><br></body></html>", _label);

            return summary;
		}
        public string getDescription()
        {
            if (Language.getLanguage() == "fr")
            {
                return this.description_fr;
            }
            else
                return this.description_en;
        }
        public string getName()
        {
            if (Language.getLanguage() == "fr")
            {
                return this.name_fr;
            }
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
    }
}
