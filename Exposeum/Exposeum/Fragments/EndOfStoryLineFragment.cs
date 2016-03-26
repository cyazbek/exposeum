using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Exposeum.Fragments
{
    public class EndOfStoryLineFragment : DialogFragment
    {
		public delegate void Callback(bool directionsToStart);
		private Callback _callback;

		public EndOfStoryLineFragment(Callback callback)
        {
			_callback = callback;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
			var view = inflater.Inflate(Resource.Layout.EndOfStoryLinePopup, container, false);
			var textview = view.FindViewById<TextView>(Resource.Id.storyLineCompleteDescription);
            textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            textview.VerticalScrollBarEnabled = true;
            textview.HorizontalFadingEdgeEnabled = true;
			var buttonYes = view.FindViewById<Button>(Resource.Id.guidedBackToStartTrueButton);
            
			buttonYes.Click += (sender, e) =>
            {
                Dismiss();
				if(_callback != null)
					_callback(true);
            };

			var buttonNo = view.FindViewById<Button>(Resource.Id.guidedBackToStartFalseButton);

			buttonNo.Click += (sender, e) =>
			{
				Dismiss();
				if(_callback != null)
					_callback(false);
			};

            Dialog.SetCanceledOnTouchOutside(true);
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

		public void SetCallBack(Callback callback){
			_callback = callback;
		}

    }
}