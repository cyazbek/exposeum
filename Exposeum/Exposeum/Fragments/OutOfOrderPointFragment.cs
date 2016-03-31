using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Models;

namespace Exposeum.Fragments
{
    public class OutOfOrderPointFragment : DialogFragment
    {

        private readonly PointOfInterest _currentPoint;
        private readonly IEnumerable<MapElement> _skippedPoints; 
		public delegate void Callback(PointOfInterest currentPoi, IEnumerable<MapElement> skippedPoints);
		private readonly Callback _callback;

		public OutOfOrderPointFragment(PointOfInterest currentPoint, IEnumerable<MapElement> skippedPoint, Callback callback)
        {
			_currentPoint = currentPoint;
            _skippedPoints = skippedPoint;
			_callback = callback;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.OutOfOrderPointPopup, container, false);
            var textview = view.FindViewById<TextView>(Resource.Id.wrongPointDesc);
            textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            textview.VerticalScrollBarEnabled = true;
            textview.HorizontalFadingEdgeEnabled = true;
            var noButton = view.FindViewById<Button>(Resource.Id.wrongPointButton);
            var yesButton = view.FindViewById<Button>(Resource.Id.rightPointButton);

            noButton.Click += (sender, e) =>
            {
                Dismiss();
                // maybe add shortest path to skipped poi if needed
            };

            yesButton.Click += (sender, e) =>
            {
                _callback(_currentPoint, _skippedPoints);
                Dismiss();
            };

            Dialog.SetCanceledOnTouchOutside(true);
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

    }
}