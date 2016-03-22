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
		private readonly PointOfInterest _skippedPoint;
		public delegate void Callback(PointOfInterest currentPoi, PointOfInterest skippedPoi);
		private Callback _callback;

		public OutOfOrderPointFragment(PointOfInterest currentPoint, PointOfInterest skippedPoint, Callback callback)
        {
			_currentPoint = currentPoint;
			_skippedPoint = skippedPoint;
			_callback = callback;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.OutOfOrderPointPopup, container, false);
			view.FindViewById<TextView>(Resource.Id.wrongPointDesc).Text += _skippedPoint.NameEn;
            var textview = view.FindViewById<TextView>(Resource.Id.wrongPointDesc);
            textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            textview.VerticalScrollBarEnabled = true;
            textview.HorizontalFadingEdgeEnabled = true;
            var button = view.FindViewById<Button>(Resource.Id.wrongPointButton);
            button.Text = User.GetInstance().GetButtonText("wrongPointButton");
            
            button.Click += (sender, e) =>
            {
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