using System;
using Android.Graphics;

namespace Exposeum.Models
{
	public class PointOfTravel : Node
	{
		public PointOfTravel() : base()
		{
		}

		public PointOfTravel(float u, float v) : base(u, v)
		{
		}

		public PointOfTravel(float u, float v, Floor floor) : base (u, v, floor)
		{
		}

		public override void Draw(Canvas canvas){
			
		}
	
	}
}

