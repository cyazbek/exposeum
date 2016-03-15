using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Models;
using Exposeum.Controllers;

namespace Exposeum
{
    class DialogStoryline : DialogFragment
    {
        private StoryLine _storyLine;
        private Context _context; 
		private StorylineController _storylineController = StorylineController.GetInstance();
        private User _user = User.GetInstance(); 
        
        public DialogStoryline(StoryLine storyLine, Context context)
        {
            _storyLine = storyLine;
            _context = context;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.StoryLine_Dialog, container, false);
            view.FindViewById<ImageView>(Resource.Id.sotryLineDialogPic).SetImageResource(_storyLine.ImageId);
            view.FindViewById<TextView>(Resource.Id.storyLineDialogTitle).Text = _storyLine.GetName();
            view.FindViewById<TextView>(Resource.Id.storyLineDialogAudience).Text = _storyLine.GetAudience();
            view.FindViewById<TextView>(Resource.Id.storyLineDialogDuration).Text = _storyLine.Duration + " min";
            view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription).Text = _storyLine.GetDescription();
            var textview = view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription);
            textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            textview.VerticalScrollBarEnabled = true;
            textview.HorizontalFadingEdgeEnabled = true;
            var button = view.FindViewById<Button>(Resource.Id.storyLineDialogButton);
            button.Text = _user.GetButtonText("storyLineDialogButton");
            button.Click += delegate {
				_storylineController.SetActiveStoryLine();
                _storylineController.BeginJournery();
                var intent = new Intent(_context, typeof(MapActivity));
                StartActivity(intent);
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