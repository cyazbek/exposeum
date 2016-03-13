using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Exposeum.Controllers;
using Exposeum.Models;

namespace Exposeum
{
    class DialogStorylineInProgress : DialogFragment
    {
        StoryLine _storyLine;
		Context _context; 
        StorylineController _storylineController = StorylineController.GetInstance();
        User user = User.GetInstance(); 
        public DialogStorylineInProgress(StoryLine storyLine){
			this._storyLine = storyLine;
		}
        
        public DialogStorylineInProgress(StoryLine storyLine,Context context)
        {
            this._storyLine = storyLine;
            this._context = context; 
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.StoryLine_dialog_inProgress, container, false);
                view.FindViewById<ImageView>(Resource.Id.sotryLineDialogPic).SetImageResource(_storyLine.ImageId);
                view.FindViewById<TextView>(Resource.Id.storyLineDialogTitle).Text = _storyLine.GetName();
                view.FindViewById<TextView>(Resource.Id.storyLineDialogAudience).Text = _storyLine.GetAudience();
                view.FindViewById<TextView>(Resource.Id.storyLineDialogDuration).Text = _storyLine.Duration + " min";

            var textview = view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription);
                textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
                textview.VerticalScrollBarEnabled = true;
                textview.HorizontalFadingEdgeEnabled = true;

            var buttonToResume = view.FindViewById<Button>(Resource.Id.storyLineDialogButtonToResume);
            var buttonToReset = view.FindViewById<Button>(Resource.Id.storyLineDialogButtonToReset);
            var textView = view.FindViewById<TextView>(Resource.Id.storyInProgress);
            buttonToResume.Text = user.GetButtonText("storyLineDialogButtonToResume");
            buttonToReset.Text = user.GetButtonText("storyLineDialogButtonToReset");
            textView.Text = user.GetButtonText("storyInProgress");

            buttonToResume.Click += delegate
            {
				_storylineController.SetActiveStoryLine();
                _storylineController.ResumeStorylineBeacons();
                string name = _storylineController._selectedStoryLine.NameEn;
                Toast.MakeText(_context, name, ToastLength.Long).Show();
                var intent = new Intent(_context, typeof(MapActivity));
                StartActivity(intent);
            };

            buttonToReset.Click += delegate {
                _storylineController.SetActiveStoryLine();
                _storylineController.ResetStorylineProgress();
                _storylineController.ResumeStorylineBeacons();
                string name = _storylineController._selectedStoryLine.NameEn;
                Toast.MakeText(_context, name, ToastLength.Long).Show();
                var intent = new Intent(_context, typeof(MapActivity));
                StartActivity(intent);
            };

            this.Dialog.SetCanceledOnTouchOutside(true);
            return view;
        }


        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }
        
    }
}