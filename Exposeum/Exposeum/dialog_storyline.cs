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
    class dialog_storyline : DialogFragment
    {
        StoryLine storyLine;
        public dialog_storyline(StoryLine storyLine)
        {
            this.storyLine = storyLine;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.StoryLine_Dialog, container, false);
            view.FindViewById<ImageView>(Resource.Id.sotryLineDialogPic).SetImageResource(storyLine.ImageId);
            view.FindViewById<TextView>(Resource.Id.storyLineDialogTitle).Text = storyLine.getName();
            view.FindViewById<TextView>(Resource.Id.storyLineDialogAudience).Text = storyLine.getAudience();
            view.FindViewById<TextView>(Resource.Id.storyLineDialogDuration).Text = storyLine.duration + " min";
            view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription).Text = storyLine.getDescription();
            var textview = view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription);
            textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            textview.VerticalScrollBarEnabled = true;
            textview.HorizontalFadingEdgeEnabled = true;
            var button = view.FindViewById<Button>(Resource.Id.storyLineDialogButton);
            if (Language.getLanguage() == "fr")
                button.Text = "Commencez l'�xperience";
            else
                button.Text = "Begin Journey";
            return view;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

    }
}