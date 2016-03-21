namespace Exposeum.Models
{
	public interface IBeaconFinderObservable
	{
		void AddObserver(IBeaconFinderObserver observer);
		void NotifyObservers();
	}
}

