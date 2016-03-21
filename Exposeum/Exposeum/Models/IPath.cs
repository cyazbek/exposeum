using System.Collections.Generic;

namespace Exposeum.Models
{
	public interface IPath
	{
		Status CurrentStatus { get; set; }
		List<MapElement> MapElements { get; set; }

		void AddMapElement (MapElement e);
		void UpdateProgress (MapElement mapElement);
		PointOfInterest FindPoi (EstimoteSdk.Beacon beacon);
		bool HasBeacon (EstimoteSdk.Beacon beacon);
	}
}

