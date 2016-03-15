namespace Exposeum
{
	public interface IBeaconFinderObservable
	{
		void AddObserver(IBeaconFinderObserver observer);
		void NotifyObservers();
	}
}

