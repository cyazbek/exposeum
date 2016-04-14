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
		public delegate void CallbackSkipUnvisitedPoints(PointOfInterest currentPoi, IEnumerable<MapElement> skippedPoints);
        private readonly CallbackSkipUnvisitedPoints _callbackSkip;

        public delegate void CallbackReturnToLastPoint(PointOfInterest start);
        private readonly CallbackReturnToLastPoint _callbackReturn;

		public OutOfOrderPointFragment(PointOfInterest currentPoint, IEnumerable<MapElement> skippedPoint, CallbackSkipUnvisitedPoints callbackSkip, CallbackReturnToLastPoint callbackReturn)
        {
			_currentPoint = currentPoint;
            _skippedPoints = skippedPoint;
            _callbackSkip = callbackSkip;
		    _callbackReturn = callbackReturn;
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

            yesButton.Click += (sender, e) =>
            {
                _callbackReturn(_currentPoint);
                Dismiss();
            };

            noButton.Click += (sender, e) =>
            {
                _callbackSkip(_currentPoint, _skippedPoints);
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