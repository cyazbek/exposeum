using System;

namespace Exposeum
{
	public class PointOfInterest
	{
		private float u;
		private float v;
		private String label;

		public PointOfInterest ()
		{
			u = 0.0f;
			v = 0.0f;
		}

		public PointOfInterest (float u, float v)
		{
			this.u = u;
			this.v = v;
		}

		public string Label
		{
			set { this.label = value; }
			get { return this.label; }
		}

		public string U
		{
			set { this.u = value; }
			get { return this.u; }
		}

		public string V
		{
			set { this.v = value; }
			get { return this.v; }
		}
	}
}
