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
using Exposeum.Models;

namespace Exposeum
{
    class DialogStorylineInProgress : DialogFragment
    {
        StoryLine storyLine;
        
        public DialogStorylineInProgress(StoryLine storyLine)
        {
            this.storyLine = storyLine;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.StoryLine_dialog_inProgress, container, false);
            view.FindViewById<ImageView>(Resource.Id.sotryLineDialogPic).SetImageResource(storyLine.ImageId);
            view.FindViewById<TextView>(Resource.Id.storyLineDialogTitle).Text = storyLine.getName();
            view.FindViewById<TextView>(Resource.Id.storyLineDialogAudience).Text = storyLine.getAudience();
            view.FindViewById<TextView>(Resource.Id.storyLineDialogDuration).Text = storyLine.duration + " min";
            view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription).Text = storyLine.getDescription();
            var textview = view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription);
            textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            textview.VerticalScrollBarEnabled = true;
            textview.HorizontalFadingEdgeEnabled = true;
            var buttonToResume = view.FindViewById<Button>(Resource.Id.storyLineDialogButtonToResume);
            var buttonToReset = view.FindViewById<Button>(Resource.Id.storyLineDialogButtonToReset);
            if (Language.getLanguage() == "fr")
            {
                buttonToResume.Text = "Continuer";
                buttonToReset.Text = "Recommencer";
            }
                
            else
            {
                buttonToResume.Text = "Resume Journey";
                buttonToReset.Text = "Restart Jouney";
            }
            buttonToResume.Click += delegate {
               
            };
            buttonToReset.Click += delegate {

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