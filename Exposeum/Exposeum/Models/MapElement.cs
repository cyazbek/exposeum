using System;
using Exposeum.Models;

namespace Exposeum
{
    public abstract class MapElement
	{
		public int x_coordinate, y_coordinate;
		public Floor floor;

		public MapElement ()
		{
		}
	}
}

