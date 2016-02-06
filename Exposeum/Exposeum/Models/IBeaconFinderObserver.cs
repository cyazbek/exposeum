using System;

namespace Exposeum
{
	public interface IBeaconFinderObserver
	{
		void beaconFinderObserverUpdate(IBeaconFinderObservable observable);
	}
}

