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
        StoryLine storyLine;
		Context context; 
        StorylineController _storylineController = StorylineController.GetInstance();
        
        public DialogStorylineInProgress(StoryLine storyLine){
			this.storyLine = storyLine;
		}
        
        public DialogStorylineInProgress(StoryLine storyLine,Context context)
        {
            this.storyLine = storyLine;
            this.context = context; 
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.StoryLine_dialog_inProgress, container, false);
                view.FindViewById<ImageView>(Resource.Id.sotryLineDialogPic).SetImageResource(storyLine.ImageId);
                view.FindViewById<TextView>(Resource.Id.storyLineDialogTitle).Text = storyLine.getName();
                view.FindViewById<TextView>(Resource.Id.storyLineDialogAudience).Text = storyLine.getAudience();
                view.FindViewById<TextView>(Resource.Id.storyLineDialogDuration).Text = storyLine.duration + " min";

            var textview = view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription);
                textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
                textview.VerticalScrollBarEnabled = true;
                textview.HorizontalFadingEdgeEnabled = true;

            var buttonToResume = view.FindViewById<Button>(Resource.Id.storyLineDialogButtonToResume);
            var buttonToReset = view.FindViewById<Button>(Resource.Id.storyLineDialogButtonToReset);
                

            if (Language.getLanguage() == "fr")
            {
                view.FindViewById<TextView>(Resource.Id.storyInProgress).Text = "Actuallement en cours.";
                buttonToResume.Text = "Reprendre";
                buttonToReset.Text = "Recommencer";
            }  
            else
            {
                view.FindViewById<TextView>(Resource.Id.storyInProgress).Text = "Storyline already in progress.";
                buttonToResume.Text = "Resume";
                buttonToReset.Text = "Restart";
            }

            buttonToResume.Click += delegate
            {
				_storylineController.SetActiveStoryLine();
                _storylineController.ResumeStorylineBeacons();
				var intent = new Intent(context, typeof(MapActivity));
                StartActivity(intent);
            };

            buttonToReset.Click += delegate {

                _storylineController.SetActiveStoryLine();
                _storylineController.ResetStorylineProgress(storyLine);
                _storylineController.ResumeStorylineBeacons();
                var intent = new Intent(context, typeof(MapActivity));
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