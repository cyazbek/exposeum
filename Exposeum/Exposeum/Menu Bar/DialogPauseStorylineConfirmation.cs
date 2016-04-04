using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Controllers;
using Exposeum.Models;

namespace Exposeum.Menu_Bar
{
    class DialogPauseStorylineConfirmation : DialogFragment
    {
        StoryLine _storyLine;
        readonly StorylineController _storylineController = StorylineController.GetInstance();
        private readonly Context _context;

        public DialogPauseStorylineConfirmation(StoryLine storyLine, Context context)
        {
            _storyLine = storyLine;
            _context = context;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Pause_confirmation, container, false);
            var buttonToConfirmPause = view.FindViewById<Button>(Resource.Id.confirm_pause);
            var buttonToRejectPause = view.FindViewById<Button>(Resource.Id.reject_pause);
            var textView = view.FindViewById<TextView>(Resource.Id.pause_text); 
            buttonToConfirmPause.Text = User.GetInstance().GetButtonText("confirm_pause");
            buttonToRejectPause.Text = User.GetInstance().GetButtonText("reject_pause");
            textView.Text = User.GetInstance().GetButtonText("pause_text");

            buttonToConfirmPause.Click += delegate
            {
                _storylineController.PauseStorylineBeacons();
                Toast.MakeText(_context, "Paused", ToastLength.Short).Show();
                ((Activity)_context).OnBackPressed();
                Dismiss();

                // Redirecting to list of storylines
                //var intent = new Intent(_context, typeof(StoryLineListActivity));
                //StartActivity(intent);
            };

            buttonToRejectPause.Click += delegate {
                Dialog.Dismiss();
            };

            Dialog.SetCanceledOnTouchOutside(true);
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_upup;
        }

    }
}