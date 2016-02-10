using System;

namespace Exposeum
{
	public interface IBeaconFinderObservable
	{
		void addObserver(IBeaconFinderObserver observer);
		void notifyObservers();
	}
}

