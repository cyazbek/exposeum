using System;

namespace Exposeum
{
	public interface IBeaconFinderObserver
	{
		void BeaconFinderObserverUpdate(IBeaconFinderObservable observable);
	}
}

