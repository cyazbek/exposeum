using Android.Views;
using Exposeum.Models;

namespace Exposeum.Controllers
{
    public class TopMenuController
    {
        private readonly BeaconFinder _beaconFinder = BeaconFinder.getInstance();

        public void PauseStoryline()
        {
            _beaconFinder.stopBeaconFinder();
        }

        public void ResumeStoryline()
        {
            _beaconFinder.findBeacons();
        }

    }
}